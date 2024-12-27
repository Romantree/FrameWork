using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using XCOMLib;

namespace TS.FW.XCom.Data
{
    public class XComINT2 : IXComData<short>
    {
        public XComINT2() : base(eXComData.INT2) { }

        public XComINT2(short data) : this()
        {
            this.Values.Add(data);
        }

        public XComINT2(IEnumerable<short> data) : this()
        {
            if (data == null || data.Count() <= 0) return;

            this.Values.AddRange(data);
        }

        public override void SetData(XCOM xcom, int msgID)
        {
            if (this.Length <= 1)
            {
                xcom.SetINT2(msgID, this.Value, this.Length);
            }
            else
            {
                xcom.SetINT2(msgID, this.Values.ToArray(), this.Length);
            }
        }

        public static implicit operator short(XComINT2 data) => data.Value;
        public static implicit operator List<short>(XComINT2 data) => data.Values.ToList();
        public static implicit operator short[] (XComINT2 data) => data.Values.ToArray();

        public static implicit operator XComINT2(short data) => new XComINT2(data);
        public static implicit operator XComINT2(List<short> data) => new XComINT2(data);
        public static implicit operator XComINT2(short[] data) => new XComINT2(data);
    }
}
