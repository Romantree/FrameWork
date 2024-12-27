using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Diagnostics;
using TS.FW.Net.Serial;

namespace TS.FW.Net.Probe
{
    public class KeyenceClient : ISerialClient
    {
        private readonly object _locker = new object();
        private readonly BackgroundTimer _trUpdate = new BackgroundTimer();
        private readonly Dictionary<int, double> _dataList = new Dictionary<int, double>();

        public double this[int ch] => this.GetData(ch);

        public override int DeleyTime => 100;

        public KeyenceClient()
        {
            this._trUpdate.SleepTimeMsc = 100;
            this._trUpdate.DoWork += _trUpdate_DoWork;
            this._trUpdate.Start();
        }

        public bool CheckSafetyPosition => this.GetCheckSafetyPosition();

        private bool GetCheckSafetyPosition()
        {
            return (this[0] > 12) && (this[1] > 12);
        }

        public override void Close()
        {
            try
            {
                base.Close();

                this._trUpdate.Stop();
            }
            catch (Exception)
            {
            }
        }

        public double GetData(int ch)
        {
            lock (this._locker)
            {
                if (this._dataList.ContainsKey(ch) == false) return 0;

                return this._dataList[ch];
            }
        }

        private void _trUpdate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                if (this.IsSimulation == false && this.Connected == false) return;

                this.AllReadSensorData();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void AllReadSensorData()
        {
            try
            {
                if (this.IsSimulation) return;

                var buffer = this.M0_CMD().ToArray();

                this.SendData(buffer);

                var recv = this.ReadData();
                if (recv == null) return;

                this.CommandParser(recv);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void CommandParser(byte[] buffer)
        {
            try
            {
                var data = this.Encoding.GetString(buffer);

                foreach (var msg in data.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var cmd = msg.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (cmd.Length < 3) return;

                    switch (cmd[0])
                    {
                        case "ER":
                            {
                                Logger.Write(this, msg, Logger.LogEventLevel.Error);
                            }
                            break;
                        case "M0":
                            {
                                foreach (var item in cmd.Skip(1).Select((t, Ch) => new { Ch, Data = Convert.ToDouble(t) }))
                                {
                                    this.InsertData(item.Ch, item.Data);
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void InsertData(int ch, double data)
        {
            lock (this._locker)
            {
                if (this._dataList.ContainsKey(ch) == false) this._dataList.Add(ch, 0);

                this._dataList[ch] = data;
            }
        }

        private IEnumerable<byte> M0_CMD()
        {
            foreach (var item in this.Encoding.GetBytes("M0\r\n"))
            {
                yield return item;
            }
        }
    }
}
