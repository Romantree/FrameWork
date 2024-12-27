using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.CTC.Test.Process.Robot.Item
{
    public class RobotHistory
    {
        public DateTime LogTime { get; private set; }

        public int PortNo { get; set; }

        public int SlotNo { get; set; }

        public RobotArmType RobotArm { get; set; }

        public RobotAction Action { get; set; }

        public RobotMove Move { get; set; }

        public RobotHistory()
        {
            this.LogTime = DateTime.Now;
        }
    }
}
