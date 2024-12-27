using System;
using TS.FW.Diagnostics;
using TS.FW.Wpf.v2.Core;

namespace TS.FW.Wpf.v2.Helpers
{
    public class DateTimeHelper : IModel
    {
        public static DateTimeHelper Ins { get; private set; }

        static DateTimeHelper() => Ins = new DateTimeHelper();

        private readonly BackgroundTimer timer = new BackgroundTimer(System.Threading.ApartmentState.MTA);

        public DateTime Now => DateTime.Now;

        private DateTimeHelper()
        {
            timer.SleepTimeMsc = 100;
            timer.DoWork += Timer_DoWork;
            timer.Start();
        }

        private void Timer_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.OnPropertyChanged(nameof(Now));
        }
    }
}
