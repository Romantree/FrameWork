using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using XCOMLib;

namespace TS.FW.XCom.Data
{
    public class XComUINT1 : IXComData<byte>
    {
        public XComUINT1() : base(eXComData.UINT1) { }

        public XComUINT1(byte data) : this()
        {
            this.Values.Add(data);
        }

        public XComUINT1(IEnumerable<byte> data) : this()
        {
            if (data == null || data.Count() <= 0) return;

            this.Values.AddRange(data);
        }

        public override void SetData(XCOM xcom, int msgID)
        {
            if (this.Length <= 1)
            {
                xcom.SetUINT1(msgID, this.Value, this.Length);
            }
            else
            {
                xcom.SetUINT1(msgID, this.Values.ToArray(), this.Length);
            }
        }

        public static implicit operator byte(XComUINT1 data) => data.Value;
        public static implicit operator List<byte>(XComUINT1 data) => data.Values.ToList();
        public static implicit operator byte[] (XComUINT1 data) => data.Values.ToArray();

        public static implicit operator XComUINT1(byte data) => new XComUINT1(data);
        public static implicit operator XComUINT1(List<byte> data) => new XComUINT1(data);
        public static implicit operator XComUINT1(byte[] data) => new XComUINT1(data);
    }
}
