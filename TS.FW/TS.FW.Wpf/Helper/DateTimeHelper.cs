using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Diagnostics;
using TS.FW.Wpf.Core;

namespace TS.FW.Wpf.Helper
{
    public class DateTimeHelper : ModelBase
    {
        public static DateTimeHelper Ins { get; private set; }

        static DateTimeHelper()
        {
            Ins = new DateTimeHelper();
        }

        private readonly BackgroundTimer _trUpdate = new BackgroundTimer();
        private readonly Stopwatch _stopWatch = new Stopwatch();

        public string NowString => DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        public string NowDateString => DateTime.Now.ToString("yyyy/MM/dd");

        public string NowTimeString => DateTime.Now.ToString("HH:mm:ss");

        public long WatchMsec => this._stopWatch.ElapsedMilliseconds;

        public double WatchSec => this._stopWatch.ElapsedMilliseconds / 1000D;

        private DateTimeHelper()
        {
            this._trUpdate.SleepTimeMsc = 100;
            this._trUpdate.DoWork += _trUpdate_DoWork;
            this._trUpdate.Start();
        }

        public void StartWatch()
        {
            this._stopWatch.Restart();
        }

        public void StopWatch()
        {
            this._stopWatch.Stop();
        }

        private void _trUpdate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                this.OnNotifyPropertyChanged(nameof(this.WatchMsec));
                this.OnNotifyPropertyChanged(nameof(this.WatchSec));

                this.OnNotifyPropertyChanged(nameof(this.NowTimeString));
                this.OnNotifyPropertyChanged(nameof(this.NowDateString));
                this.OnNotifyPropertyChanged(nameof(this.NowString));
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
