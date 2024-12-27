using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TS.FW.Plc.Dto.Packet;
using TS.FW.Plc.Dto.Packet.Mitsubishi;
using TS.FW.Utils;

namespace TS.FW.Plc.Dto
{
    public class PlcWord : IPlcDataBase
    {
        public int Length { get; private set; }

        public PlcWordDataType Type { get; private set; }

        public PlcDeviceCode Code { get; private set; }

        public object Value { get; set; }

        public object WriteData { get; set; }

        public PlcWord(int address, string name, int length, PlcWordDataType type, PlcDeviceCode code) : base(address, name)
        {
            this.Length = length;
            this.Type = type;            
            this.Code = code;
            this.Value = this.InitValue(this.Type);
        }

        public PlcWord(int address, string name, int length, PlcWordDataType type, PlcDeviceCode code, object value) : this(address, name, length, type, code)
        {
            this.Value = value;
        }

        public string ToAddressMxComponent()
        {
            return this.Code == PlcDeviceCode.R_FILE_ACCESS_REGISTER ? this.Address.ToString() : this.Address.ToString("X");
        }

        public PlcWord ToPlcWordAddressOffset(int offset)
        {
            return new PlcWord(this.Address + offset, this.Name, this.Length, this.Type, this.Code, this.Value);
        }

        public PlcWirteWordModel ToPlcWirteWordModel(object value)
        {
            return new PlcWirteWordModel(this, value);
        }

        public object ToDataParse(byte[] buffer, ref int index)
        {
            object value = null;

            if (this.Type == PlcWordDataType.Number)
            {
                switch (this.Length)
                {
                    case 1:
                        {
                            value = buffer.ToInt16(index);
                        }
                        break;
                    case 2:
                        {
                            value = buffer.ToInt32(index);
                        }
                        break;
                }
            }
            else if (this.Type == PlcWordDataType.LIST)
            {
                value = buffer.Skip(index).Take(this.Length * 2).ToArray();
            }
            else if (this.Type == PlcWordDataType.Double)
            {
                switch (this.Length)
                {
                    case 2:
                        {
                            value = buffer.ToFloat(index);
                        }
                        break;
                    case 4:
                        {
                            value = buffer.ToDouble(index);
                        }
                        break;
                }
            }
            else
            {
                value = Encoding.ASCII.GetString(buffer.Skip(index).Take(this.Length * 2).ToArray()).Replace("\0", " ");
            }

            if (value == null)
                throw new IndexOutOfRangeException(string.Format("Word 데이터 파싱 도중 에러가 발생하였습니다. {0} Type : {1} Length {2}", this.ToAddress(), this.Type, this.Length));

            index += (this.Length * 2);

            return value;
        }

        public byte[] ToByte(object value)
        {
            if (this.Type == PlcWordDataType.Number)
            {
                // 숫자형 데이터
                switch (this.Length)
                {
                    case 1:
                        {
                            return Convert.ToInt16(value).ToByte();
                        }
                    case 2:
                        {
                            return Convert.ToInt32(value).ToByte();
                        }
                    default:
                        {
                            throw new IndexOutOfRangeException(string.Format("Word 데이터 파싱 도중 에러가 발생하였습니다. 데이터 유형이 byte[]가 아닙니다. {0} Type : {1} Length {2}"
                                    , this.ToAddress(), this.Type, this.Length));
                        }
                }
            }
            else if(this.Type == PlcWordDataType.LIST)
            {
                if (value is IEnumerable<byte>)
                {
                    var buffer = value as IEnumerable<byte>;

                    if (buffer.Count() > this.Length * 2)
                    {
                        throw new IndexOutOfRangeException(string.Format("Word 데이터 파싱 도중 에러가 발생하였습니다. 입력하는 byte[] 길이가 큽니다. {0} Type : {1} Length {2}"
                        , this.ToAddress(), this.Type, this.Length));
                    }

                    return buffer.ToArray();
                }
                else
                {
                    throw new IndexOutOfRangeException(string.Format("Word 데이터 파싱 도중 에러가 발생하였습니다. 데이터 유형이 byte[]가 아닙니다. {0} Type : {1} Length {2}"
                        , this.ToAddress(), this.Type, this.Length));
                }
            }
            else if (this.Type == PlcWordDataType.Double && this.Length > 1)
            {
                // 소수점 데이터
                switch (this.Length)
                {
                    case 2:
                        {
                            return Convert.ToSingle(value).ToByte();
                        }
                    case 4:
                        {
                            return Convert.ToDouble(value).ToByte();
                        }
                    default: // TODO : Number 형식의 데이터가 아닐 경우가 농후 함..
                        throw new IndexOutOfRangeException(string.Format("Word 데이터 파싱 도중 에러가 발생하였습니다. {0} Type : {1} Length {2}", this.ToAddress(), this.Type, this.Length));
                }
            }
            else
            {
                // 문자형 데이터 - ASCII 코드                
                if (this.Length > 1)
                {
                    // 다수의 문자열 전송
                    return this.ToAsciiString(Convert.ToString(value), this.Length).ToArray();
                }
                else
                {
                    // 하나의 문자열만 전송
                    return this.ToAsciiString(Convert.ToString(value), this.Length).Take(2).ToArray();
                }
            }
        }

        public override string ToAddress()
        {
            return string.Format("{0}[{1}]", this.Name, this.Code == PlcDeviceCode.R_FILE_ACCESS_REGISTER ? this.Address.ToString() : this.Address.ToHex());
        }

        public override string AddressString()
        {
            return this.Code == PlcDeviceCode.R_FILE_ACCESS_REGISTER ? this.Address.ToString() : base.AddressString();
        }

        public override string ToString()
        {
            return string.Format("{0} : {1}\r\nTYPE : {2}[{3}]\r\nCode : {4}", base.ToString(), this.ToValueString(), this.Type, this.Length, this.Code);
        }

        public override string ToValueString()
        {
            if (this.Value == null) return string.Empty;

            if (this.Value is IEnumerable<byte>) return (this.Value as IEnumerable<byte>).ToHex();

            return this.Value.ToString();
        }

        public override int GetHashCode()
        {
            return string.Format("{0}_{1}", this.Address, this.Code).GetHashCode();
        }

        private IEnumerable<byte> ToAsciiString(string text, int length)
        {
            if (text.Length > length * 2) text = text.Substring(0, length * 2);

            return Encoding.ASCII.GetBytes(text.PadRight(length * 2, ' '));
        }

        private object InitValue(PlcWordDataType type)
        {
            switch (type)
            {
                case PlcWordDataType.Number: return 0;
                case PlcWordDataType.Double: return 0.0D;
                case PlcWordDataType.ASCII: return string.Empty;
                case PlcWordDataType.LIST: return new byte[this.Length * 2];
                default: return null;
            }
        }
    }

    public class PlcWirteWordModel : PlcWord
    {
        public object WirteValue { get; set; }

        public PlcWirteWordModel(PlcWord word, object value) : base(word.Address, word.Name, word.Length, word.Type, word.Code, word.Value)
        {
            this.WirteValue = value;
        }
    }
}
