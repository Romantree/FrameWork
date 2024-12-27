namespace TS.FW.Device.Dto.DInOut
{
    public class DInOutBit
    {
        public int ModuleNo { get; private set; }

        public int BitNo { get; private set; }

        public eSignal Signal { get; internal set; }

        public DInOutBit(int moduleNo, int bitNo)
        {
            this.ModuleNo = moduleNo;
            this.BitNo = bitNo;
            this.Signal = eSignal.ERROR;
        }

        public DInOutBit(int moduleNo, int bitNo, bool signal) : this(moduleNo, bitNo)
        {
            this.Signal = signal ? eSignal.ON : eSignal.OFF;
        }

        public override string ToString()
        {
            return string.Format("M:{0} B:{1} = {2}", this.ModuleNo, this.BitNo, this.Signal);
        }

        public static implicit operator eSignal(DInOutBit item)
        {
            return item.Signal;
        }

        public static implicit operator bool(DInOutBit item)
        {
            return item.Signal == eSignal.ON ? true : false;
        }
    }
}
