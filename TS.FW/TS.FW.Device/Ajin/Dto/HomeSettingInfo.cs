using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Device.Ajin.Lib;

namespace TS.FW.Device.Ajin.Dto.Axis
{
    public class HomeSettingInfo
    {
        public AXT_MOTION_LEVEL_MODE HomeMode { get; internal set; }

        public AXT_MOTION_MOVE_DIR Direction { get; internal set; }

        public AXT_MOTION_HOME_DETECT HomeLevel { get; internal set; }

        public bool ZPhase { get; internal set; }

        public double ClearDeleay { get; internal set; }

        public double OffSet { get; internal set; }

        public double FirstSpeed { get; internal set; }

        public double SecondSpeed { get; internal set; }

        public double ThirdSpeed { get; internal set; }

        public double LastSpeed { get; internal set; }

        public double FirstAccel { get; internal set; }

        public double SecoundAccel { get; internal set; }
    }
}
