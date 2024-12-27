using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TS.FW.Diagnostics;
using TS.FW.Wpf.v2.Core;

namespace TS.FW.Wpf.v2.Helpers
{
    public class ProgramHelper : IModel
    {
        private const double MB = 1024 * 1024;
        private const double UNIT = 25;

        public static ProgramHelper Ins { get; private set; }

        static ProgramHelper() => Ins = new ProgramHelper();

        private readonly BackgroundTimer trCollector = new BackgroundTimer(System.Threading.ApartmentState.MTA);
        private readonly List<double> mList = new List<double>();

        private System.Diagnostics.PerformanceCounter _mem = null;
        private double _maxMemory = 0;

        public double Memory { get => GetValue<double>(); private set => SetValue(value); }

        public string Version { get; private set; }

        public DateTime BulidTime { get; private set; }

        private ProgramHelper()
        {
            var assembly = Assembly.GetEntryAssembly();

            this.Version = string.Format("v {0}", assembly.GetName().Version);
            this.BulidTime = System.IO.File.GetLastWriteTime(assembly.Location);

            this.trCollector.SleepTimeMsc = 100;
            this.trCollector.DoWork += TrCollector_DoWork;
        }

        public void Start()
        {
            var name = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            this._mem = new System.Diagnostics.PerformanceCounter("Process", "Working Set - Private", name);

            this.trCollector.Start();
        }

        private void TrCollector_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                this.mList.Add(this._mem.NextValue());

                if (this.mList.Count < 10) return;

                this.Memory = this.mList.Average() / MB;
                this.mList.Clear();

                var value = this.ToMemory(this.Memory, UNIT);
                if (value <= this._maxMemory) return;

                this._maxMemory = value;

                Logger.CustomWrite("Memory", this, string.Format("메모리 {0:f3} MB", this.Memory), Logger.LogEventLevel.Error);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public double ToMemory(double memory, double unit)
        {
            return memory - (memory % unit);
        }
    }
}
