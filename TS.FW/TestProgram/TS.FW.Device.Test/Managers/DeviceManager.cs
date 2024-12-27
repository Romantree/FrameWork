using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Device.Ajin;
using TS.FW.Device.Simulation;

namespace TS.FW.Device.Test.Managers
{
    public class DeviceManager
    {
        public static DeviceManager Ins { get; private set; }

        static DeviceManager()
        {
            Ins = new DeviceManager(true);
        }

        public IDevice Device { get; private set; }

        public IAxisModule AxisModule => this.Device as IAxisModule;

        public IDInOutModule DInOutModule => this.Device as IDInOutModule;

        public IDInOut IO => this.DInOutModule.IO;

        public IAnalogModule AnalogModule => this.Device as IAnalogModule;

        public IAnalog Al => this.AnalogModule.Al;

        public DeviceManager(bool Simulation)
        {
            this.Device = Simulation ? (IDevice)new SimulationDevice() : (IDevice)new AjinDevice();
        }

        public void Start() => this.Device.Open();
    }
}
