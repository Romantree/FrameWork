using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using TS.FW.Utils;
using TS.FW.XCom.Data;
using XCOMLib;

namespace TS.FW.XCom
{
    public abstract class IXComData
    {
        public static Encoding Encoding { get; set; } = Encoding.Default;

        public eXComData Type { get; private set; }

        public abstract int Length { get; }

        public XComList List => this as XComList;

        public XComString ASCII => this as XComString;

        public XComBinary Binary => this as XComBinary;

        public XComBool Bool => this as XComBool;

        public XComINT1 INT1 => this as XComINT1;

        public XComINT2 INT2 => this as XComINT2;

        public XComINT4 INT4 => this as XComINT4;

        public XComINT8 INT8 => this as XComINT8;

        public XComUINT1 UINT1 => this as XComUINT1;

        public XComUINT2 UINT2 => this as XComUINT2;

        public XComUINT4 UINT4 => this as XComUINT4;

        public XComUINT8 UINT8 => this as XComUINT8;

        public XComFLOAT4 FLOAT4 => this as XComFLOAT4;

        public XComFLOAT8 FLOAT8 => this as XComFLOAT8;

        public IXComData(eXComData type)
        {
            this.Type = type;
        }

        public abstract void SetData(XCOM xcom, int msgID);

        public abstract string ToJson(string name);

        public abstract string ToJson();

        public override string ToString()
        {
            return string.Format("{0},\t{1}", this.Type, this.Length);
        }

        public static IXComData ToXComData(byte[] buffer)
        {
            if (buffer == null || buffer.Length <= 0) return null;

            var index = 0;
            return ToXComData(buffer, ref index);
        }

        private static IXComData ToXComData(byte[] buffer, ref int index)
        {
            MakeHeader(buffer, ref index, out eXComData type, out int length);

            switch (type)
            {
                case eXComData.LIST:
                    {
                        var item = new XComList();

                        for (int i = 0; i < length; i++)
                        {
                            item.Values.Add(ToXComData(buffer, ref index));
                        }

                        return item;
                    }
                case eXComData.BINARY:
                    {
                        var item = new XComBinary();

                        for (int i = 0; i < length; i++)
                        {
                            item.Values.Add(buffer[index]);
                            index += 1;
                        }

                        return item;
                    }
                case eXComData.BOOL:
                    {
                        var item = new XComBool();

                        for (int i = 0; i < length; i++)
                        {
                            item.Values.Add(Convert.ToBoolean(buffer[index]));
                            index += 1;
                        }

                        return item;
                    }
                case eXComData.ASCII:
                    {
                        var item = new XComString();

                        item.Value = Encoding.GetString(buffer, index, length);
                        index += length;

                        return item;
                    }
                case eXComData.INT1:
                    {
                        var item = new XComINT1();

                        for (int i = 0; i < length; i++)
                        {
                            item.Values.Add(buffer[index]);
                            index += 1;
                        }

                        return item;
                    }
                case eXComData.INT2:
                    {
                        var item = new XComINT2();

                        for (int i = 0; i < (length / 2); i++)
                        {
                            item.Values.Add(buffer.ToInt16(index, BitHandler.ByteOrder.BigEndian));
                            index += 2;
                        }

                        return item;
                    }
                case eXComData.INT4:
                    {
                        var item = new XComINT4();

                        for (int i = 0; i < (length / 4); i++)
                        {
                            item.Values.Add(buffer.ToInt32(index, BitHandler.ByteOrder.BigEndian));
                            index += 4;
                        }

                        return item;
                    }
                case eXComData.INT8:
                    {
                        var item = new XComINT8();

                        for (int i = 0; i < (length / 8); i++)
                        {
                            item.Values.Add(buffer.ToInt64(index, BitHandler.ByteOrder.BigEndian));
                            index += 8;
                        }

                        return item;
                    }
                case eXComData.UINT1:
                    {
                        var item = new XComUINT1();

                        for (int i = 0; i < length; i++)
                        {
                            item.Values.Add(buffer[index]);
                            index += 1;
                        }

                        return item;
                    }
                case eXComData.UINT2:
                    {
                        var item = new XComUINT2();

                        for (int i = 0; i < (length / 2); i++)
                        {
                            item.Values.Add(buffer.ToUInt16(index, BitHandler.ByteOrder.BigEndian));
                            index += 2;
                        }

                        return item;
                    }
                case eXComData.UINT4:
                    {
                        var item = new XComUINT4();

                        for (int i = 0; i < (length / 4); i++)
                        {
                            item.Values.Add(buffer.ToUInt32(index, BitHandler.ByteOrder.BigEndian));
                            index += 4;
                        }

                        return item;
                    }
                case eXComData.UINT8:
                    {
                        var item = new XComUINT8();

                        for (int i = 0; i < (length / 8); i++)
                        {
                            item.Values.Add(buffer.ToUInt64(index, BitHandler.ByteOrder.BigEndian));
                            index += 8;
                        }

                        return item;
                    }
                case eXComData.FLOAT4:
                    {
                        var item = new XComFLOAT4();

                        for (int i = 0; i < (length / 4); i++)
                        {
                            item.Values.Add(buffer.ToFloat(index, BitHandler.ByteOrder.BigEndian));
                            index += 4;
                        }

                        return item;
                    }
                case eXComData.FLOAT8:
                    {
                        var item = new XComFLOAT8();

                        for (int i = 0; i < (length / 8); i++)
                        {
                            item.Values.Add(buffer.ToDouble(index, BitHandler.ByteOrder.BigEndian));
                            index += 8;
                        }

                        return item;
                    }
                default:
                    throw new TypeAccessException(string.Format("정의 되지 않은 유형의 데이터 입니다. DataType = {0}", type));
            }
        }

        public static string ToXComLog(IXComData data)
        {
            if (data == null) return string.Empty;

            return ToXComLog(data, 0);
        }

        public static string ToXComLog(IXComData data, int depth)
        {
            var sb = new StringBuilder();

            sb.AppendLine(string.Format("{0}{1}", ToDepth(depth), data));

            switch (data.Type)
            {
                case eXComData.LIST:
                    {
                        var item = data as XComList;

                        foreach (var sub in item.Values)
                        {
                            sb.Append(ToXComLog(sub, depth + 1));
                        }
                    }
                    break;
            }

            return sb.ToString();
        }

        private static string ToDepth(int count)
        {
            var str = string.Empty;

            for (int i = 0; i < count; i++)
            {
                str += "  ";
            }

            return str;
        }

        private static void MakeHeader(byte[] buffer, ref int index, out eXComData type, out int lenght)
        {
            var count = buffer[index] & 0x03;

            type = (eXComData)((buffer[index] & 0xFC) >> 2);

            var temp = buffer.Skip(index + 1).Take(count).ToList();

            for (int i = 0; i < 4 - count; i++)
            {
                temp.Insert(0, 0x00);
            }

            lenght = temp.ToArray().ToInt32(0, BitHandler.ByteOrder.BigEndian);

            index += (count + 1);
        }

        public static implicit operator IXComData(byte[] buffer)
        {
            return ToXComData(buffer);
        }
    }

    public abstract class IXComData<T> : IXComData
    {
        public List<T> Values { get; set; } = new List<T>();

        public override int Length => (this.Values == null || this.Values.Count <= 0) ? 0 : this.Values.Count;

        public T Value => (this.Values == null || this.Values.Count <= 0) ? default(T) : this.Values[0];

        public IXComData(eXComData type) : base(type) { }

        public override string ToJson(string name)
        {
            return string.Format("\"{0}\":{1}", name, this.ToJson());
        }

        public override string ToJson()
        {
            if(this.Values.Count <= 1)
            {
                return string.Format("{0}", this.Value);
            }
            else
            {
                return string.Format("[{0}]", string.Join(",", this.Values.Select(t => t.ToString())));
            }
        }

        public override string ToString()
        {
            return string.Format("{0},\t<{1}>", base.ToString(), string.Join(" ", this.Values));
        }
    }
}