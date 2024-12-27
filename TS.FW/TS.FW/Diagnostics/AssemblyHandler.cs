using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Diagnostics
{
    public static class AssemblyHandler
    {
        /// <summary>
        /// 어셈블리 정보 가져오기
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static FileVersionInfo GetFileVersionInfo(this Type type)
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetAssembly(type).ManifestModule.FullyQualifiedName);
        }

        /// <summary>
        /// 어셈블리 정보 제품 문자열 가져오기
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ProductName(this Type type)
        {
            return type.GetFileVersionInfo().ProductName;
        }

        /// <summary>
        /// 어셈블리 정보 버전 문자열 가져오기
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string FileVersion(this Type type)
        {
            return type.GetFileVersionInfo().FileVersion;
        }

        /// <summary>
        /// 어셈블리 정보 저작권 문자열 가져오기
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string FileLegalCopyright(this Type type)
        {
            return type.GetFileVersionInfo().LegalCopyright;
        }

        /// <summary>
        /// 어셈블리 정보 설명 가져오기
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string FileDescription(this Type type)
        {
            return (Assembly.GetAssembly(type).GetCustomAttribute(typeof(AssemblyDescriptionAttribute)) as AssemblyDescriptionAttribute).Description;
        }

        /// <summary>
        /// 어셈블리 빌드 날짜 정보 가져오기
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DateTime FileBulidData(this Type type)
        {
            var info = Assembly.GetAssembly(type).GetName().Version;

            return new DateTime(2000, 1, 1).AddDays(info.Build).AddSeconds(info.Revision * 2);
        }
    }
}
