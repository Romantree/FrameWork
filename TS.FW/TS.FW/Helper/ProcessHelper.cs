using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Helper
{
    public static class ProcessHelper
    {
        public static bool TimeOutCheck(this Stopwatch watch, int timeMsc)
        {
            return watch.ElapsedMilliseconds > timeMsc;
        }

        // TODO : 버그 존재함 -100위치에서 +100 위치 체크시 동일한 위치라 판단 (박현식)
        //public static bool CheckPosition(this double curPosition, double position, double gap = 0.01)
        //{
        //    var cur = Math.Abs(curPosition);
        //    var pos = Math.Abs(position);

        //    return Math.Abs(cur - pos) < gap;
        //}

        public static bool CheckPosition(this double curPosition, double position, double gap = 0.01)
        {
            return Math.Sqrt(Math.Pow(curPosition - position, 2)) < gap;
        }
    }
}
