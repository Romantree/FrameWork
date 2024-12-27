using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW
{
    public static class Win32Api
    {
        #region Ini File API

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int GetPrivateProfileInt(string ipAppName, string ipKeyName, int ipDefault, string iniFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetPrivateProfileSection(string lpAppName, IntPtr lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, uint nSize, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern int GetPrivateProfileStringW(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, char[] lpReturnedString, int nSize, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, IntPtr lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, byte[] lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        #endregion

        #region Windows DateTime

        [DllImport("kernel32.dll")]
        private static extern uint SetLocalTime(ref stTime lpSystemTime);

        [StructLayout(LayoutKind.Sequential)]
        private struct stTime
        {
            internal short Year;
            internal short Month;
            internal short DayOfWeek;
            internal short Day;
            internal short Hour;
            internal short Minute;
            internal short Second;
            internal short Millisecond;
        }

        /// <summary>
        /// 시간 변경 유틸 함수
        /// </summary>
        /// <param name="dateTime"></param>
        public static Response SetWindowsTime(this DateTime dateTime)
        {
            try
            {
                if (DateTime.MinValue == dateTime) return new Response(false, "최소 날짜로는 변경이 불가능 합니다.");

                var time = new stTime()
                {
                    Year = (short)dateTime.Year,
                    Month = (short)dateTime.Month,
                    DayOfWeek = (short)dateTime.DayOfWeek,
                    Day = (short)dateTime.Day,
                    Hour = (short)dateTime.Hour,
                    Minute = (short)dateTime.Minute,
                    Second = (short)dateTime.Second,
                    Millisecond = (short)dateTime.Millisecond,
                };

                SetLocalTime(ref time);

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        #endregion
    }
}
