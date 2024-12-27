using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Device.Ajin.Lib;

namespace TS.FW.Device.Ajin.Dto.Axis
{
    public class AxisLimitInfo
    {
        public AXT_MOTION_STOPMODE EStopMode { get; internal set; }

        public AXT_MOTION_LEVEL_MODE PositiveLevel { get; internal set; }

        public AXT_MOTION_LEVEL_MODE NegtiveLevel { get; internal set; }
    }
}
