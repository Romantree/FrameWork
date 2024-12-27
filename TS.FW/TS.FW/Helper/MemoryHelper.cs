using System;
using System.Collections.Generic;
using System.Linq;

namespace TS.FW.Helper
{
    public class MemoryHelper
    {
        public static MemoryHelper Ins { get; private set; }

        static MemoryHelper() => Ins = new MemoryHelper();

        private readonly List<double> _mList = new List<double>();

        private System.Diagnostics.PerformanceCounter _mem = null;
        private double _maxMemory = 0;

        public double Memory { get; private set; }

        public int MaxCount { get; set; } = 10;

        public double LogUnit { get; set; } = 25;

        private MemoryHelper() { }

        public void InitControl()
        {
            try
            {
                var name = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                this._mem = new System.Diagnostics.PerformanceCounter("Process", "Working Set - Private", name);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void SetCollect()
        {
            try
            {
                this._mList.Add(this._mem.NextValue());

                if (this._mList.Count < MaxCount) return;

                this.Memory = this._mList.Average() / 1024D / 1024D;
                this._mList.Clear();

                var value = this.ToMemory(this.Memory, LogUnit);
                if (value <= this._maxMemory) return;

                this._maxMemory = value;

                Logger.Write(this, string.Format("메모리 {0:f3} MB", this.Memory), Logger.LogEventLevel.Error);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private double ToMemory(double memory, double unit) => memory - (memory % unit);
    }
}
