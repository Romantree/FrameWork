using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.CTC.Test.Process.Work.Item;

namespace TS.FW.CTC.Test.Process.Work.Align
{
    public partial class AlignProcess
    {
        public StepResult START(WorkItem item)
        {
            this.SetProcessMessage("시작 : {0}", item.SlotNo);

            System.Threading.Thread.Sleep(UnitTestDelay);

            return StepResult.Next;
        }

        public StepResult END(WorkItem item)
        {
            this.SetProcessMessage("종료 : {0}", item.SlotNo);

            return StepResult.Finish;
        }
    }
}
