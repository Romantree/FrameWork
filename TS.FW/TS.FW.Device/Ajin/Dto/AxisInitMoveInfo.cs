using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Device.Ajin.Lib;

namespace TS.FW.Device.Ajin.Dto.Axis
{
    public class AxisInitMoveInfo
    {
        public double Position { get; internal set; }

        public double Speed { get; internal set; }

        public double Accel { get; internal set; }

        public double Decel { get; internal set; }
    }
}
