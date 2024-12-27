using TS.FW.Device.Dto.DInOut;

namespace TS.FW.Device
{
    public interface IDInOut
    {
        Response<DInOutBit> ReadBitIn(int moduleNo, int bitNo);

        Response<DInOutBit> ReadBitOut(int moduleNo, int bitNo);

        Response<DInOutByte> ReadByteIn(int moduleNo, int offset);

        Response<DInOutByte> ReadByteOut(int moduleNo, int offset);

        Response<DInOutWord> ReadWordIn(int moduleNo, int offset);

        Response<DInOutWord> ReadWordOut(int moduleNo, int offset);

        Response<DInOutDWord> ReadDWordIn(int moduleNo);

        Response<DInOutDWord> ReadDWordOut(int moduleNo);

        Response WriteBit(int moduleNo, int bitNo, eSignal signal);

        Response WriteByte(int moduleNo, int offset, eSignal signal);

        Response WriteWord(int moduleNo, int offset, eSignal signal);

        Response WriteDWord(int moduleNo, eSignal signal);
    }

    public interface IDInOutModule
    {
        bool IsDInOutModule { get; }

        IDInOut IO { get; }
    }
}
