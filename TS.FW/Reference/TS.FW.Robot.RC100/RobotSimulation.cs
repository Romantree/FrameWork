using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Robot.RC100.State;

namespace TS.FW.Robot.RC100
{
    public class RobotSimulation
    {
        private readonly Encoding _encoding = Encoding.ASCII;

        private readonly RobotState State = new RobotState() { RemoteMode = true, RobotReady = true, CmdReady = true, Alarm = true, ProgramRun = true, ArmFoldR1 = true, ArmFoldR2 = true, ServoON = true };
        private readonly RobotPosition Pos = new RobotPosition();

        private TcpListener _listener = null;
        private TcpClient _client = null;

        public void Open(string ip, int port)
        {
            var ipaddress = IPAddress.Parse(ip);

            this._listener = new TcpListener(ipaddress, port);
            this._listener.Start();
            this._listener.BeginAcceptTcpClient(AcceptCallback, null);
        }

        private void AcceptCallback(IAsyncResult result)
        {
            try
            {
                if (this._client != null) this._client.Close();

                this._client = this._listener.EndAcceptTcpClient(result);
                if (this._client == null) return;

                var buffer = new byte[1024];
                this._client.GetStream().BeginRead(buffer, 0, buffer.Length, ReadCallback, buffer);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                this._listener.BeginAcceptTcpClient(AcceptCallback, null);
            }
        }

        private void ReadCallback(IAsyncResult result)
        {
            try
            {
                if (this._client == null) return;

                var len = this._client.GetStream().EndRead(result);
                if (len <= 0)
                {
                    this._client.Close();
                    return;
                }

                var buffer = result.AsyncState as byte[];
                var msg = _encoding.GetString(buffer, 0, len);

                this.CommandWork(msg);

                Array.Clear(buffer, 0, len);
                this._client.GetStream().BeginRead(buffer, 0, buffer.Length, ReadCallback, buffer);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void CommandWork(string buffer)
        {
            try
            {
                var msg = buffer.Replace("\r\n", "");
                var token = msg.Split(new string[] { "," }, StringSplitOptions.None);

                var cmd = (RobotCmdType)Enum.Parse(typeof(RobotCmdType), token[0]);

                if (cmd == RobotCmdType.STATE)
                {
                    this.Send(string.Format("*{0},{1}", cmd, this.State.GetData()));

                    this.Send(string.Format(">{0}", cmd));
                }
                else if (cmd == RobotCmdType.GETCPOS)
                {
                    this.Send(string.Format("-{0}", cmd));

                    this.Send(string.Format("*{0},{1}", cmd, this.Pos.GetData()));

                    this.Send(string.Format(">{0}", cmd));
                }
                else if (cmd == RobotCmdType.INITIAL)
                {
                    //this.Send("*Err:1");

                    this.Send(string.Format("-{0}", cmd));

                    this.INITIAL();
                }
                else if (cmd == RobotCmdType.MAPSCAN)
                {
                    this.Send(string.Format("-{0}", cmd));

                    this.MAPSCAN();
                }
                else
                {
                    this.Send(string.Format("-{0}", cmd));

                    this.Send(string.Format(">{0}", cmd));
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private Random _ran = new Random((int)DateTime.Now.Ticks);

        private async void MAPSCAN()
        {
            this.State.ProgramRun = true;
            this.State.IsBusy = true;

            await Task.Delay(1000);

            this.Pos.X = 100;
            this.Pos.T = -45;
            this.Pos.Z = 5;
            this.Pos.R1 = 10;
            this.Pos.R2 = 15;

            await Task.Delay(1000);

            for (int i = 0; i < 25; i++)
            {
                this.Pos.Z++;

                await Task.Delay(10);
            }

            await Task.Delay(1000);

            var list = new List<int>();

            for (int i = 0; i < 25; i++)
            {
                //list.Add(_ran.Next(0, 1000) % 2 == 0 ? 0 : 1);
                //list.Add(i < 10 ? 1 : 0); //임시로 3장만 테스트             
                list.Add(0);
            }

            this.Send(string.Format("*{0},11,{1}", RobotCmdType.MAPSCAN, string.Join(",", list)));

            await Task.Delay(1000);

            //this.State.ProgramRun = false;
            this.State.IsBusy = false;

            this.Send(string.Format(">{0}", RobotCmdType.MAPSCAN));
        }

        private async void INITIAL()
        {
            this.State.ProgramRun = true;

            this.State.ServoON = true;
            this.State.Alarm = false;

            await Task.Delay(1000);

            this.Pos.X = 0;
            this.Pos.T = 0;
            this.Pos.Z = 0;
            this.Pos.R1 = 0;
            this.Pos.R1 = 0;

            await Task.Delay(1000);

            this.State.RemoteMode = true;
            this.State.RobotReady = true;

            this.State.ArmFoldR1 = true;
            this.State.ArmFoldR2 = true;

            this.State.CmdReady = true;
            //this.State.ProgramRun = false;

            this.Send(string.Format("> {0}", RobotCmdType.INITIAL));
        }

        private void Send(string msg)
        {
            var buffer = this._encoding.GetBytes(msg + "\r\n");
            this._client.GetStream().Write(buffer, 0, buffer.Length);
        }
    }
}
