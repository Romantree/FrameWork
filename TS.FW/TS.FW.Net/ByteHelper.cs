using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Net
{
    public class ByteHelper
    {
        private IList<byte> data = new List<byte>();
        public DateTime Time
        {
            get;
            private set;
        }
        public int RetryCount
        {
            get;
            private set;
        }
        public ByteHelper()
        {
            RetryCount = 0;
        }

        /// <summary>
        /// Message 송신 된 시간 확인용.
        /// </summary>
        public void SetTime()
        {
            Time = DateTime.Now;
            RetryCount++;
        }

        public void Append(UInt16 b)
        {
            byte[] array = new byte[2];
            array[1] = (byte)(b & 0xff);
            array[0] = (byte)((b >> 8) & 0xff);
            data.Add(array[0]);
            data.Add(array[1]);
        }

        public void Append(string str)
        {
            var bb = Encoding.ASCII.GetBytes(str);
            foreach (var b in bb)
            {
                data.Add(b);
            }
        }

        public void Append(byte b)
        {
            data.Add(b);
        }

        public void Append(byte[] bb)
        {
            foreach (var b in bb)
            {
                data.Add(b);
            }
        }

        public byte[] ToReverseByte()
        {
            var ret = new byte[data.Count];
            //int i = 0;
            //foreach (var b in data)
            for (int i = 0; i < data.Count; i += 2)
            {
                if (data.Count == i + 1)
                {
                    ret[i] = data[i];
                    break;
                }
                ret[i] = data[i + 1];
                ret[i + 1] = data[i];
            }
            return ret;
        }

        public byte[] ToByte()
        {
            var ret = new byte[data.Count];
            int i = 0;
            foreach (var b in data)
            {
                ret[i++] = b;
            }
            return ret;
        }

        //public UInt16[] LRC_ToInt16(int ckLen = 4)
        //{
        //    var buf = new byte[data.Count - ckLen];
        //    Array.Copy(data.ToArray(), 4, buf, 0, data.Count - ckLen);

        //    var len = (buf.Length % 2 == 0) ? (buf.Length / 2) : (buf.Length / 2) + 1;
        //    var ret = new UInt16[len];
        //    for (int i = 0; i < len; i++)
        //    {
        //        var high = buf[i * 2] << (ushort)8;
        //        var low = 0x0000;
        //        if (buf.Length > (i * 2 + 1))
        //            low = buf[i * 2 + 1];

        //        ret[i] = (UInt16)(high + low);
        //    }

        //    return ret;
        //}

        //public UInt16 GetLRC_CheckSum()
        //{
        //    return (UInt16)(data[data.Count - 2] * 0x0100 + data[data.Count - 1]);
        //}

        public byte[] GetCheckSum()
        {
            var totalsum = 0;
            for (int i = 0; i < data.Count; i++)
            {
                totalsum += data[i];
            }

            var hexstr = totalsum.ToString("X");
            var len = hexstr.Length;
            var str = hexstr.Substring(len - 2, len - 1); //뒤에서 두자리만 자르기 
            return Encoding.ASCII.GetBytes(str);
        }

        public byte GetCheckSumEx()
        {
            var totalsum = 0;
            for (int i = 0; i < data.Count; i++)
            {
                totalsum += data[i];
            }


            return (byte)(totalsum & 0xFF);
        }

        public override string ToString()
        {

            return string.Format("{0}", BitConverter.ToString(ToByte()));
        }
    }
}
