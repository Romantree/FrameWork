using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.CTC.Test.Process.Work.Align;
using TS.FW.CTC.Test.Process.Work.Centering;
using TS.FW.CTC.Test.Process.Work.Cotter;

namespace TS.FW.CTC.Test.Process.Work.Item
{
    public class WorkStep
    {
        public ProcessStep<AlignStep> Align { get; } = new ProcessStep<AlignStep>();

        public ProcessStep<CenteringStep> Centering { get; } = new ProcessStep<CenteringStep>();

        public ProcessStep<CotterStep> Cotter { get; } = new ProcessStep<CotterStep>();
    }
}
