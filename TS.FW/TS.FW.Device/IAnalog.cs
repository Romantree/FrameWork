using TS.FW.Device.Dto.Analog;

namespace TS.FW.Device
{
    public interface IAnalogModule
    {
        bool IsIAnalogModule { get; }

        IAnalog Al { get; }
    }

    public interface IAnalog
    {
        Response SetRangeIn(int channelNo, double minVoltage, double maxVoltage);

        Response SetRangeOut(int channelNo, double minVoltage, double maxVoltage);

        Response<AnalogRange> GetRangeIn(int channelNo);

        Response<AnalogRange> GetRangeOut(int channelNo);

        Response<double> ReadVoltageIn(int channelNo);

        Response<double> ReadVoltageOut(int channelNo);

        Response WriteVoltage(int channelNo, double voltage);
    }
}
