using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.CTC.Test.Process.Work.Item;

namespace TS.FW.CTC.Test.Process.Robot.Item
{
    public abstract class IRobotSeq
    {
        public RobotMove Move { get; set; }

        public int SlotNo { get; set; }

        public static IEnumerable<IRobotSeq> ToRobotSeq(IEnumerable<WorkSeq> seqList)
        {
            foreach (var group in seqList.GroupBy(t => t.GroupNo))
            {
                var sList = group.ToList();

                var first = sList.First();
                var last = sList.Last();

                yield return new RobotGetSeq() { Move = first.Move, SlotNo = first.SlotNo };

                var count = sList.Count - 1;

                for (int i = 1; i < count; i += 2)
                {
                    var item = sList[i];
                    var item2 = sList[i + 1];

                    yield return new RobotExchangeSeq() { Move = item.Move, SlotNo = item.SlotNo, ExchangeSlotNo = item2.SlotNo };
                }

                yield return new RobotPutSeq() { Move = last.Move, SlotNo = last.SlotNo };
            }
        }

        public static IEnumerable<IRobotSeq> ToRobotSeqOneHand(IEnumerable<WorkSeq> seqList)
        {
            foreach (var seq in seqList)
            {
                if(seq.Action == RobotAction.GET)
                {
                    yield return new RobotGetSeq() { Move = seq.Move, SlotNo = seq.SlotNo };
                }
                else
                {
                    yield return new RobotPutSeq() { Move = seq.Move, SlotNo = seq.SlotNo };
                }
            }
        }

        public static IEnumerable<IRobotSeq> ToRobotSeqFix(IEnumerable<WorkSeq> seqList)
        {
            foreach (var group in seqList.OrderBy(t => t.GroupNo).GroupBy(t => t.GroupNo))
            {
                var sList = group.OrderBy(t => t.Action).OrderByDescending(t => t.No).ToList();

                var first = sList.First();
                var last = sList.Last();

                yield return new RobotGetSeq() { Move = first.Move, SlotNo = first.SlotNo };

                var count = sList.Count - 1;

                for (int i = 1; i < count; i += 2)
                {
                    var item = sList[i + 1];
                    var item2 = sList[i];

                    yield return new RobotExchangeSeq() { Move = item.Move, SlotNo = item.SlotNo, ExchangeSlotNo = item2.SlotNo };
                }

                yield return new RobotPutSeq() { Move = last.Move, SlotNo = last.SlotNo };
            }
        }

        public override string ToString()
        {
            return string.Format("{0} : {1}", this.Move, this.SlotNo);
        }
    }

    public class RobotPutSeq : IRobotSeq
    {
        public RobotAction Action => RobotAction.PUT;

        public override string ToString()
        {
            return string.Format("{0} [{1}]", base.ToString(), this.Action);
        }
    }

    public class RobotGetSeq : IRobotSeq
    {
        public RobotAction Action => RobotAction.GET;

        public override string ToString()
        {
            return string.Format("{0} [{1}]", base.ToString(), this.Action);
        }
    }

    public class RobotExchangeSeq : IRobotSeq
    {
        public int ExchangeSlotNo { get; set; }

        public override string ToString()
        {
            return string.Format("{0} <=> {2} [{1}]", base.ToString(), "EX", this.ExchangeSlotNo);
        }
    }
}
