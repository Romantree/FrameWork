using TS.FW.Device.Ajin.Core;
using TS.FW.Device.Dto.DInOut;

namespace TS.FW.Device.Ajin
{
    public class AjinDInOut : IDInOut
    {
        public Response<DInOutBit> ReadBitIn(int moduleNo, int bitNo)
        {
            return this.AxdiReadInportBit(moduleNo, bitNo);
        }

        public Response<DInOutBit> ReadBitOut(int moduleNo, int bitNo)
        {
            return this.AxdoReadOutportBit(moduleNo, bitNo);
        }

        public Response<DInOutByte> ReadByteIn(int moduleNo, int offset)
        {
            return this.AxdiReadInportByte(moduleNo, offset);
        }

        public Response<DInOutByte> ReadByteOut(int moduleNo, int offset)
        {
            return this.AxdoReadOutportByte(moduleNo, offset);
        }

        public Response<DInOutWord> ReadWordIn(int moduleNo, int offset)
        {
            return this.AxdiReadInportWord(moduleNo, offset);
        }

        public Response<DInOutWord> ReadWordOut(int moduleNo, int offset)
        {
            return this.AxdoReadOutportWord(moduleNo, offset);
        }

        public Response<DInOutDWord> ReadDWordIn(int moduleNo)
        {
            return this.AxdiReadInportDword(moduleNo);
        }

        public Response<DInOutDWord> ReadDWordOut(int moduleNo)
        {
            return this.AxdoReadOutportDword(moduleNo);
        }

        public Response WriteBit(int moduleNo, int bitNo, eSignal signal)
        {
            return this.AxdoWriteOutportBit(moduleNo, bitNo, signal == eSignal.ON);
        }

        public Response WriteByte(int moduleNo, int offset, eSignal signal)
        {
            return this.AxdoWriteOutportByte(moduleNo, offset, signal == eSignal.ON);
        }

        public Response WriteWord(int moduleNo, int offset, eSignal signal)
        {
            return this.AxdoWriteOutportWord(moduleNo, offset, signal == eSignal.ON);
        }

        public Response WriteDWord(int moduleNo, eSignal signal)
        {
            return this.AxdoWriteOutportDword(moduleNo, 0, signal == eSignal.ON);
        }
    }
}
