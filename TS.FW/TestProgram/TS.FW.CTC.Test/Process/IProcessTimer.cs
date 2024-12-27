using System;
using System.Diagnostics;
using TS.FW.Diagnostics;
using TS.FW.Helper;

namespace TS.FW.CTC.Test.Process
{
    public abstract class IProcessTimer : BackgroundTimer
    {
        private const int SLEEP_TIME = 10;

        public static event EventHandler<string> OnProcessMessageChangedEvent;

        private string _processMessage = null;
        protected Stopwatch _watch = new Stopwatch();

        public string ProcessMessage => this._processMessage;

        public abstract string Name { get; }

        public IProcessTimer() : base(System.Threading.ApartmentState.MTA)
        {
            this.SleepTimeMsc = SLEEP_TIME;
            this.DoWork += IProcessTimer_DoWork;
        }

        public abstract void DoWork_Porcess();

        private void IProcessTimer_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                this.DoWork_Porcess();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public virtual void SetProcessMessage(string format, params object[] arg)
        {
            this.SetProcessMessage(string.Format(format, arg));
        }

        public virtual void SetProcessMessage(string message, bool islog = true)
        {
            try
            {
                if (this._processMessage == message) return;

                var msg = string.Format("[{0}] {1}", this.Name, message);

                if (islog == true)
                {
                    Logger.CustomWrite(this.GetType().Name, this, msg);
                }

                OnProcessMessageChangedEvent?.Invoke(this, msg);
            }
            finally
            {
                this._processMessage = message;
            }
        }

        protected void TimerStart()
        {
            this._watch.Restart();
        }

        protected bool TimeOutCheck(int timeMsc)
        {
            return this._watch.TimeOutCheck(timeMsc);
        }

        protected bool TimeOutCheck(double timeSec)
        {
            return this._watch.TimeOutCheck(Convert.ToInt32(timeSec * 1000));
        }
    }
}