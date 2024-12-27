using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.CTC.Test.Process.Robot.Item
{
    public class RobotArm
    {
        public RobotArmType RobotArmType { get; set; }

        public bool IsWafer { get; set; }

        public int WaferSlot { get; set; }

        public override string ToString() => this.RobotArmType.ToString();

        public static implicit operator bool(RobotArm item) => item.IsWafer;
        public static implicit operator int(RobotArm item) => item.WaferSlot;
    }
}
