using System;
using System.Collections.Generic;
using TS.FW.Device.Dto.Analog;

namespace TS.FW.Device.Simulation
{
    public class SimulationAnalog : IAnalog
    {
        private readonly Dictionary<int, AnalogRangeValue> _channelList = new Dictionary<int, AnalogRangeValue>();

        public Response<AnalogRange> GetRangeIn(int channelNo)
        {
            try
            {
                return new Response<AnalogRange>(this.GetChannelRange(channelNo));
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response<AnalogRange> GetRangeOut(int channelNo)
        {
            try
            {
                return new Response<AnalogRange>(this.GetChannelRange(channelNo));
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response<double> ReadVoltageIn(int channelNo)
        {
            try
            {
                var range = this.GetChannelRange(channelNo);

                return new Response<double>(range.Voltage);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response<double> ReadVoltageOut(int channelNo)
        {
            try
            {
                var range = this.GetChannelRange(channelNo);

                return new Response<double>(range.Voltage);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response SetRangeIn(int channelNo, double minVoltage, double maxVoltage)
        {
            try
            {
                var range = this.GetChannelRange(channelNo);
                range.MinVoltage = minVoltage;
                range.MaxVoltage = maxVoltage;

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response SetRangeOut(int channelNo, double minVoltage, double maxVoltage)
        {
            try
            {
                var range = this.GetChannelRange(channelNo);
                range.MinVoltage = minVoltage;
                range.MaxVoltage = maxVoltage;

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response WriteVoltage(int channelNo, double voltage)
        {
            try
            {
                var range = this.GetChannelRange(channelNo);
                range.Voltage = voltage;

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        private AnalogRangeValue GetChannelRange(int channelNo)
        {
            if (this._channelList.ContainsKey(channelNo) == false)
            {
                this._channelList.Add(channelNo, new AnalogRangeValue(channelNo, -10, 10));
            }

            return this._channelList[channelNo];
        }
    }

    internal class AnalogRangeValue : AnalogRange
    {
        private const double SCALE = 0.00001;
        private static readonly Random _ran = new Random((int)DateTime.Now.Ticks);

        public double Voltage { get; set; }

        public AnalogRangeValue(int channelNo, double min, double max) : base(channelNo, min, max)
        {
            this.Voltage = this.GetRandomValue(this);
        }

        private double GetRandomValue(AnalogRange range)
        {
            var value = _ran.Next((int)(range.MinVoltage / SCALE), (int)(range.MaxVoltage / SCALE));

            return value * SCALE;
        }
    }
}
