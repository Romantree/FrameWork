using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Utils
{
    public static class IniFileHandler
    {
        /// <summary>
        /// Ini 파일 존재 여부 확인
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static bool IsFileExists(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)) return false;
            //if (string.Equals(Path.GetExtension(filePath), ".ini", StringComparison.OrdinalIgnoreCase) == false) return false;

            return File.Exists(filePath);
        }

        private static string[] ConvertToStringArray(this IntPtr ptr, int length)
        {
            if (length == 0) return new string[0];

            return Marshal.PtrToStringAuto(ptr, length - 1).Split(new char[1]);
        }

        public static Response<string[]> GetIniFile_KeyNameList(this string filePath, string sectionName)
        {
            if (IniFileHandler.IsFileExists(filePath) == false) return new Response<string[]>(false, string.Format("INI 파일이 존재하지 않습니다. {0}", filePath));
            if (string.IsNullOrWhiteSpace(sectionName)) return new Response<string[]>(false, "SectionName  값이 Null 입니다.");

            var handler = Marshal.AllocCoTaskMem(524288);

            try
            {
                var length = Win32Api.GetPrivateProfileString(sectionName, null, null, handler, 524288, filePath);
                var list = handler.ConvertToStringArray(length);

                return new Response<string[]>(list);
            }
            catch (Exception ex)
            {
                return ex;
            }
            finally
            {
                Marshal.FreeCoTaskMem(handler);
            }
        }

        public static Response<string> GetIniFile_Value(this string filePath, string sectionName, string keyName, string defaultValue)
        {
            if (IniFileHandler.IsFileExists(filePath) == false) return new Response<string>(false, string.Format("INI 파일이 존재하지 않습니다. {0}", filePath));
            if (string.IsNullOrWhiteSpace(sectionName)) return new Response<string>(false, "SectionName  값이 Null 입니다.");
            if (string.IsNullOrWhiteSpace(keyName)) return new Response<string>(false, "KeyName  값이 Null 입니다.");

            try
            {
                var tokens = new StringBuilder(524288);

                Win32Api.GetPrivateProfileStringW(sectionName, keyName, defaultValue, tokens, tokens.Capacity, filePath);

                return new Response<string>(tokens.ToString());
            }
            catch (Exception ex)
            {
                return new Response<string>(ex) { Result = defaultValue };
            }
        }

        public static Response<int> GetIniFile_Value(this string filePath, string sectionName, string keyName, int defaultValue)
        {
            try
            {
                var res = GetIniFile_Value(filePath, sectionName, keyName, "");

                if (res == false) return new Response<int>(false, res.Comment);

                int.TryParse(res.Result, out defaultValue);

                return new Response<int>(defaultValue);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex) { Result = defaultValue };
            }
        }

        public static Response<double> GetIniFile_Value(this string filePath, string sectionName, string keyName, double defaultValue)
        {
            try
            {
                var res = GetIniFile_Value(filePath, sectionName, keyName, "");

                if (res == false) return new Response<double>(false, res.Comment);

                double.TryParse(res.Result, out defaultValue);

                return new Response<double>(defaultValue);
            }
            catch (Exception ex)
            {
                return new Response<double>(ex) { Result = defaultValue };
            }
        }

        public static ResponseList<KeyValuePair<string, string>> GetIniFile_ToList(this string filePath, string sectionName, string defaultValue)
        {
            try
            {
                var keyList = IniFileHandler.GetIniFile_KeyNameList(filePath, sectionName);
                if (keyList == false) return new ResponseList<KeyValuePair<string, string>>(false, keyList.Comment);

                var list = new List<KeyValuePair<string, string>>();

                foreach (var key in keyList.Result)
                {
                    var value = IniFileHandler.GetIniFile_Value(filePath, sectionName, key, defaultValue);
                    if (value == false) return new ResponseList<KeyValuePair<string, string>>(false, value.Comment);

                    list.Add(new KeyValuePair<string, string>(key, value.Result));
                }

                return new ResponseList<KeyValuePair<string, string>>(list);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static ResponseList<string, string> GetIniFile_ToDictionary(this string filePath, string sectionName, string defaultValue)
        {
            try
            {
                var keyList = IniFileHandler.GetIniFile_KeyNameList(filePath, sectionName);
                if (keyList == false) return new ResponseList<string, string>(false, keyList.Comment);

                var list = new Dictionary<string, string>();

                foreach (var key in keyList.Result.Distinct())
                {
                    if (string.IsNullOrEmpty(key) || key.Contains("EMPTY")) continue;
                    var value = IniFileHandler.GetIniFile_Value(filePath, sectionName, key, defaultValue);
                    if (value == false) return new ResponseList<string, string>();

                    list.Add(key, value.Result);
                }

                return new ResponseList<string, string>(list);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response SetIniFile_Value(this string filePath, string sectionName, string keyName, string value)
        {
            try
            {
                //if (IniFileHandler.IsFileExists(filePath) == false) return new Response<string[]>(false, string.Format("INI 파일이 존재하지 않습니다. {0}", filePath));

                if (Win32Api.WritePrivateProfileString(sectionName, keyName, value, filePath) == false)
                    return new Response(false, string.Format("알 수 없는 이유로 데이터 등록에 실패하였습니다. FilePath='{0}', SectionName='{1}', KeyName='{2}', Value='{3}'"
                        , filePath
                        , sectionName
                        , keyName
                        , value));

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response SetIniFile_Value(this string filePath, string sectionName, string keyName, short value)
        {
            try
            {
                return IniFileHandler.SetIniFile_Value(filePath, sectionName, keyName, value.ToString());
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response SetIniFile_Value(this string filePath, string sectionName, string keyName, int value)
        {
            try
            {
                return IniFileHandler.SetIniFile_Value(filePath, sectionName, keyName, value.ToString());
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response SetIniFile_Value(this string filePath, string sectionName, string keyName, float value)
        {
            try
            {
                return IniFileHandler.SetIniFile_Value(filePath, sectionName, keyName, value.ToString());
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Response SetIniFile_Value(this string filePath, string sectionName, string keyName, double value)
        {
            try
            {
                return IniFileHandler.SetIniFile_Value(filePath, sectionName, keyName, value.ToString());
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static Dictionary<string, Dictionary<string, string>> ToIniDictionary(this string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var temp = lines.Select((Line, No) => new { No, Line }).ToList();
            var sList = temp.Where(t => string.IsNullOrEmpty(t.Line) == false && (t.Line[0] == '[' && t.Line[t.Line.Length - 1] == ']')).ToList();

            var list = new Dictionary<string, Dictionary<string, string>>();

            for (int i = 0; i < sList.Count; i++)
            {
                var key = sList[i].Line.Replace("[", "").Replace("]", "");

                list.Add(key, new Dictionary<string, string>());

                if (i == (sList.Count - 1))
                {
                    var start = sList[i].No + 1;
                    var end = lines.Length;

                    for (int k = start; k < end; k++)
                    {
                        var str = lines[k];
                        if (string.IsNullOrWhiteSpace(str)) continue;

                        var item = str.Split(new string[] { "=" }, StringSplitOptions.None);
                        if (item.Length <= 1) continue;

                        list[key].Add(item[0], item[1]);
                    }
                }
                else
                {
                    var start = sList[i].No + 1;
                    var end = sList[i + 1].No;

                    for (int k = start; k < end; k++)
                    {
                        var str = lines[k];
                        if (string.IsNullOrWhiteSpace(str)) continue;

                        var item = str.Split(new string[] { "=" }, StringSplitOptions.None);
                        if (item.Length <= 1) continue;

                        list[key].Add(item[0], item[1]);
                    }
                }
            }

            return list;
        }
    }
}
