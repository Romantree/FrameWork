using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TS.FW.Net;

namespace TS.FW.Bcr.Keyence
{
    public class KeyenceBcrClient
    {
        public bool UnicodeString { get; set; }

        public event EventHandler<string> OnDspMessage;

        private void DpsMessage(bool isSend, byte[] buffer)
        {
            try
            {
                var msg = string.Format("{0} >> {1}", isSend ? "TX" : "RX", string.Join(" ", buffer.Select(t => this.ToHex(t, 2))));

                this.OnDspMessage?.Invoke(this, msg);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private string ToHex(int value, int len)
        {
            return Convert.ToString(value, 16).PadLeft(len, '0').ToUpper();
        }

        IPEndPoint _ipEndPoint;
        TcpClient _socket;
        Thread thReceive;
        Thread thConnect;
        //Thread thCheckConnection;
        NetworkStream _readStream;
        NetworkStream _writeStream;
        object _objlock = new object();

        bool _bTerminate = false;
        public bool IsConnect { get => _socket == null ? false : _socket.Connected; }

        public event EventHandler<string> OnReceiveMsgEvent;
        public void DspMessageEvent(string msg)
        {
            this.OnReceiveMsgEvent?.Invoke(this, msg);
        }
        public event EventHandler<string> OnConnectEvent;
        public void ConnectEvent(string msg)
        {
            this.OnConnectEvent?.Invoke(this, msg);
        }
        public event EventHandler<string> OnDisconnectEvent;
        public void DisConnectEvent(string msg)
        {
            this.OnDisconnectEvent?.Invoke(this, msg);
        }

        public KeyenceBcrClient()
        {
        }

        private bool _serverConnected = false;
        public bool IsConnected => _serverConnected;

        private void FuncRecieve()
        {
            string ReceiveData = null;
            string sPrevMsg = "";
            while (_bTerminate == false)
            {
                Thread.Sleep(30);
                try
                {
                    if (_socket == null) continue;

                    if (_serverConnected)
                    {
                        if (_socket.Available > 0)
                        {
                            Byte[] ReceiveByte = new Byte[8192];
                            int nValue = _readStream.Read(ReceiveByte, 0, ReceiveByte.Length);
                            _readStream.Flush();
                            if (nValue > 0)
                            {
                                if (UnicodeString) 
                                    ReceiveData = Encoding.Unicode.GetString(ReceiveByte);
                                else //Ascii Code 형태 메시지
                                    ReceiveData = Encoding.ASCII.GetString(ReceiveByte);

                                DspMessageEvent(ReceiveData);
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    if (sPrevMsg != ex.Message)
                    {
                        sPrevMsg = ex.Message;
                        DspMessageEvent(ex.Message);
                    }
                }
            }
        }

        void FuncConnect()
        {
            string sPrevMsg = "";
            while (_bTerminate == false)
            {
                Thread.Sleep(500);
                try
                {
                    if (_serverConnected == false)
                    {
                        _socket.Close();
                        _socket = new TcpClient();
                        _socket.Connect(_ipEndPoint);
                        _socket.NoDelay = true;
                        _readStream = _socket.GetStream();
                        _writeStream = _socket.GetStream();

                        if (thReceive != null)
                        {
                            if (thReceive.IsAlive)
                            {
                                thReceive.Abort();
                                Thread.Sleep(100);
                            }
                        }
                        thReceive = new Thread(new ThreadStart(FuncRecieve));
                        thReceive.IsBackground = true;
                        thReceive.Start();

                        _serverConnected = true;
                        ConnectEvent("Connect success");
                        break; //While문을 빠져 나가라
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message != sPrevMsg)
                    {
                        sPrevMsg = ex.Message;
                        DspMessageEvent(ex.Message);
                    }
                }
            }
        }
        public bool Connect(string ServerIPAddress, int nPort)
        {
            try
            {
                if (_ipEndPoint != null)
                    return false;

                var ip = System.Net.IPAddress.Parse(ServerIPAddress);

                thConnect = new Thread(new ThreadStart(FuncConnect));
                _ipEndPoint = new IPEndPoint(ip, nPort);
                _socket = new TcpClient();

                _bTerminate = false;

                thConnect.IsBackground = true;
                thConnect.Start();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0}", ex.Message));
            }
        }
        public void DisConnect()
        {
            _ipEndPoint = null;
            _bTerminate = true;

            Thread.Sleep(10);

            _serverConnected = false;

            if (_socket != null)
            {
                _socket.Close();
            }

            if (thReceive != null)
                thReceive.Abort();
            if (thConnect != null)
                thConnect.Abort();

            DisConnectEvent("DisConnect success");
        }

        public IEnumerable<byte> ToCommand(string cmd, bool dspmsg = false)
        {
            var b = new ByteHelper();
            b.Append(cmd);
            b.Append('\r');

            //switch (cmd)
            //{
            //    case "LON": //StartReadId
            //        break;
                   
            //    case "LOFF": //EndReadId
            //        break;
                    
            //    case "RESET": //Bcr Reset
            //        break;
                    
            //    case "BCLR"://Buffer Clear
            //        break;
            //    default:
            //        break;
            //}

            return b.ToByte();
        }

        public void SendConmmand(string SendData, bool dspMsg = false)
        {
            if (_socket == null)
                return;
            try
            {
                lock (_objlock)
                {
                    if (_socket.Connected)
                    {
                        var command = ToCommand(SendData, dspMsg).ToArray();
                        if (dspMsg) DpsMessage(true, command);

                        _writeStream.Flush();
                        _writeStream.Write(command, 0, SendData.Length);

                        //todo : 데이터 보내고 나서 들어오는거 확인 필요. 
                    }
                }
            }
            catch (Exception)
            {

            }

        }
        public void SendConmmand(byte[] SendData, bool dspMsg = false)
        {
            if (_socket == null)
                return;
            try
            {
                lock (_objlock)
                {
                    if (_socket.Connected)
                    {
                        if(dspMsg) DpsMessage(true, SendData);

                        _writeStream.Flush();
                        _writeStream.Write(SendData, 0, SendData.Length);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

    }
}
