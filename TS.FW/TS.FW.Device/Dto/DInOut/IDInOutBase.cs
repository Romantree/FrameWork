using System.Linq;

namespace TS.FW.Device.Dto.DInOut
{
    public abstract class IDInOutBase
    {
        public DInOutBit[] DInOutList { get; private set; }

        public DInOutBit this[int moduleNo, int bitNo] => this.DInOutList.FirstOrDefault(t => t.ModuleNo == moduleNo && t.BitNo == bitNo);

        protected IDInOutBase(int moduleNo, int startBitNo, int count)
        {
            this.DInOutList = new DInOutBit[count];

            for (int i = 0; i < count; i++)
            {
                this.DInOutList[i] = new DInOutBit(moduleNo, startBitNo + i);
            }
        }

        protected void SetData(string binaryString)
        {
            foreach (var item in binaryString.Reverse().Select((t, i) => new { No = i, Signal = t == '1' }))
            {
                this.DInOutList[item.No].Signal = item.Signal ? eSignal.ON : eSignal.OFF;
            }
        }

        public override string ToString()
        {
            return string.Join(" ", this.DInOutList.Select(t => string.Format("[{0}]", t)));
        }
    }
}
