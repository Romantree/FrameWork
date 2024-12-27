using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Device.Ajin.Lib;

namespace TS.FW.Device.Ajin.Dto.Axis
{
    public class SoftwareLimitInfo
    {
        public bool Enable { get; internal set; }

        public AXT_MOTION_STOPMODE StopMode { get; internal set; }

        public AXT_MOTION_SELECTION Selection { get; internal set; }

        public double PositivePos { get; internal set; }

        public double NegativePos { get; internal set; }
    }
}
