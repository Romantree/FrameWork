using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Comm
{
    public abstract class TcpClientEx
    {
        private System.Net.Sockets.TcpClient _client = new System.Net.Sockets.TcpClient();
        private readonly bool _isCallback = false;

        private NetworkStream _stream;
        private byte[] _recvBuffer;

        public event EventHandler<byte[]> OnRecvDataEvent;

        //public bool Connected =>  this._client.Connected;
        public bool Connected
        {
            get
            {
                if (this._client == null) return false;
                return this._client.Connected;
            }
        }
        public virtual Encoding Encoding => Encoding.ASCII;

        public TcpClientEx(bool isCallback = true)
        {
            this._isCallback = isCallback;
        }

        public virtual bool Connect(string ipAdd, int port, int buffersize = 4096, int timeout = 1000)
        {
            if (ipAdd == null || ipAdd.Length == 0) return false;

            var ip = IPAddress.Parse(ipAdd);

            this._client = this._client ?? new System.Net.Sockets.TcpClient();

            this._client.ReceiveBufferSize = buffersize;
            this._client.ReceiveTimeout = timeout;

            var result = this._client.BeginConnect(ipAdd, port, this.ConnectCallback, null);

            if (result.AsyncWaitHandle.WaitOne(timeout) == false)
            {
                this._client.EndConnect(result);
                return false;
            }

            //_stream = new NetworkStream(this._client.Client); //todo : 확인
            return true;
        }

        public void Disconnect()
        {
            if (this.Connected == false) return;

            try
            {
                if (_stream != null)
                {
                    this._stream.Close();
                    this._stream.Dispose();
                }
            }
            finally
            {
                this._client.Close();
                this._client = null;
            }
        }

        public bool Send(byte[] buffer, int timeoutMs = 1000)
        {
            try
            {
                if (this.Connected == false) return false;

                var result = this._stream.BeginWrite(buffer, 0, buffer.Length, WriteCallback, buffer);

                if (result.AsyncWaitHandle.WaitOne(timeoutMs) == false)
                {
                    this._stream.EndWrite(result);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return false;
            }
        }

        public byte[] Recv()
        {
            if (this._isCallback == true) return new byte[0];

            var buffer = new byte[this._client.ReceiveBufferSize];

            var len = this._stream.Read(buffer, 0, buffer.Length);
            if (len == 0) return new byte[0];

            return buffer.Take(len).ToArray();
        }

        private void ConnectCallback(IAsyncResult result)
        {
            try
            {
                //if (result.IsCompleted) return;

                this._client.EndConnect(result);

                this._stream = this._client.GetStream();

                if (this._isCallback == false) return;

                this._recvBuffer = new byte[this._client.ReceiveBufferSize];
                this._stream.BeginRead(this._recvBuffer, 0, this._recvBuffer.Length, ReadCallback, null);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void WriteCallback(IAsyncResult result)
        {
            try
            {
                //if (result.IsCompleted) return;

                this._stream.EndWrite(result);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void ReadCallback(IAsyncResult result)
        {
            try
            {
                //if (result.IsCompleted) return;

                var len = this._stream.EndRead(result);
                if (len == 0) //연결이 끊어진 상태1
                {
                    this.Disconnect();
                    return;
                }

                this.RecvDataCallback(len);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void RecvDataCallback(int length)
        {
            try
            {
                var buffer = this._recvBuffer.Take(length).ToArray();
                this.OnRecvDataEvent?.Invoke(this, buffer);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                Array.Clear(this._recvBuffer, 0, this._recvBuffer.Length);
                this._stream.BeginRead(this._recvBuffer, 0, this._recvBuffer.Length, ReadCallback, null);
            }
        }
        protected string ToHex(int value, int len)
        {
            return Convert.ToString(value, 16).PadLeft(len, '0').ToUpper();
        }
    }
}
