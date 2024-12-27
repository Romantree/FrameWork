using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using XCOMLib;

namespace TS.FW.XCom.Data
{
    public class XComUINT4 : IXComData<uint>
    {
        public XComUINT4() : base(eXComData.UINT4) { }

        public XComUINT4(uint data) : this()
        {
            this.Values.Add(data);
        }

        public XComUINT4(IEnumerable<uint> data) : this()
        {
            if (data == null || data.Count() <= 0) return;

            this.Values.AddRange(data);
        }

        public override void SetData(XCOM xcom, int msgID)
        {
            if (this.Length <= 1)
            {
                xcom.SetUINT4(msgID, Convert.ToInt64(this.Value), this.Length);
            }
            else
            {
                xcom.SetUINT4(msgID, this.Values.Select(t => Convert.ToInt64(t)).ToArray(), this.Length);
            }
        }

        public static implicit operator uint(XComUINT4 data) => data.Value;
        public static implicit operator List<uint>(XComUINT4 data) => data.Values.ToList();
        public static implicit operator uint[] (XComUINT4 data) => data.Values.ToArray();

        public static implicit operator XComUINT4(uint data) => new XComUINT4(data);
        public static implicit operator XComUINT4(List<uint> data) => new XComUINT4(data);
        public static implicit operator XComUINT4(uint[] data) => new XComUINT4(data);
    }
}
