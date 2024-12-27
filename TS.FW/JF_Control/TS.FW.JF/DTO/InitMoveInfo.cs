using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.JF.DTO
{
    public class InitMoveInfo
    {
        public enDir Dir { get; internal set; }
        public double Position { get; internal set; }
        public double Speed { get; internal set; }
        public int Accel { get; internal set; }
        public int Decel { get; internal set; }
        public double JogSpeed { get; internal set; }
        public int JogAccel { get; internal set; }
    }
}
