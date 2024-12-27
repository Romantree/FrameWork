using TS.FW.Utils;

namespace TS.FW.Device.Dto.DInOut
{
    public class DInOutByte : IDInOutBase
    {
        internal DInOutByte(int moduleNo, int startBitNo, byte data) : base(moduleNo, startBitNo, 8)
        {
            this.SetData(data.ToBinary());
        }
    }
}
