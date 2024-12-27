using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Diagnostics;
using TS.FW.Utils;

namespace TS.FW.Elec
{
    public class ElectMeterClient
    {
        private const ushort START = 20007;
        private const ushort END = 20054;//20032;
        private const ushort COUNT = END - START + 1;
        private const byte FNC_CODE = 0x04;

        private const BitHandler.ByteOrder ORDER = BitHandler.ByteOrder.BigEndian;

        private readonly BackgroundTimer _trUpdate = new BackgroundTimer(System.Threading.ApartmentState.MTA);

        private TcpClient _tcpClient = null;
        private NetworkStream _stream = null;

        private byte _unitID;
        private byte[] _packet = null;
        private byte[] _recvBuffe = new byte[200];

        private object _obj = new object();
        public ElectMeterData Data { get; private set; } = new ElectMeterData();

        public bool IsConnect => this._tcpClient == null ? false : this._tcpClient.Connected;
        public ElectMeterClient()
        {
            this._trUpdate.SleepTimeMsc = 500;
            this._trUpdate.DoWork += _trUpdate_DoWork;
        }

        private int _readType = 0;

        private void _trUpdate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                if (this._tcpClient == null)
                {
                    //todo :연결이 해지 되면 다시 재 연결 시도 하는 코드 추가해야함. 230424 by Jp
                    this._trUpdate.Stop();
                    return;
                }

                if (this._tcpClient.Connected == false) return;

                var res = BeginRead();

                this.Write();

                if (res.AsyncWaitHandle.WaitOne(1000) == false)
                {
                    this._stream.EndRead(res);
                    this.Close();
                    return;
                }

                this.EndRead(res);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public Response Connect(string ip, int port, byte unitID)
        {
            try
            {
                this._tcpClient = this._tcpClient ?? new TcpClient();

                this._unitID = unitID;

                var ipAddress = IPAddress.Parse(ip);

                var res = this._tcpClient.BeginConnect(ipAddress, port, this.ConnectCallback, null);
                if (res.AsyncWaitHandle.WaitOne(500) == false) return new Response(false, "연결 타임아웃");

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public void Close()
        {
            try
            {
                if (this._stream != null)
                {
                    this._stream.Close();
                    this._stream.Dispose();
                    this._stream = null;
                }

                if (this._tcpClient.Connected) this._tcpClient.Close();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                this._tcpClient = null;
            }
        }

        private void Write()
        {
            var buffer = this.ToPacketData();

            this._stream.Write(buffer, 0, buffer.Length);

            this._packet = null;
        }

        private IAsyncResult BeginRead()
        {
            Array.Clear(this._recvBuffe, 0, this._recvBuffe.Length);

            return this._stream.BeginRead(this._recvBuffe, 0, this._recvBuffe.Length, null, null);
        }

        private void EndRead(IAsyncResult result)
        {
            var len = this._stream.EndRead(result);
            if (len <= 0)
            {
                this.Close();
                return;
            }

            var buffer = this._recvBuffe.Take(len).ToArray();

            var i = 0;

            var trID = buffer.ToUInt16(ref i, BitHandler.ByteOrder.BigEndian);
            var pID = buffer.ToUInt16(ref i, BitHandler.ByteOrder.BigEndian);
            var length = buffer.ToUInt16(ref i, BitHandler.ByteOrder.BigEndian);
            var unitId = buffer[i++];
            var fncCode = buffer[i++];
            var count = buffer[i++];

            var data = buffer.Skip(i).Take(count).ToArray();

            lock (this._obj)
            {
                if (this._readType == 0)
                {
                    //기본 SEM 정보
                    this.Data.SetData(data, ORDER);
                    this._readType = 1;
                }
                else
                {
                    //누설전류 읽어라
                    this.Data.SetData2(data, ORDER);
                    this._readType = 0;
                }
            }
        }


        private void ConnectCallback(IAsyncResult result)
        {
            try
            {
                this._tcpClient.EndConnect(result);

                if (this._tcpClient.Connected == false) return;

                this._stream = this._tcpClient.GetStream();

                this._trUpdate.Start();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public byte[] ToPacketData()
        {
            this._packet = this._packet ?? this.ToPacket().ToArray();

            return this._packet;
        }

        private IEnumerable<byte> ToPacket()
        {
            ushort transactionID = 0;
            ushort protocolID = 0;

            foreach (var item in transactionID.ToByte(ORDER))
            {
                yield return item;
            }

            foreach (var item in protocolID.ToByte(ORDER))
            {
                yield return item;
            }

            var list = this.ToData().ToList();

            foreach (var item in ((ushort)list.Count).ToByte(ORDER))
            {
                yield return item;
            }

            foreach (var item in list)
            {
                yield return item;
            }
        }

        private IEnumerable<byte> ToData()
        {
            yield return this._unitID;

            yield return FNC_CODE;

            if (this._readType == 0)
            {
                var tt = START.ToByte(ORDER);
                foreach (var item in START.ToByte(ORDER)) yield return item;

                foreach (var item in COUNT.ToByte(ORDER)) yield return item;
            }
            else
            {
                ushort msg = 154;
                ushort len = 2;
                foreach (var item in msg.ToByte(ORDER)) yield return item;

                foreach (var item in len.ToByte(ORDER)) yield return item;
            }
        }
    }
}
