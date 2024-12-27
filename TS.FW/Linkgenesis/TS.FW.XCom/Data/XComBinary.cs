using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using XCOMLib;

namespace TS.FW.XCom.Data
{
    public class XComBinary : IXComData<byte>
    {
        public XComBinary() : base(eXComData.BINARY) { }

        public XComBinary(byte data) : this()
        {
            this.Values.Add(data);
        }

        public XComBinary(IEnumerable<byte> data) : this()
        {
            if (data == null || data.Count() <= 0) return;

            this.Values.AddRange(data);
        }

        public override void SetData(XCOM xcom, int msgID)
        {
            if (this.Length <= 1)
            {
                xcom.SetBinary(msgID, this.Value, this.Length);
            }
            else
            {
                xcom.SetBinary(msgID, this.Values.ToArray(), this.Length);
            }
        }

        public static implicit operator byte(XComBinary data) => data.Value;
        public static implicit operator List<byte>(XComBinary data) => data.Values.ToList();
        public static implicit operator byte[] (XComBinary data) => data.Values.ToArray();

        public static implicit operator XComBinary(byte data) => new XComBinary(data);
        public static implicit operator XComBinary(List<byte> data) => new XComBinary(data);
        public static implicit operator XComBinary(byte[] data) => new XComBinary(data);
    }
}