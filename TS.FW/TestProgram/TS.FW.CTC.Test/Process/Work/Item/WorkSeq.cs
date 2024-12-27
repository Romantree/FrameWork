using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.CTC.Test.Process.Work.Item
{
    public class WorkSeq
    {
        public int No { get; set; }

        public int GroupNo { get; set; }

        public int SlotNo { get; set; }

        public RobotAction Action { get; set; }

        public RobotMove Move { get; set; }

        private WorkSeq(int groupNo, int slotNo, RobotAction action)
        {
            this.GroupNo = groupNo;
            this.SlotNo = slotNo;
            this.Action = action;
        }

        public WorkSeq(int groupNo, int slotNo, RobotAction action, UnitRecipeType move) : this(groupNo, slotNo, action)
        {
            this.Move = this.ToRobotMove(move);
        }

        public WorkSeq(int groupNo, int slotNo, RobotAction action, RobotMove move) : this(groupNo, slotNo, action)
        {
            this.Move = move;
        }

        private RobotMove ToRobotMove(UnitRecipeType type)
        {
            switch (type)
            {
                case UnitRecipeType.Align: return RobotMove.Align;
                case UnitRecipeType.Centering: return RobotMove.Centering;
                case UnitRecipeType.Cotter: return RobotMove.Cotter;
                case UnitRecipeType.Imprint: return RobotMove.Imprint;
            }

            return RobotMove.None;
        }

        public override string ToString()
        {
            return string.Format("G:{0} S:{1} A:{2} M:{3}", this.GroupNo, this.SlotNo, this.Action, this.Move);
        }
    }
}
