using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TS.FW.Utils;

namespace TS.FW.Melsec
{
    public class MelsecDataList : List<MelsecData>
    {
        /// <summary>
        /// 사용하지 마시오.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public new MelsecData this[int address]
        {
            get
            {
                lock (this)
                {
                    var item = this.FirstOrDefault(t => t.Address == address);
                    if (item == null) return null;

                    return item;
                }
            }
        }

        /// <summary>
        /// Melsec Device Type의 주소에 해당하는 Melsec Data 추출
        /// </summary>
        /// <param name="devTyp"> LB / LW </param>
        /// <param name="address"> 원하는 Data의 시작 주소값 </param>
        /// <returns></returns>
        public MelsecData this[MDevType devTyp, int address]
        {
            get
            {
                lock (this)
                {
                    var item = this.FirstOrDefault(t => t.DevType == devTyp && t.Address == address);
                    if (item == null) return null;

                    return item;
                }
            }
        }

        public MelsecDataList() : base() { }

        public MelsecDataList(IEnumerable<MelsecData> list) : base(list) { }

        /// <summary>
        /// Melsec Data를 address 주소부터 length만큼 원하는 Data Type으로 Array로 추출
        /// </summary>
        /// <typeparam name="T"> 원하는 Data Type(ex : 정수형 - int)</typeparam>
        /// <param name="address"> 원하는 Data의 시작주소 </param>
        /// <param name="lenght"> 원하는 Data의 길이 </param>
        /// <returns></returns>
        public T ToConvert<T>(int address, int length = 1)
        {
            if (typeof(T).Equals(typeof(int)))
            {
                var buffer = this.ToData(address, 2).SelectMany(t => t.Data.ToByte()).ToArray();
                return (T)Convert.ChangeType(buffer.ToInt32(0), typeof(T));
            }
            else if (typeof(T).Equals(typeof(long)))
            {
                var buffer = this.ToData(address, 4).SelectMany(t => t.Data.ToByte()).ToArray();
                return (T)Convert.ChangeType(buffer.ToInt32(0), typeof(T));
            }
            else if (typeof(T).Equals(typeof(float)))
            {
                var buffer = this.ToData(address, 2).SelectMany(t => t.Data.ToByte()).ToArray();
                return (T)Convert.ChangeType(buffer.ToInt32(0) / 1000F, typeof(T));
            }
            else if (typeof(T).Equals(typeof(double)))
            {
                var buffer = this.ToData(address, 4).SelectMany(t => t.Data.ToByte()).ToArray();
                return (T)Convert.ChangeType(buffer.ToInt64(0) / 1000D, typeof(T));
            }
            else if (typeof(T).Equals(typeof(string)))
            {
                var buffer = this.ToData(address, length).SelectMany(t => t.Data.ToByte()).Where(t => t != 0).ToArray();
                return (T)Convert.ChangeType(Encoding.ASCII.GetString(buffer), typeof(T));
            }
            else if (typeof(T).Equals(typeof(bool)))
            {
                return (T)Convert.ChangeType(this[MDevType.LW, address].Data == 1, typeof(T));
            }
            else if (typeof(T).Equals(typeof(short)))
            {
                return (T)Convert.ChangeType(this[MDevType.LW, address].Data, typeof(T));
            }

            return default(T);
        }

        /// <summary>
        /// Melsec Data를 address 주소부터  Device Type이 일치하는 Data만 count 만큼 return
        /// address 주소값에 해당하는 Data 중 변경된 Data만 return
        /// </summary>
        /// <param name="address"> 시작 Melsec 주소 </param>
        /// <param name="count"> Melsec Data의 길이 </param>
        /// <param name="devType"> Melsec Data Type : LB / LW </param>
        /// <returns></returns>
        public IEnumerable<MelsecData> ToData(int address, int count, MDevType devType = MDevType.LW)
        {
            return this.Where(t => t.DevType == devType).OrderBy(t => t.Address).SkipWhile(t => t.Address != address).Take(count);
        }

        public bool CheckItem(int address, int length, MDevType devType = MDevType.LW)
        {
            var list = this.ToData(address, length, devType).ToList();
            if (list == null || list.Count <= 0 || list.Count != length) return false;

            var temp1 = list.Sum(t => (long)t.Address);
            var temp2 = this.Sum(address, address + length - 1);

            return temp1 == temp2;
        }

        private long Sum(int min, int max)
        {
            long value = 0;

            for (int i = min; i <= max; i++)
            {
                value += i;
            }

            return value;
        }
    }
}
