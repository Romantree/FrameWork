using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Device.Ajin.Lib;

namespace TS.FW.Device.Ajin.Core
{
    internal static class DeviceHelper
    {
        public static bool AxlIsOpened(this AjinDevice device)
        {
            return CAXL.AxlIsOpened() == 1;
        }

        public static Response AxlOpenNoReset(this AjinDevice device)
        {
            try
            {
                if (device.IsOpen) return new Response();

                // 기존 파라메타 유지를 위해 AxlOpenNoReset 호출
                // 제품 출고 초기화 값 7 기본 설정
                var res = (AXT_FUNC_RESULT)CAXL.AxlOpenNoReset(7);
                if (res.ErrorCheck() == false) return new Response(false, "Open Error");

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxlClose(this AjinDevice device)
        {
            try
            {
                if (device.IsOpen == false) return new Response();

                return new Response(CAXL.AxlClose() == 1, "");
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxmMotLoadParaAll(this AjinDevice device, string filePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath)) return new Response(false, "FilePath 'Null' Or Empty.");
                if (File.Exists(filePath) == false) return new Response(false, "File Not Found. '{0}'", filePath);

                var res = (AXT_FUNC_RESULT)CAXM.AxmMotLoadParaAll(Path.GetFullPath(filePath));
                if (res.ErrorCheck() == false) return new Response(false, "AxmMotLoadParaAll Error [{0}]", filePath);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxmMotSaveParaAll(this AjinDevice device, string filePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath)) return new Response(false, "FilePath 'Null' Or Empty.");
                //if (CAXM.AxmMotSaveParaAll(temp) != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)

                var res = (AXT_FUNC_RESULT)CAXM.AxmMotSaveParaAll(Path.GetFullPath(filePath));
                if (res.ErrorCheck() == false) return new Response(false, "AxmMotSaveParaAll Error [{0}]", filePath);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response AxmVirtualSetAxisNoMap(this AjinDevice device, int nRealAxisNo, int nVirtualAxisNo)
        {
            try
            {
                var res = (AXT_FUNC_RESULT)CAXM.AxmVirtualSetAxisNoMap(nRealAxisNo, nVirtualAxisNo);
                if (res.ErrorCheck() == false) return new Response(false, "AxmVirtualSetAxisNoMap Error");

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static bool AxdInfoIsDIOModule(this AjinDevice device)
        {
            try
            {
                uint status = 0;

                var res = (AXT_FUNC_RESULT)CAXD.AxdInfoIsDIOModule(ref status);
                if (res.ErrorCheck() == false) return false;

                return status == (uint)AXT_EXISTENCE.STATUS_EXIST;
            }
            catch (Exception ex)
            {
                Logger.Write(device, ex);
                return false;
            }
        }

        public static bool AxaInfoIsAIOModule(this AjinDevice device)
        {
            try
            {
                uint status = 0;

                var res = (AXT_FUNC_RESULT)CAXA.AxaInfoIsAIOModule(ref status);
                if (res.ErrorCheck() == false) return false;

                return status == (uint)AXT_EXISTENCE.STATUS_EXIST;
            }
            catch (Exception ex)
            {
                Logger.Write(device, ex);
                return false;
            }
        }

        public static bool ErrorCheck(this AXT_FUNC_RESULT res, [CallerMemberName] string callerMemberName = null)
        {
            if (res == AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                return true;
            }
            else
            {
                Logger.Write(typeof(DeviceHelper), string.Format("{0} Error : {1}[{2}]", callerMemberName, res, (uint)res), Logger.LogEventLevel.Error);
                return false;
            }
        }
    }
}
