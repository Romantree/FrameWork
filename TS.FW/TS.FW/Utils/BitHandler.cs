using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Utils
{
    public static class BitHandler
    {
        public enum ByteOrder
        {
            LittleEndian,
            Mid_LittileEndian,
            BigEndian,
            Mid_BigEndian,
        }

        #region ToByte

        //public static byte[] ToByte(this short[] value, ByteOrder order = ByteOrder.LittleEndian)
        //{

        //}

        public static byte[] ToByte(this short value, ByteOrder order = ByteOrder.LittleEndian)
        {
            return ((ushort)value).ToByte(order);
        }

        public static byte[] ToByte(this ushort value, ByteOrder order = ByteOrder.LittleEndian)
        {
            switch (order)
            {
                case ByteOrder.LittleEndian:
                case ByteOrder.Mid_LittileEndian:
                    {
                        //BA
                        return BitConverter.GetBytes(value);
                    }
                case ByteOrder.BigEndian:
                case ByteOrder.Mid_BigEndian:
                    {
                        //AB
                        return BitConverter.GetBytes(value).Reverse().ToArray();
                    }
            }

            throw new NotImplementedException(string.Format("지원하지 않는 ByteOrder 입니다. {0}", order));
        }

        public static byte[] ToByte(this int value, ByteOrder order = ByteOrder.LittleEndian)
        {
            return ((uint)value).ToByte(order);
        }

        public static short ToInt16(byte[] buffer, ref int index, object oRDER)
        {
            throw new NotImplementedException();
        }

        public static byte[] ToByte(this uint value, ByteOrder order = ByteOrder.LittleEndian)
        {
            switch (order)
            {
                case ByteOrder.LittleEndian:
                    {
                        // DC BA
                        return BitConverter.GetBytes(value);
                    }
                case ByteOrder.Mid_LittileEndian:
                    {
                        // 16Bit 통신 기준으로 코딩 됨... 32bit 통신 이상일 경우 버그 발생 우려
                        // CD AB
                        return (((value & 0x0000FFFF) << 16) | ((value & 0xFFFF0000) >> 16)).ToByte(ByteOrder.BigEndian);
                    }
                case ByteOrder.BigEndian:
                    {
                        // AB CD
                        return BitConverter.GetBytes(value).Reverse().ToArray();
                    }
                case ByteOrder.Mid_BigEndian:
                    {
                        // 16Bit 통신 기준으로 코딩 됨... 32bit 통신 이상일 경우 버그 발생 우려
                        // BA DC
                        return (((value & 0x0000FFFF) << 16) | ((value & 0xFFFF0000) >> 16)).ToByte(ByteOrder.LittleEndian);
                    }
            }

            throw new NotImplementedException(string.Format("지원하지 않는 ByteOrder 입니다. {0}", order));
        }

        public static byte[] ToByte(this long value, ByteOrder order = ByteOrder.LittleEndian)
        {
            return ((ulong)value).ToByte(order);
        }

        public static byte[] ToByte(this ulong value, ByteOrder order = ByteOrder.LittleEndian)
        {
            switch (order)
            {
                case ByteOrder.LittleEndian:
                    {
                        // HG FE DC BA
                        return BitConverter.GetBytes(value);
                    }
                case ByteOrder.Mid_LittileEndian:
                    {
                        // 16Bit 통신 기준으로 코딩 됨... 32bit 통신 이상일 경우 버그 발생 우려
                        // GH EF CD AB
                        return (((value & 0x000000000000FFFF) << 48)
                            | ((value & 0x00000000FFFF0000) << 16)
                            | ((value & 0x0000FFFF00000000) >> 16)
                            | ((value & 0xFFFF000000000000) >> 48))
                            .ToByte(ByteOrder.BigEndian);
                    }
                case ByteOrder.BigEndian:
                    {
                        // AB CD EF GH
                        return BitConverter.GetBytes(value).Reverse().ToArray();
                    }
                case ByteOrder.Mid_BigEndian:
                    {
                        // 16Bit 통신 기준으로 코딩 됨... 32bit 통신 이상일 경우 버그 발생 우려
                        // BA DC FE HG
                        return (((value & 0x000000000000FFFF) << 48)
                            | ((value & 0x00000000FFFF0000) << 16)
                            | ((value & 0x0000FFFF00000000) >> 16)
                            | ((value & 0xFFFF000000000000) >> 48))
                            .ToByte(ByteOrder.LittleEndian);
                    }
            }

            throw new NotImplementedException(string.Format("지원하지 않는 ByteOrder 입니다. {0}", order));
        }

        public static byte[] ToByte(this float value, ByteOrder order = ByteOrder.LittleEndian)
        {
            switch (order)
            {
                case ByteOrder.LittleEndian:
                case ByteOrder.Mid_LittileEndian:
                    {
                        return BitConverter.GetBytes(value);
                    }
                case ByteOrder.BigEndian:
                case ByteOrder.Mid_BigEndian:
                    {
                        return BitConverter.GetBytes(value).Reverse().ToArray();
                    }
            }

            throw new NotImplementedException(string.Format("지원하지 않는 ByteOrder 입니다. {0}", order));
        }

        public static byte[] ToByte(this double value, ByteOrder order = ByteOrder.LittleEndian)
        {
            switch (order)
            {
                case ByteOrder.LittleEndian:
                case ByteOrder.Mid_LittileEndian:
                    {
                        return BitConverter.GetBytes(value);
                    }
                case ByteOrder.BigEndian:
                case ByteOrder.Mid_BigEndian:
                    {
                        return BitConverter.GetBytes(value).Reverse().ToArray();
                    }
            }

            throw new NotImplementedException(string.Format("지원하지 않는 ByteOrder 입니다. {0}", order));
        }

        public static short[] ToShort(this int value, ByteOrder order = ByteOrder.LittleEndian)
        {
            switch(order)
            {
                case ByteOrder.LittleEndian:
                case ByteOrder.Mid_LittileEndian:
                    {
                        var s1 = (short)(value & 0xFFFF);
                        var s2 = (short)(value >> 16);
                        return new short[2] { s1, s2 };
                    }
                case ByteOrder.BigEndian:
                case ByteOrder.Mid_BigEndian:
                    {

                        var s1 = (short)(value & 0xFFFF);
                        var s2 = (short)(value >> 16);
                        return new short[2] { s2, s1 };
                    }
            }
            throw new NotImplementedException(string.Format("지원하지 않는 ByteOrder 입니다. {0}", order));
        }

        #endregion

        #region ToConverter

        public static IEnumerable<short> ToInt16List(this byte[] buffer, int index, int count, ByteOrder order = ByteOrder.LittleEndian)
        {
            for (int i = 0; i < count; i++)
            {
                yield return buffer.ToInt16(ref index, order);
            }
        }

        public static short ToInt16(this byte[] buffer, int index, ByteOrder order = ByteOrder.LittleEndian)
        {
            switch (order)
            {
                case ByteOrder.LittleEndian:
                case ByteOrder.Mid_LittileEndian:
                    {
                        return BitConverter.ToInt16(buffer, index);
                    }
                case ByteOrder.BigEndian:
                case ByteOrder.Mid_BigEndian:
                    {
                        return buffer.Skip(index).Take(2).Reverse().ToArray().ToInt16(0);
                    }
            }

            throw new NotImplementedException(string.Format("지원하지 않는 ByteOrder 입니다. {0}", order));
        }

        public static ushort ToUInt16(this byte[] buffer, int index, ByteOrder order = ByteOrder.LittleEndian)
        {
            switch (order)
            {
                case ByteOrder.LittleEndian:
                case ByteOrder.Mid_LittileEndian:
                    {
                        return BitConverter.ToUInt16(buffer, index);
                    }
                case ByteOrder.BigEndian:
                case ByteOrder.Mid_BigEndian:
                    {
                        return buffer.Skip(index).Take(2).Reverse().ToArray().ToUInt16(0);
                    }
            }

            throw new NotImplementedException(string.Format("지원하지 않는 ByteOrder 입니다. {0}", order));
        }

        public static int ToInt32(this byte[] buffer, int index, ByteOrder order = ByteOrder.LittleEndian)
        {
            switch (order)
            {
                case ByteOrder.LittleEndian:
                    {
                        return BitConverter.ToInt32(buffer, index);
                    }
                case ByteOrder.Mid_LittileEndian:
                    {
                        var temp = buffer.Skip(index).Take(4).ToArray();
                        return new byte[] { temp[1], temp[0], temp[3], temp[2] }.ToInt32(0);
                    }
                case ByteOrder.BigEndian:
                    {
                        return buffer.Skip(index).Take(4).Reverse().ToArray().ToInt32(0);
                    }
                case ByteOrder.Mid_BigEndian:
                    {
                        var temp = buffer.Skip(index).Take(4).ToArray();
                        return new byte[] { temp[2], temp[3], temp[0], temp[1] }.ToInt32(0);
                    }
            }

            throw new NotImplementedException(string.Format("지원하지 않는 ByteOrder 입니다. {0}", order));
        }

        public static uint ToUInt32(this byte[] buffer, int index, ByteOrder order = ByteOrder.LittleEndian)
        {
            switch (order)
            {
                case ByteOrder.LittleEndian:
                    {
                        return BitConverter.ToUInt32(buffer, index);
                    }
                case ByteOrder.Mid_LittileEndian:
                    {
                        var temp = buffer.Skip(index).Take(4).ToArray();
                        return new byte[] { temp[1], temp[0], temp[3], temp[2] }.ToUInt32(0);
                    }
                case ByteOrder.BigEndian:
                    {
                        return buffer.Skip(index).Take(4).Reverse().ToArray().ToUInt32(0);
                    }
                case ByteOrder.Mid_BigEndian:
                    {
                        var temp = buffer.Skip(index).Take(4).ToArray();
                        return new byte[] { temp[2], temp[3], temp[0], temp[1] }.ToUInt32(0);
                    }
            }

            throw new NotImplementedException(string.Format("지원하지 않는 ByteOrder 입니다. {0}", order));
        }

        public static long ToInt64(this byte[] buffer, int index, ByteOrder order = ByteOrder.LittleEndian)
        {
            switch (order)
            {
                case ByteOrder.LittleEndian:
                    {
                        return BitConverter.ToInt64(buffer, index);
                    }
                case ByteOrder.Mid_LittileEndian:
                    {
                        // GH EF CD AB
                        var temp = buffer.Skip(index).Take(8).ToArray();
                        return new byte[] { temp[1], temp[0], temp[3], temp[2], temp[5], temp[4], temp[7], temp[6] }.ToInt64(0);
                    }
                case ByteOrder.BigEndian:
                    {
                        return buffer.Skip(index).Take(8).Reverse().ToArray().ToInt64(0);
                    }
                case ByteOrder.Mid_BigEndian:
                    {
                        // BA DC FE HG
                        var temp = buffer.Skip(index).Take(8).ToArray();
                        return new byte[] { temp[6], temp[7], temp[4], temp[5], temp[2], temp[3], temp[0], temp[1] }.ToInt64(0);
                    }
            }

            throw new NotImplementedException(string.Format("지원하지 않는 ByteOrder 입니다. {0}", order));
        }

        public static ulong ToUInt64(this byte[] buffer, int index, ByteOrder order = ByteOrder.LittleEndian)
        {
            switch (order)
            {
                case ByteOrder.LittleEndian:
                    {
                        return BitConverter.ToUInt64(buffer, index);
                    }
                case ByteOrder.Mid_LittileEndian:
                    {
                        // GH EF CD AB
                        var temp = buffer.Skip(index).Take(8).ToArray();
                        return new byte[] { temp[1], temp[0], temp[3], temp[2], temp[5], temp[4], temp[7], temp[6] }.ToUInt64(0);
                    }
                case ByteOrder.BigEndian:
                    {
                        return buffer.Skip(index).Take(8).Reverse().ToArray().ToUInt64(0);
                    }
                case ByteOrder.Mid_BigEndian:
                    {
                        // BA DC FE HG
                        var temp = buffer.Skip(index).Take(8).ToArray();
                        return new byte[] { temp[6], temp[7], temp[4], temp[5], temp[2], temp[3], temp[0], temp[1] }.ToUInt64(0);
                    }
            }

            throw new NotImplementedException(string.Format("지원하지 않는 ByteOrder 입니다. {0}", order));
        }

        public static float ToFloat(this byte[] buffer, int index, ByteOrder order = ByteOrder.LittleEndian)
        {
            var arr1 = buffer.Skip(index).Take(2).ToArray();
            Array.Reverse(arr1);
            var arr2 = buffer.Skip(index + 2).Take(2).ToArray();
            Array.Reverse(arr2);

            switch (order)
            {
                case ByteOrder.LittleEndian:
                case ByteOrder.Mid_LittileEndian:
                    {
                        return BitConverter.ToSingle(arr2.Concat(arr1).ToArray(), 0);
                    }
                case ByteOrder.BigEndian:
                case ByteOrder.Mid_BigEndian:
                    {
                        return BitConverter.ToSingle(arr1.Concat(arr2).ToArray(), 0);
                        //return buffer.Skip(index).Take(4).Reverse().ToArray().ToFloat(0);
                    }
            }

            throw new NotImplementedException(string.Format("지원하지 않는 ByteOrder 입니다. {0}", order));
        }

        public static double ToDouble(this byte[] buffer, int index, ByteOrder order = ByteOrder.LittleEndian)
        {
            switch (order)
            {
                case ByteOrder.LittleEndian:
                case ByteOrder.Mid_LittileEndian:
                    {
                        return BitConverter.ToDouble(buffer, index);
                    }
                case ByteOrder.BigEndian:
                case ByteOrder.Mid_BigEndian:
                    {
                        return buffer.Skip(index).Take(8).Reverse().ToArray().ToDouble(0);
                    }
            }

            throw new NotImplementedException(string.Format("지원하지 않는 ByteOrder 입니다. {0}", order));
        }


        public static short ToInt16(this byte[] buffer, ref int index, ByteOrder order = ByteOrder.LittleEndian)
        {
            try
            {
                return buffer.ToInt16(index, order);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                index += 2;
            }
        }

        public static ushort ToUInt16(this byte[] buffer, ref int index, ByteOrder order = ByteOrder.LittleEndian)
        {
            try
            {
                return buffer.ToUInt16(index, order);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                index += 2;
            }
        }

        public static int ToInt32(this byte[] buffer, ref int index, ByteOrder order = ByteOrder.LittleEndian)
        {
            try
            {
                return buffer.ToInt32(index, order);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                index += 4;
            }
        }

        public static uint ToUInt32(this byte[] buffer, ref int index, ByteOrder order = ByteOrder.LittleEndian)
        {
            try
            {
                return buffer.ToUInt32(index, order);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                index += 4;
            }
        }

        public static long ToInt64(this byte[] buffer, ref int index, ByteOrder order = ByteOrder.LittleEndian)
        {
            try
            {
                return buffer.ToInt64(index, order);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                index += 8;
            }
        }

        public static ulong ToUInt64(this byte[] buffer, ref int index, ByteOrder order = ByteOrder.LittleEndian)
        {
            try
            {
                return buffer.ToUInt64(index, order);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                index += 8;
            }
        }

        public static float ToFloat(this byte[] buffer, ref int index, ByteOrder order = ByteOrder.LittleEndian)
        {
            try
            {
                return buffer.ToFloat(index, order);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                index += 4;
            }
        }

        public static double ToDouble(this byte[] buffer, ref int index, ByteOrder order = ByteOrder.LittleEndian)
        {
            try
            {
                return buffer.ToDouble(index, order);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                index += 8;
            }
        }

        #endregion

        #region ToString

        public static string ToHex(this IEnumerable<byte> list)
        {
            return string.Join(" ", list.Select(t => t.ToHex()));
        }

        public static string ToHex(this byte value)
        {
            return string.Format("0x{0}", Convert.ToString(value, 16).ToUpper().PadLeft(2, '0'));
        }

        public static string ToHex(this sbyte value)
        {
            return string.Format("0x{0}", Convert.ToString(value, 16).ToUpper().PadLeft(2, '0'));
        }

        public static string ToHex(this short value)
        {
            return string.Format("0x{0}", Convert.ToString(value, 16).ToUpper().PadLeft(4, '0'));
        }

        public static string ToHex(this ushort value)
        {
            return string.Format("0x{0}", Convert.ToString(value, 16).ToUpper().PadLeft(4, '0'));
        }

        public static string ToHex(this int value)
        {
            return string.Format("0x{0}", Convert.ToString(value, 16).ToUpper().PadLeft(8, '0'));
        }

        public static string ToHex(this uint value)
        {
            return string.Format("0x{0}", Convert.ToString(value, 16).ToUpper().PadLeft(8, '0'));
        }

        public static string ToHex(this long value)
        {
            return string.Format("0x{0}", Convert.ToString(value, 16).ToUpper().PadLeft(16, '0'));
        }

        public static string ToHex(this ulong value)
        {
            return string.Format("0x{0}", Convert.ToString((long)value, 16).ToUpper().PadLeft(16, '0'));
        }

        public static string ToBinary(this IEnumerable<byte> list)
        {
            return string.Join(" ", list.Select(t => t.ToBinary()));
        }

        public static string ToBinary(this byte value)
        {
            return Convert.ToString(value, 2).PadLeft(8, '0');
        }

        public static string ToBinary(this sbyte value)
        {
            return Convert.ToString(value, 2).PadLeft(8, '0');
        }

        public static string ToBinary(this short value)
        {
            return Convert.ToString(value, 2).PadLeft(16, '0');
        }

        public static string ToBinary(this ushort value)
        {
            return Convert.ToString(value, 2).PadLeft(16, '0');
        }

        public static string ToBinary(this int value)
        {
            return Convert.ToString(value, 2).PadLeft(32, '0');
        }

        public static string ToBinary(this uint value)
        {
            return Convert.ToString(value, 2).PadLeft(32, '0');
        }

        public static string ToBinary(this long value)
        {
            return Convert.ToString(value, 2).PadLeft(64, '0');
        }

        public static string ToBinary(this ulong value)
        {
            return Convert.ToString((long)value, 2).PadLeft(64, '0');
        }

        #endregion
    }
}
