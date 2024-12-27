using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Reflection;

namespace TS.FW.Utils
{
    public static class CollectionsHandler
    {
        /// <summary>
        /// 연속 되는 데이터 그룹화
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> ToContinuousList<T>(this IEnumerable<T> list, Func<T, int> keySelector, Func<T, int> indexSelector)
        {
            if (list.Count() > 0)
            {
                var orderByList = list.OrderBy(keySelector);
                var firsetItem = orderByList.First();

                var temp = new List<T>();
                temp.Add(firsetItem);

                var index = indexSelector(firsetItem);

                foreach (var item in orderByList.Skip(1))
                {
                    if (keySelector(item) == (index + 1))
                    {
                        temp.Add(item);
                        index = indexSelector(item);
                    }
                    else
                    {
                        yield return temp;

                        temp = new List<T>();
                        temp.Add(item);
                        index = indexSelector(item);
                    }
                }

                if (temp.Count > 0) yield return temp;
            }
        }

        /// <summary>
        /// 페이지 처리
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="count"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToPageList<T>(this IEnumerable<T> list, int count, int page)
        {
            var length = list.Count();
            var skipCount = count * page;

            if (skipCount < 0) skipCount = count;
            else if (length <= skipCount) skipCount = count * (length / count);

            return list.Skip(skipCount).Take(count);
        }

        /// <summary>
        /// 페이지 처리
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> ToPageList<T>(this IEnumerable<T> list, int count)
        {
            if (list.Count() == count)
            {
                yield return list;
            }
            else
            {
                var length = list.Count() / count;

                for (int i = 0; i <= length; i++)
                {
                    yield return list.Skip(i * count).Take(count);
                }
            }
        }

        /// <summary>
        /// 범위 지정 처리
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="list"></param>
        /// <param name="keySelector"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToBetweenList<T, TKey>(this IEnumerable<T> list, Func<T, TKey> keySelector, T from, T to)
        {
            var temp = list.OrderBy(keySelector).SkipWhile(t => t.Equals(from) == false).TakeWhile(t => t.Equals(to) == false).ToList();
            temp.Add(to);

            return temp;
        }

        public static IEnumerable<IEnumerable<T>> ToLengthList<T>(this IEnumerable<T> list, Func<T, int> keySelector, int maxLength)
        {
            if (list.Count() > 0)
            {
                var firsetItem = list.First();

                var temp = new List<T>();
                temp.Add(firsetItem);

                var length = keySelector(firsetItem);

                foreach (var item in list.Skip(1))
                {
                    length += keySelector(item);

                    if (length <= maxLength)
                    {
                        temp.Add(item);
                    }
                    else
                    {
                        yield return temp;

                        temp = new List<T>();
                        temp.Add(item);
                        length = keySelector(item);
                    }
                }

                if (temp.Count > 0) yield return temp;
            }
        }

        public static string ToCsv<T>(this IEnumerable<T> list) where T : class
        {
            var type = typeof(T);
            var pList = type.GetProperties();

            var tList = new List<CsvInfo>();

            foreach (var info in pList)
            {
                var at = info.GetCustomAttribute<DataMemberAttribute>();
                if (at == null) continue;

                tList.Add(new CsvInfo(at, info));
            }

            var csv = new List<string>();

            var oList = tList.OrderBy(t => t.Order).ToList();
            csv.Add(string.Join(",", oList.Select(t => t.Name)));

            foreach (var item in list)
            {
                var content = string.Join(",", oList.Select(t => t.GetValue(item)));
                csv.Add(content);
            }

            return string.Join(Environment.NewLine, csv);
        }

        /// <summary>
        /// 20231109. bjjung. 클래스 전달 시 csv 형식으로 데이터 저장을 위해 추가함.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ToCsv<T>(this T list, bool withheader, bool withvalue) where T : class
        {
            var type = typeof(T);
            var pList = type.GetProperties();

            var tList = new List<CsvInfo>();

            foreach (var info in pList)
            {
                var at = info.GetCustomAttribute<DataMemberAttribute>();
                if (at == null) continue;
                tList.Add(new CsvInfo(at, info));
            }

            var csv = new List<string>();
            var oList = tList.ToList();

            // 신규파일 생성 시 헤더 포함
            if (withheader == true)
                csv.Add(string.Join(",", oList.Select(t => t.Name)));

            var content = string.Join(",", oList.Select(t => t.GetValue(list)));
            if (withvalue == true)
                csv.Add(content);

            return string.Join(Environment.NewLine, csv);
        }
    }

    internal class CsvInfo
    {
        public int Order { get; set; }

        public string Name { get; set; }

        public PropertyInfo Info { get; set; }

        public CsvInfo(DataMemberAttribute at, PropertyInfo info)
        {
            this.Order = at.Order;
            this.Info = info;
            this.Name = string.IsNullOrWhiteSpace(at.Name) ? info.Name : at.Name;
        }

        public string GetValue(object item)
        {
            return Info.GetValue(item).ToString();
        }

        public override string ToString()
        {
            return $"{this.Order} {this.Name}";
        }
    }
}
