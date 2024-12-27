using System;
using TS.FW.Device.Ajin.Lib;
using TS.FW.Device.Dto.DInOut;

namespace TS.FW.Device.Ajin.Core
{
    internal static class DInOutHelper
    {
        public static Response<DInOutBit> AxdiReadInportBit(this AjinDInOut ajin, int moduleNo, int bitNo)
        {
            try
            {
                uint value = 0;

                var res = (AXT_FUNC_RESULT)CAXD.AxdiReadInportBit(moduleNo, bitNo, ref value);
                if (res.ErrorCheck() == false) return new Response<DInOutBit>(false, "AxdiReadInportBit Error [{0} = {1}]", moduleNo, bitNo);

                return new Response<DInOutBit>(new DInOutBit(moduleNo, bitNo, value == 1));
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response<DInOutBit> AxdoReadOutportBit(this AjinDInOut ajin, int moduleNo, int bitNo)
        {
            try
            {
                uint value = 0;

                var res = (AXT_FUNC_RESULT)CAXD.AxdoReadOutportBit(moduleNo, bitNo, ref value);
                if (res.ErrorCheck() == false) return new Response<DInOutBit>(false, "AxdoReadOutportBit Error [{0} = {1}]", moduleNo, bitNo);

                return new Response<DInOutBit>(new DInOutBit(moduleNo, bitNo, value == 1));
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response<DInOutByte> AxdiReadInportByte(this AjinDInOut ajin, int moduleNo, int offset)
        {
            try
            {
                uint value = 0;

                var res = (AXT_FUNC_RESULT)CAXD.AxdiReadInportByte(moduleNo, offset, ref value);
                if (res.ErrorCheck() == false) return new Response<DInOutByte>(false, "AxdiReadInportByte Error [{0} = {1}]", moduleNo, offset);

                return new Response<DInOutByte>(new DInOutByte(moduleNo, offset * 8, (byte)value));
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response<DInOutByte> AxdoReadOutportByte(this AjinDInOut ajin, int moduleNo, int offset)
        {
            try
            {
                uint value = 0;

                var res = (AXT_FUNC_RESULT)CAXD.AxdoReadOutportByte(moduleNo, offset, ref value);
                if (res.ErrorCheck() == false) return new Response<DInOutByte>(false, "AxdoReadOutportByte Error [{0} = {1}]", moduleNo, offset);

                return new Response<DInOutByte>(new DInOutByte(moduleNo, offset * 8, (byte)value));
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response<DInOutWord> AxdiReadInportWord(this AjinDInOut ajin, int moduleNo, int offset)
        {
            try
            {
                uint value = 0;

                var res = (AXT_FUNC_RESULT)CAXD.AxdiReadInportWord(moduleNo, offset, ref value);
                if (res.ErrorCheck() == false) return new Response<DInOutWord>(false, "AxdiReadInportWord Error [{0} = {1}]", moduleNo, offset);

                return new Response<DInOutWord>(new DInOutWord(moduleNo, offset * 16, (ushort)value));
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response<DInOutWord> AxdoReadOutportWord(this AjinDInOut ajin, int moduleNo, int offset)
        {
            try
            {
                uint value = 0;

                var res = (AXT_FUNC_RESULT)CAXD.AxdoReadOutportWord(moduleNo, offset, ref value);
                if (res.ErrorCheck() == false) return new Response<DInOutWord>(false, "AxdoReadOutportWord Error [{0} = {1}]", moduleNo, offset);

                return new Response<DInOutWord>(new DInOutWord(moduleNo, offset * 16, (ushort)value));
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response<DInOutDWord> AxdiReadInportDword(this AjinDInOut ajin, int moduleNo, int offset = 0)
        {
            try
            {
                uint value = 0;

                var res = (AXT_FUNC_RESULT)CAXD.AxdiReadInportDword(moduleNo, offset, ref value);
                if (res.ErrorCheck() == false) return new Response<DInOutDWord>(false, "AxdiReadInportDword Error [{0} = {1}]", moduleNo, offset);

                return new Response<DInOutDWord>(new DInOutDWord(moduleNo, offset * 32, value));
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response<DInOutDWord> AxdoReadOutportDword(this AjinDInOut ajin, int moduleNo, int offset = 0)
        {
            try
            {
                uint value = 0;

                var res = (AXT_FUNC_RESULT)CAXD.AxdoReadOutportDword(moduleNo, offset, ref value);
                if (res.ErrorCheck() == false) return new Response<DInOutDWord>(false, "AxdoReadOutportDword Error [{0} = {1}]", moduleNo, offset);

                return new Response<DInOutDWord>(new DInOutDWord(moduleNo, offset * 32, value));
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxdoWriteOutportBit(this AjinDInOut ajin, int moduleNo, int bitNo, bool signal)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXD.AxdoWriteOutportBit(moduleNo, bitNo, (uint)(signal ? 1 : 0));
                if (res.ErrorCheck() == false) return new Response(false, "AxdoWriteOutportBit Error [{0} = {1}]", moduleNo, bitNo);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxdoWriteOutportByte(this AjinDInOut ajin, int moduleNo, int offset, bool signal)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXD.AxdoWriteOutportByte(moduleNo, offset, (uint)(signal ? byte.MaxValue : 0));
                if (res.ErrorCheck() == false) return new Response(false, "AxdoWriteOutportBit Error [{0} = {1}]", moduleNo, offset);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxdoWriteOutportWord(this AjinDInOut ajin, int moduleNo, int offset, bool signal)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXD.AxdoWriteOutportWord(moduleNo, offset, (uint)(signal ? ushort.MaxValue : 0));
                if (res.ErrorCheck() == false) return new Response(false, "AxdoWriteOutportWord Error [{0} = {1}]", moduleNo, offset);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxdoWriteOutportDword(this AjinDInOut ajin, int moduleNo, int offset, bool signal)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXD.AxdoWriteOutportDword(moduleNo, offset, (uint)(signal ? uint.MaxValue : 0));
                if (res.ErrorCheck() == false) return new Response(false, "AxdoWriteOutportDword Error [{0} = {1}]", moduleNo, offset);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
