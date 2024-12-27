using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Utils
{
    public static class DateTimeHandler
    {
        /// <summary>
        /// 날짜 및 시간 정보를 sec 정보로 변환
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int DateToSec(this DateTime date)
        {
            DateTime DT = date;
            TimeSpan TS = DT - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            double dSec = TS.TotalSeconds;
            return Convert.ToInt32(dSec);
        }
        /// <summary>
        /// sec 정보를 날짜 정보 Datetime 으로 변환
        /// </summary>
        /// <param name="dsec"></param>
        /// <returns></returns>
        public static string SecToDate(this int dsec)
        {
            TimeSpan TS = TimeSpan.FromSeconds(dsec);
            DateTime DT = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) + TS;
            string strDT = DT.ToString("yyyy/MM/dd HH:mm:ss");
            return strDT;
        }

    }
}
