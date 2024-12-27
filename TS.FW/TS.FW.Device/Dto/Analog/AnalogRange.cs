using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Device.Dto.Analog
{
    public class AnalogRange
    {
        public int ChannelNo { get; internal set; }

        public double MinVoltage { get; internal set; }

        public double MaxVoltage { get; internal set; }

        public double ToDigit(double voltage, double maxDigit = ushort.MaxValue)
        {
            return ToDigit(voltage, this.MinVoltage, this.MaxVoltage, maxDigit);
        }

        public double ToVoltage(double digit, double maxDigit = ushort.MaxValue)
        {
            return ToVoltage(digit, this.MinVoltage, this.MaxVoltage, maxDigit);
        }

        public double ToVoltageRangeChange(double voltage, double minVoltage, double maxVoltage)
        {
            return ToVoltageRangeChange(voltage, minVoltage, maxVoltage, this.MinVoltage, this.MaxVoltage);
        }

        public AnalogRange(int channelNo, double min, double max)
        {
            this.ChannelNo = channelNo;
            this.MinVoltage = min;
            this.MaxVoltage = max;
        }

        public static double ToDigit(double voltage, double minVoltage, double maxVoltage, double maxDigit = ushort.MaxValue)
        {
            return maxDigit * ((voltage - minVoltage) / (maxVoltage - minVoltage));
        }

        public static double ToVoltage(double digit, double minVoltage, double maxVoltage, double maxDigit = ushort.MaxValue)
        {
            return ((maxVoltage - minVoltage) * (digit / maxDigit)) + minVoltage;
        }

        public static double ToVoltageRangeChange(double voltage, double minVoltageSrc, double maxVoltageSrc, double minVoltageDst, double maxVoltageDst)
        {
            return ((voltage - minVoltageSrc) * (maxVoltageDst - minVoltageDst) / (maxVoltageSrc - minVoltageSrc)) + minVoltageDst;
        }
    }
}
