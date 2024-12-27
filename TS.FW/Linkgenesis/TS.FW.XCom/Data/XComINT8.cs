using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using XCOMLib;

namespace TS.FW.XCom.Data
{
    public class XComINT8 : IXComData<long>
    {
        public XComINT8() : base(eXComData.INT8) { }

        public XComINT8(long data) : this()
        {
            this.Values.Add(data);
        }

        public XComINT8(IEnumerable<long> data) : this()
        {
            if (data == null || data.Count() <= 0) return;

            this.Values.AddRange(data);
        }

        public override void SetData(XCOM xcom, int msgID)
        {
            if (this.Length <= 1)
            {
                xcom.SetINT8(msgID, Convert.ToInt32(this.Value), this.Length);
            }
            else
            {
                xcom.SetINT8(msgID, this.Values.Select(t => Convert.ToInt32(t)).ToArray(), this.Length);
            }
        }

        public static implicit operator long(XComINT8 data) => data.Value;
        public static implicit operator List<long>(XComINT8 data) => data.Values.ToList();
        public static implicit operator long[] (XComINT8 data) => data.Values.ToArray();

        public static implicit operator XComINT8(long data) => new XComINT8(data);
        public static implicit operator XComINT8(List<long> data) => new XComINT8(data);
        public static implicit operator XComINT8(long[] data) => new XComINT8(data);
    }
}
