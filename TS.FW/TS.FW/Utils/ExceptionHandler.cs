using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Utils
{
    public static class ExceptionHandler
    {
        public static string MakeExceptionMessage(this Exception ex)
        {
            var sb = new StringBuilder();
            sb.Append(ex.ExceptionString());

            if (ex is System.Reflection.ReflectionTypeLoadException)
            {
                foreach (var lEx in ((System.Reflection.ReflectionTypeLoadException)ex).LoaderExceptions)
                {
                    sb.Append(Environment.NewLine + string.Format(" => {0}", lEx.ExceptionString()));
                }
            }
            else if (ex.InnerException != null)
            {
                sb.Append(Environment.NewLine + MakeExceptionMessage(ex.InnerException));
            }

            return sb.ToString();
        }

        public static string ExceptionString(this Exception ex)
        {
            var wEx = ex as WebException;
            if (wEx == null || wEx.Response == null) return string.Format("{0} => {1}", ex.Message, ex.ToString());

            return string.Format("{0}\t{1} => {2}", wEx.Message, wEx.Response.ResponseUri, wEx.ToString());
        }
    }
}
