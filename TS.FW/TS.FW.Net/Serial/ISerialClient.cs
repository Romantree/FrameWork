﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TS.FW.Helper;
using TS.FW.Wpf.Helper;

namespace TS.FW.Net.Serial
{
    public enum enReturnType
    {
        Callback = 1,
        SendRecv,
    }
    public abstract class ISerialClient
    {
        protected const int ACK = 0x06;
        protected const int ENQ = 0x05;
        protected const int CR = 0x0D;
        protected const int LF = 0x0A;
        protected const int DELEY = 0x31;

        protected readonly Dictionary<int, double> _dataList = new Dictionary<int, double>();

        private readonly SerialPort _client = new SerialPort();
        private readonly object _locker = new object();

        private int _recvErrorCount = 0;
        protected int _recvMaxErrorCount = 10;

        public virtual bool Connected => this.IsConnected();

        public bool RecvError => this._recvErrorCount >= this._recvMaxErrorCount;

        public virtual int DeleyTime => 1000;

        public virtual Encoding Encoding => Encoding.ASCII;

        private readonly enReturnType ReturnType;
        public delegate void OnReceiveEventHandler(byte[] Recv);
        public event OnReceiveEventHandler OnReceive;

        public ISerialClient(enReturnType returntype = enReturnType.SendRecv)
        {
            this._client.WriteBufferSize = 1024;
            this._client.ReadBufferSize = 1024;

            ReturnType = returntype;

            if (ReturnType == enReturnType.Callback)
            {
                this._client.DataReceived += _client_DataReceived;
                this._client.ErrorReceived += _client_ErrorReceived;
            }
        }

        private void _client_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (_client.BytesToRead > 0)
                {
                    var buffer = new byte[1024];

                    var len = this._client.Read(buffer, 0, buffer.Length);
                    if (len == 0) return;

                    if (this.OnReceive != null)
                    {
                        var recv = buffer.Take(len).ToArray();
                        this.OnReceive(recv);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void _client_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {

        }

        public virtual bool Open(NetSerialData item, double readTimeout = 1, double sendTimeout = 1)
        {
            try
            {
                if (item == null || string.IsNullOrWhiteSpace(item.PortName)) return true; // 아직 통신 설정이 안됨

                if (this._client.IsOpen) this.Close();

                lock (this._locker)
                {
                    this._client.PortName = item.PortName;
                    this._client.BaudRate = item.BaudRate;
                    this._client.Parity = item.Parity;
                    this._client.DataBits = item.DataBits;
                    this._client.StopBits = item.StopBits;

                    this._client.Encoding = this.Encoding;
                    this._client.ReadTimeout = Convert.ToInt32(readTimeout * 1000);
                    this._client.WriteTimeout = Convert.ToInt32(sendTimeout * 1000);

                    this._client.ReadBufferSize = 1024; //일단 이정도면 충분함

                    this._client.Open();
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);

                return false;
            }
        }

        public virtual void Close()
        {
            try
            {
                if (this._client.IsOpen == false) return;

                lock (this._locker)
                {
                    this._client.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        protected virtual byte[] ReadData()
        {
            try
            {
                if (this._client.IsOpen == false) return null;

                lock (this._locker)
                {
                    var buffer = new byte[this._client.ReadBufferSize];

                    var len = this._client.Read(buffer, 0, buffer.Length);
                    if (len == 0) return null;

                    this._recvErrorCount = 0;

                    return buffer.Take(len).ToArray();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                this._recvErrorCount++;

                return null;
            }
        }

        protected virtual void SendData(byte[] buffer)
        {
            try
            {
                if (this._client.IsOpen == false) return;

                lock (this._locker)
                {
                    this._client.Write(buffer, 0, buffer.Length);

                    this.Sleep(this.DeleyTime);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        protected void Sleep(int deley)
        {
            var value = deley / 10;

            for (int i = 0; i < value; i++)
            {
                DoEvent.DoEvents();

                Thread.Sleep(10);
            }
        }

        private bool IsConnected()
        {
            try
            {
                if (this._client == null) return false;

                return this._client.IsOpen;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }

            return false;
        }

        protected IEnumerable<byte> CheckSum(byte[] buffer)
        {
            //yield return ENQ;

            foreach (var item in buffer)
            {
                yield return item;
            }

            foreach (var item in this.CheckSum(buffer.Sum(t => t)))
            {
                yield return item;
            }

            //yield return CR;
            //yield return LF;
        }

        protected IEnumerable<byte> CheckSum(int checkSum)
        {
            var value = this.ToHex(checkSum, 2);

            if (value.Length > 2)
            {
                var len = value.Length - 2;

                foreach (var item in this.Encoding.GetBytes(value.Skip(len).ToArray()))
                {
                    yield return item;
                }
            }
            else
            {
                foreach (var item in this.Encoding.GetBytes(value))
                {
                    yield return item;
                }
            }
        }

        protected string ToHex(int value, int len)
        {
            return Convert.ToString(value, 16).PadLeft(len, '0').ToUpper();
        }
    }
}
