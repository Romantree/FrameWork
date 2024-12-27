using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TS.FW.Diagnostics
{
    public class BackgroundTimer
    {
        private readonly static List<BackgroundTimer> LIST = new List<BackgroundTimer>();

        private readonly ApartmentState ApartmentState;

        private Thread _bg = null;
        private bool _IsPause = false;

        public bool IsCancel { get; private set; } = false;

        public event EventHandler<DoWorkEventArgs> DoWork;

        public int SleepTimeMsc { get; set; } = 100;

        public bool IsBusy => this._bg == null ? false : this._bg.IsAlive;

        public bool IsPause
        {
            get
            {
                if (this.IsBusy == false) return false;

                return this._IsPause;
            }
            protected set
            {
                this._IsPause = value;
            }
        }

        public BackgroundTimer(ApartmentState apartmentState = ApartmentState.Unknown)
        {
            this.ApartmentState = apartmentState;
            LIST.Add(this);
        }

        public void Join()
        {
            if (this._bg == null) return;

            this._bg.Join();
        }

        public virtual void Start()
        {
            try
            {
                if (this._bg != null && this.IsBusy) return;

                this.IsCancel = false;

                this._bg = new Thread(this.Background_DoWork);
                this._bg.IsBackground = false;
                this._bg.Priority = ThreadPriority.Normal;
                this._bg.SetApartmentState(this.ApartmentState);
                this._bg.Start();
            }
            finally
            {
                this.IsPause = false;
            }
        }

        public virtual void Stop()
        {
            if (this._bg == null || this.IsCancel) return;

            this.Pause();

            this.IsCancel = true;
        }

        public virtual void Stop(bool isAbort = false)
        {
            if (this._bg == null || this.IsCancel) return;

            this.Pause();

            if (isAbort == true)
                this._bg.Abort();

            this.IsCancel = true;
        }

        public virtual void Resume()
        {
            this.IsPause = false;
        }

        public virtual void Pause()
        {
            this.IsPause = true;
        }

        private void Background_DoWork()
        {
            try
            {
                while (this.IsCancel == false)
                {
                    try
                    {
                        this.DoWork?.Invoke(this, new DoWorkEventArgs(null));
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(this, ex);
                    }
                    finally
                    {
                        if (this.SleepTimeMsc >= 5 * 1000)
                        {
                            var count = this.SleepTimeMsc / 100;

                            for (int i = 0; i < count; i++)
                            {
                                if (this.IsCancel == true) break;

                                Thread.Sleep(100);
                            }
                        }
                        else
                        {
                            Thread.Sleep(this.SleepTimeMsc);
                        }
                    }

                    if (this.IsCancel == true) return;
                }
            }
            finally
            {
                this._bg = null;
            }
        }

        public static void AllStop()
        {
            foreach (var item in LIST)
            {
                item.Stop();
            }
        }
    }
}
