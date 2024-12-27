using TS.FW.Utils;

namespace TS.FW.Device.Dto.DInOut
{
    public class DInOutWord : IDInOutBase
    {
        internal DInOutWord(int moduleNo, int startBitNo, ushort data) : base(moduleNo, startBitNo, 16)
        {
            this.SetData(data.ToBinary());
        }
    }
}
