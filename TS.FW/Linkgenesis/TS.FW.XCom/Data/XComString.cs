using System.Runtime.Serialization;
using XCOMLib;

namespace TS.FW.XCom.Data
{
    public class XComString : IXComData
    {
        public string Value { get; set; } = string.Empty;

        public override int Length => string.IsNullOrEmpty(this.Value) ? 0 : Encoding.GetByteCount(this.Value);

        public XComString() : base(eXComData.ASCII) { }

        public XComString(string data) : this()
        {
            this.Value = data;
        }

        public void SetValue(string value, int length)
        {
            var buffer = Encoding.GetBytes(value);

            this.Value = Encoding.GetString(buffer, 0, (length > buffer.Length) ? buffer.Length : length).PadRight(length, ' ');
        }

        public override void SetData(XCOM xcom, int msgID)
        {
            xcom.SetString(msgID, this.Value, this.Length);
        }

        public override string ToJson(string name)
        {
            return string.Format("\"{0}\":{1}", name, this.ToJson());
        }

        public override string ToJson()
        {
            return string.Format("\"{0}\"", this.Value);
        }

        public override string ToString()
        {
            return string.Format("{0},\t<{1}>", base.ToString(), this.Value);
        }

        public static implicit operator string(XComString data) => data.Value;

        public static implicit operator XComString(string data) => new XComString(data);
    }
}
