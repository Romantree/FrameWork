using System.Collections.Generic;
using TS.FW.CTC.Test.Process.Robot.Item;

namespace TS.FW.CTC.Test.Process
{
    public class PortData
    {
        public int PortNo { get; set; }

        public Dictionary<int, bool> SlotList { get; private set; } = new Dictionary<int, bool>();

        public List<RobotHistory> History { get; private set; } = new List<RobotHistory>();
    }
}
