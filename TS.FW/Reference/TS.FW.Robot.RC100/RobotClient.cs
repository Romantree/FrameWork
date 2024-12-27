using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Diagnostics;
using TS.FW.Robot.RC100.Data;
using TS.FW.Robot.RC100.State;

namespace TS.FW.Robot.RC100
{
    public partial class RobotClient
    {
        private readonly Encoding _encoding = Encoding.ASCII;
        private readonly BackgroundTimer _trUpdateState = new BackgroundTimer();
        private readonly List<RobotCmdData> _cmdDataList = null;

        private readonly object _locker = new object();

        private TcpClient _tcpClient = null;
        private string _theRest = string.Empty;
        private MappingResult _mappingResult = null;
        private WaferThicknessResult _waferThicknessResult = null;
        private RobotCmdData _curData = null;

        public event EventHandler<string> OnDspMsgEvent;

        public bool Connected => this._tcpClient != null ? this._tcpClient.Connected : false;

        private NetworkStream _stream => this.Connected ? this._tcpClient.GetStream() : null;

        public RobotState State { get; private set; } = new RobotState();

        public RobotPosition Position { get; private set; } = new RobotPosition();

        public RobotClient()
        {
            this._trUpdateState.SleepTimeMsc = 100;
            this._trUpdateState.DoWork += _trUpdateState_DoWork;

            this._cmdDataList = RobotCmdData.ToRobotCmdData().ToList();
        }

        public bool Open(string ip, int port, int timeOut = 1000)
        {
            try
            {
                if (this.Connected == true) return false;

                this._tcpClient = new TcpClient()
                {
                    ReceiveBufferSize = 1024,
                    SendBufferSize = 1024,
                };

                var ipaddress = IPAddress.Parse(ip);

                var res = this._tcpClient.BeginConnect(ipaddress, port, ConnectCallback, null);
                if (res.AsyncWaitHandle.WaitOne(timeOut) == false) return false;

                return this._tcpClient.Connected;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);

                return false;
            }
        }

        public void Close()
        {
            try
            {
                if (this._tcpClient == null) return;

                this._stream.Close();

                this._tcpClient.Close();
                this._tcpClient = null;

                this._cmdDataList.ForEach(t =>
                {
                    t.IsEnd = true;
                    t.Set();
                });
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                this._trUpdateState.Stop();
            }
        }

        public bool IsComplete(RobotCmdType cmd)
        {
            var item = this._cmdDataList.FirstOrDefault(t => t.Cmd == cmd);
            if (item == null) return false;

            return item.IsEnd;
        }

        public RobotError GetError(RobotCmdType cmd)
        {
            var item = this._cmdDataList.FirstOrDefault(t => t.Cmd == cmd);
            if (item == null) return RobotError.NONE;

            return item.Error;
        }

        private void _trUpdateState_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (this.Connected == false) return;

                this.GetState();
                //this.GetCurrentPos();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void ConnectCallback(IAsyncResult result)
        {
            try
            {
                this._tcpClient.EndConnect(result);

                if (this.Connected == false) return;

                var buffer = new byte[1024];
                this._stream.BeginRead(buffer, 0, buffer.Length, ReadCallback, buffer);

                this._trUpdateState.Start();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void ReadCallback(IAsyncResult result)
        {
            var buffer = result.AsyncState as byte[];

            try
            {
                if (this.Connected == false) return;

                var len = this._stream.EndRead(result);

                if (len <= 0)
                {
                    this.Close();
                    return;
                }

                var msg = this._encoding.GetString(buffer, 0, len);
                var list = this.ToRestMsg(msg, ref this._theRest);

                if (list.Count <= 0) return;

                foreach (var item in list)
                {
                    this.CommandWork(item);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                Array.Clear(buffer, 0, buffer.Length);

                if (this.Connected == true)
                    this._stream.BeginRead(buffer, 0, buffer.Length, ReadCallback, buffer);
            }
        }

        private void CommandWork(string msg)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(msg)) return;

                this.OnDspMsgEvent?.Invoke(this, string.Format("RECV => {0}", msg));

                this.LogWrite("RECV : {0}", msg);

                if (msg.Contains("ERR"))
                {
                    this.LogWriteError(msg);

                    //if (this._curData != null) this._curData.SerError(msg);
                    this._cmdDataList.ForEach(t => t.SerError(msg));
                    return;
                }

                var cmd = this.ToRobotCmdType(msg);
                if (Enum.IsDefined(typeof(RobotCmdType), cmd) == false) return;

                var data = this._cmdDataList.FirstOrDefault(t => t.Cmd == cmd);
                if (data == null) return;

                switch (cmd)
                {
                    case RobotCmdType.STATE:
                    case RobotCmdType.GETCPOS:
                    case RobotCmdType.GETLPOS:
                    case RobotCmdType.GETLPAR:
                    case RobotCmdType.GETTSPD:
                    case RobotCmdType.GETMPOS:
                    case RobotCmdType.GETMPAR:
                        {
                            if (msg.Contains("*"))
                            {
                                data.SetData(msg);
                                data.Set();
                            }
                        }
                        break;
                    case RobotCmdType.MAPSCAN:
                        {
                            if (msg.Contains("-"))
                            {
                                data.Set();
                            }
                            else if (msg.Contains("*") && this._mappingResult != null)
                            {
                                data.SetData(msg);

                                var tt = data.DataMsg.Substring(4, data.DataMsg.Length - 4); //앞에 두자리의 데이터는 Port정보와 Wafer
                                this._mappingResult.Wafers = tt.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(t => (WaferState)int.Parse(t)).ToList();
                                this._mappingResult.Complete = true;
                            }
                        }
                        break;
                    case RobotCmdType.GETMDAT:
                        {
                            if (msg.Contains("-"))
                            {
                                data.Set();
                            }
                            else if (msg.Contains("*") && this._waferThicknessResult != null)
                            {
                                data.SetData(msg);

                                this._waferThicknessResult.WaferThickness = data.DataMsg.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(t => int.Parse(t)).ToList();
                                this._waferThicknessResult.Complete = true;
                            }
                        }
                        break;
                    default:
                        {
                            if (msg.Contains("-"))
                            {
                                data.Set();
                            }
                        }
                        break;
                }

                if (msg.Contains(">"))
                {
                    data.IsEnd = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private RobotCmdType ToRobotCmdType(string msg)
        {
            if (msg.Contains("*"))
            {
                var index = msg.IndexOf(",");
                var value = msg.Substring(0, index).Replace("*", "").Trim();

                if (Enum.TryParse(value, out RobotCmdType cmd)) return cmd;
            }
            else
            {
                var value = msg.Replace("-", "").Replace(">", "").Trim();

                if (Enum.TryParse(value, out RobotCmdType cmd)) return cmd;
            }

            return (RobotCmdType)(-1);
        }

        private void GetState()
        {
            try
            {
                if (this.Connected == false) return;

                lock (this._locker)
                {
                    var cmd = this.Send(RobotCmdType.STATE);
                    if (cmd == null) return;

                    if (cmd.Error != RobotError.NONE)
                    {
                        this.LogWriteError("상태 모니터링 ERROR : {0}", cmd.Error);
                        return;
                    }

                    this.State.SetData(cmd.DataMsg);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void GetCurrentPos()
        {
            try
            {
                if (this.Connected == false) return;
                if (this.State.StateCheck(RobotStateCheck.Alarm | RobotStateCheck.CmdReady)) return;

                lock (this._locker)
                {
                    var cmd = this.Send(RobotCmdType.GETCPOS);
                    if (cmd == null) return;

                    if (cmd.Error != RobotError.NONE)
                    {
                        this.LogWriteError("로봇 현재 위치 반환 ERROR : {0}", cmd.Error);
                        return;
                    }

                    this.Position.SetData(cmd.DataMsg);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private RobotCmdData Send(RobotCmdType cmd, params object[] arg)
        {
            var data = this._cmdDataList.FirstOrDefault(t => t.Cmd == cmd);
            if (data == null || data.IsEnd == false) return null;

            var list = new List<object>();
            list.Add(cmd);
            list.AddRange(arg);

            var msg = string.Join(",", list);

            this.LogWrite("SEND : {0}", msg);

            return this.Send(msg, data);
        }

        private RobotCmdData Send(string msg, RobotCmdData data, int timeOut = 1000)
        {
            try
            {
                if (data.IsEnd == false) return null;

                data.Reset();
                this._curData = data;

                this.OnDspMsgEvent?.Invoke(this, string.Format("SEND => {0}", msg));

                var buffer = this._encoding.GetBytes(string.Format("{0}\r\n", msg));
                this._stream.Write(buffer, 0, buffer.Length);

                //if (this._curData.WaitOne(timeOut) == false) throw new TimeoutException(string.Format("{0}", data.Cmd));
                if (this._curData.WaitOne() == false) throw new TimeoutException(string.Format("{0}", data.Cmd));

                return data;
            }
            catch (Exception ex)
            {
                this._curData = null;
                throw ex;
            }
        }

        private List<string> ToRestMsg(string msg, ref string theRest)
        {
            var item = theRest + msg;
            if (string.IsNullOrWhiteSpace(item) == true) return new List<string>();

            var list = item.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            if (list.Length <= 0) return new List<string>();

            theRest = list[list.Length - 1];

            return list.Take(list.Length - 1).ToList();
        }

        private void LogWrite(string format, params object[] arg)
        {
            this.LogWrite(string.Format(format, arg));
        }

        private void LogWriteError(string format, params object[] arg)
        {
            this.LogWrite(string.Format(format, arg), Logger.LogEventLevel.Error);
        }

        private void LogWrite(string msg, Logger.LogEventLevel level = Logger.LogEventLevel.Information)
        {
            Logger.CustomWrite("RobotClient", this, msg, level);
        }
    }
}