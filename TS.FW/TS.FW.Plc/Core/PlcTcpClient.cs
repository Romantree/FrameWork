using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TS.FW.Diagnostics;

namespace TS.FW.Plc.Core
{
    internal class PlcTcpClient
    {
        private class TcpKeepAlive
        {
            public uint OnOff { get; set; }

            public uint KeepAliveTime { get; set; }

            public uint KeepAliveInterval { get; set; }

            public byte[] GetBytes()
            {
                return BitConverter.GetBytes(OnOff).Concat(BitConverter.GetBytes(KeepAliveTime)).Concat(BitConverter.GetBytes(KeepAliveInterval)).ToArray();
            }
        }

        private class CommunicationItem
        {
            public EventWaitHandle WaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
            public byte[] Buffer = new byte[PlcTcpClient._RECV_BUFFER_SIZE];

            public Socket Client { get; private set; }

            public byte[] RecvBuffer { get; set; }

            public CommunicationItem(Socket client)
            {
                this.Client = client;
                this.WaitHandle.Reset();
            }

            public static implicit operator byte[] (CommunicationItem item)
            {
                return item.Buffer;
            }
        }

        private const ulong _RECV_BUFFER_SIZE = ushort.MaxValue + 9;

        private TcpClient _tcpClient = null;
        //private System.Timers.Timer _trReconnect = null;
        private BackgroundTimer _trReconnect = null;
        private readonly object _lockRequest = new object();

        /// <summary>
        /// 연결 알림 이벤트
        /// </summary>
        public event EventHandler OnConnectedEvent;

        /// <summary>
        /// 연결 해제 이벤트
        /// </summary>
        public event EventHandler OnDisconnectedEvent;

        /// <summary>
        /// 통신 소켓
        /// </summary>
        private Socket _Client
        {
            get
            {
                if (this._tcpClient == null) return null;

                return this._tcpClient.Client;
            }
        }

        /// <summary>
        /// IP 주소
        /// </summary>
        public string IpAddress { get; private set; }

        /// <summary>
        /// 포트 번호
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// 연결 여부
        /// </summary>
        public bool Connected
        {
            get
            {
                if (this._tcpClient == null) return false;
                return this._tcpClient.Client.Connected;
            }
        }

        private PlcTcpClient()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="ipAddress">IP 주소</param>
        /// <param name="port">포트 번호</param>
        public PlcTcpClient(string ipAddress, int port) : this()
        {
            this.IpAddress = ipAddress;
            this.Port = port;
        }

        #region 함수

        /// <summary>
        /// 통신 연결 정보 수정
        ///  - 기존에 연결된 통신은 중지
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        public void ConfigChange(string ipAddress, int port)
        {
            if (this.IpAddress == ipAddress && this.Port == port) return;

            this.InitializeComponent();

            this.IpAddress = ipAddress;
            this.Port = port;
        }

        /// <summary>
        /// 통신 연결 시작
        /// </summary>
        public void Start()
        {
            if (this._tcpClient != null) return;
            this.Connect(this.IpAddress, this.Port);
        }

        /// <summary>
        /// 통신 연결 중지
        /// </summary>
        public void Stop()
        {
            this._trReconnect.Stop();
            this.Close();
        }

        /// <summary>
        /// 통신 요청
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="timeOutSec"></param>
        /// <returns></returns>
        public Response<byte[]> Request(byte[] buffer, int timeOutSec = 1)
        {
            try
            {
                if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                lock (this._lockRequest)
                {
                    if (this.Connected == false) throw new Exception("연결 중지 상태 입니다.");

                    var timeOut = timeOutSec * 1000;
                    var item = this.Send(buffer, timeOut);
                    
                    if (item.WaitHandle.WaitOne(timeOut) == false) throw new TimeoutException("통신 타임아웃이 발생하였습니다. [Receive]");

                    return new Response<byte[]>(item.RecvBuffer);
                }
            }
            catch (TimeoutException ex)
            {
                this.Close();
                return new Response<byte[]>(ex);
            }
            catch (SocketException ex)
            {
                this.Close();
                return new Response<byte[]>(ex);
            }
            catch (Exception ex)
            {
                return new Response<byte[]>(ex);
            }
        }

        #region private

        private void InitializeComponent()
        {
            this.IpAddress = string.Empty;
            this.Port = 0;

            if(this._trReconnect == null)
            {
                this._trReconnect = this._trReconnect ?? new BackgroundTimer();
                this._trReconnect.SleepTimeMsc = 5000;
                this._trReconnect.DoWork += _trReconnect_DoWork;
            }
            
            this._trReconnect.Stop();

            this.Close();
        }

        private void Close()
        {
            try
            {
                if (this._tcpClient == null) return;
                this._tcpClient.Close();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            finally
            {
                this._tcpClient = null;
                this.OnDisconnectedEvent?.Invoke(this, new EventArgs());
            }
        }

        private CommunicationItem Send(byte[] buffer, int timeOut = 1000)
        {
            if (this.Connected == false) throw new Exception("통신 연결 상태가 아닙니다.");

            var item = new CommunicationItem(this._Client);
            this._Client.BeginReceive(item.Buffer, 0, item.Buffer.Length, SocketFlags.None, this.Receive_AsyncCallback, item);

            var result = this._Client.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, this.Send_AsyncCallback, new KeyValuePair<Socket, byte[]>(this._Client, buffer));
            if (result.AsyncWaitHandle.WaitOne(timeOut) == false) throw new TimeoutException("통신 타임아웃이 발생하였습니다. [Send]");

            return item;
        }
        
        private void Connect(string ipAddress, int port)
        {
            if (this._tcpClient != null) this.Close();

            this._tcpClient = new TcpClient();
            this._tcpClient.BeginConnect(this.IpAddress, this.Port, this.Connect_AsyncCallback, this._tcpClient);
        }

        private void _trReconnect_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                if (this.Connected) return;

                this.Connect(this.IpAddress, this.Port);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Connect_AsyncCallback(IAsyncResult result)
        {
            var tcpClient = (TcpClient)result.AsyncState;

            try
            {
                tcpClient.EndConnect(result);

                var keepAlive = new TcpKeepAlive()
                {
                    OnOff = 1,
                    KeepAliveTime = 3000,
                    KeepAliveInterval = 1000,
                };

                tcpClient.Client.IOControl(IOControlCode.KeepAliveValues, keepAlive.GetBytes(), null);

                // 연결 이벤트 푸시
                this.OnConnectedEvent?.Invoke(this, new EventArgs());
            }
            catch (Exception ex)
            {
                this.Close();
                Logger.Write(this, ex);
            }
            finally
            {
                // 재연결 작업 타이머 가동.
                if (this._trReconnect.IsBusy == false) this._trReconnect.Start();
            }
        }

        private void Send_AsyncCallback(IAsyncResult result)
        {
            var item = (KeyValuePair<Socket, byte[]>)result.AsyncState;
            var client = item.Key;
            var buffer = item.Value;

            try
            {
                var length = client.EndSend(result);
                if (length == buffer.Length) return;

                // 정상적으로 데이터 송신 하지 못한 경우 나머지 데이터 재전송.
                var sendBuffer = buffer.Skip(length).ToArray();
                this.Send(sendBuffer);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Receive_AsyncCallback(IAsyncResult result)
        {
            var item = result.AsyncState as CommunicationItem;
            var buffer = (byte[])item;

            try
            {
                var length = item.Client.EndReceive(result);
                if (length == 0)
                {
                   item.Client.Disconnect(true);
                    return;
                }

                item.RecvBuffer = buffer.Take(length).ToArray();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                item.WaitHandle.Set();
            }
        }

        #endregion

        #endregion
    }
}
