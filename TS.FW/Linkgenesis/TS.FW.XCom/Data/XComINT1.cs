using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using XCOMLib;

namespace TS.FW.XCom.Data
{
    public class XComINT1 : IXComData<byte>
    {
        public XComINT1() : base(eXComData.INT1) { }

        public XComINT1(byte data) : this()
        {
            this.Values.Add(data);
        }

        public XComINT1(IEnumerable<byte> data) : this()
        {
            if (data == null || data.Count() <= 0) return;

            this.Values.AddRange(data);
        }

        public override void SetData(XCOM xcom, int msgID)
        {
            if (this.Length <= 1)
            {
                xcom.SetINT1(msgID, this.Value, this.Length);
            }
            else
            {
                xcom.SetINT1(msgID, this.Values.ToArray(), this.Length);
            }
        }

        public static implicit operator byte(XComINT1 data) => data.Value;
        public static implicit operator List<byte>(XComINT1 data) => data.Values.ToList();
        public static implicit operator byte[] (XComINT1 data) => data.Values.ToArray();

        public static implicit operator XComINT1(byte data) => new XComINT1(data);
        public static implicit operator XComINT1(List<byte> data) => new XComINT1(data);
        public static implicit operator XComINT1(byte[] data) => new XComINT1(data);
    }
}
