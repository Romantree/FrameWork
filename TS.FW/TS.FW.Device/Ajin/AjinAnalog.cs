using System;
using TS.FW.Device.Ajin.Core;
using TS.FW.Device.Dto.Analog;

namespace TS.FW.Device.Ajin
{
    public class AjinAnalog : IAnalog
    {
        public Response<AnalogRange> GetRangeIn(int channelNo)
        {
            return this.AxaiGetRange(channelNo);
        }

        public Response<AnalogRange> GetRangeOut(int channelNo)
        {
            return this.AxaoGetRange(channelNo);
        }

        public Response SetRangeIn(int channelNo, double minVoltage, double maxVoltage)
        {
            return this.AxaiSetRange(channelNo, minVoltage, maxVoltage);
        }

        public Response SetRangeOut(int channelNo, double minVoltage, double maxVoltage)
        {
            return this.AxaoSetRange(channelNo, minVoltage, maxVoltage);
        }

        public Response<double> ReadVoltageIn(int channelNo)
        {
            return this.AxaiSwReadVoltage(channelNo);
        }

        public Response<double> ReadVoltageOut(int channelNo)
        {
            return this.AxaoReadVoltage(channelNo);
        }

        public Response WriteVoltage(int channelNo, double voltage)
        {
            return this.AxaoWriteVoltage(channelNo, voltage);
        }

        public Response<double> ReadDigitIn(int channelNo)
        {
            try
            {
                return this.AxaiSwReadDigit(channelNo);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
