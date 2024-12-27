using System;
using TS.FW.Device.Ajin.Lib;
using TS.FW.Device.Dto.Analog;

namespace TS.FW.Device.Ajin.Core
{
    internal static class AnalogHelper
    {
        public static Response<AnalogRange> AxaiGetRange(this AjinAnalog ajin, int channelNo)
        {
            try
            {
                double min = 0;
                double max = 0;

                var res = (AXT_FUNC_RESULT)CAXA.AxaiGetRange(channelNo, ref min, ref max);
                if (res.ErrorCheck() == false) return new Response<AnalogRange>(false, "AxaiGetRange Error [{0}]", channelNo);

                return new Response<AnalogRange>(new AnalogRange(channelNo, min, max));
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response<AnalogRange> AxaoGetRange(this AjinAnalog ajin, int channelNo)
        {
            try
            {
                double min = 0;
                double max = 0;

                var res = (AXT_FUNC_RESULT)CAXA.AxaoGetRange(channelNo, ref min, ref max);
                if (res.ErrorCheck() == false) return new Response<AnalogRange>(false, "AxaoGetRange Error [{0}]", channelNo);

                return new Response<AnalogRange>(new AnalogRange(channelNo, min, max));
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxaiSetRange(this AjinAnalog ajin, int channelNo, double minVoltage, double maxVoltage)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXA.AxaiSetRange(channelNo, minVoltage, maxVoltage);
                if (res.ErrorCheck() == false) return new Response(false, "AxaiSetRange Error [{0}]", channelNo);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxaoSetRange(this AjinAnalog ajin, int channelNo, double minVoltage, double maxVoltage)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXA.AxaoSetRange(channelNo, minVoltage, maxVoltage);
                if (res.ErrorCheck() == false) return new Response(false, "AxaoSetRange Error [{0}]", channelNo);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response<double> AxaiSwReadDigit(this AjinAnalog ajin, int channelNo)
        {
            try
            {
                uint digit = 0;

                var res = (AXT_FUNC_RESULT)CAXA.AxaiSwReadDigit(channelNo, ref digit);
                if (res.ErrorCheck() == false) return new Response<double>(false, "AxaiSwReadDigit Error [{0}]", channelNo);

                return new Response<double>(digit);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response<double> AxaiSwReadVoltage(this AjinAnalog ajin, int channelNo)
        {
            try
            {
                double voltage = 0;

                var res = (AXT_FUNC_RESULT)CAXA.AxaiSwReadVoltage(channelNo, ref voltage);
                if (res.ErrorCheck() == false) return new Response<double>(false, "AxaiSwReadVoltage Error [{0}]", channelNo);

                return new Response<double>(voltage);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response<double> AxaoReadVoltage(this AjinAnalog ajin, int channelNo)
        {
            try
            {
                double voltage = 0;

                var res = (AXT_FUNC_RESULT)CAXA.AxaoReadVoltage(channelNo, ref voltage);
                if (res.ErrorCheck() == false) return new Response<double>(false, "AxaoReadVoltage Error [{0}]", channelNo);

                return new Response<double>(voltage);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxaoWriteVoltage(this AjinAnalog ajin, int channelNo, double voltage)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXA.AxaoWriteVoltage(channelNo, voltage);
                if (res.ErrorCheck() == false) return new Response(false, "AxaoWriteVoltage Error [{0} = {1}]", channelNo, voltage);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
