using TS.FW.Utils;

namespace TS.FW.Device.Dto.DInOut
{
    public class DInOutDWord : IDInOutBase
    {
        internal DInOutDWord(int moduleNo, int startBitNo, uint data) : base(moduleNo, startBitNo, 32)
        {
            this.SetData(data.ToBinary());
        }
    }
}
