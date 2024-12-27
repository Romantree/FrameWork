using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using XCOMLib;

namespace TS.FW.XCom.Data
{
    public class XComUINT2 : IXComData<ushort>
    {
        public XComUINT2() : base(eXComData.UINT2) { }

        public XComUINT2(ushort data) : this()
        {
            this.Values.Add(data);
        }

        public XComUINT2(IEnumerable<ushort> data) : this()
        {
            if (data == null || data.Count() <= 0) return;

            this.Values.AddRange(data);
        }

        public override void SetData(XCOM xcom, int msgID)
        {
            if (this.Length <= 1)
            {
                xcom.SetUINT2(msgID, Convert.ToInt32(this.Value), this.Length);
            }
            else
            {
                xcom.SetUINT2(msgID, this.Values.Select(t => Convert.ToInt32(t)).ToArray(), this.Length);
            }
        }

        public static implicit operator ushort(XComUINT2 data) => data.Value;
        public static implicit operator List<ushort>(XComUINT2 data) => data.Values.ToList();
        public static implicit operator ushort[] (XComUINT2 data) => data.Values.ToArray();

        public static implicit operator XComUINT2(ushort data) => new XComUINT2(data);
        public static implicit operator XComUINT2(List<ushort> data) => new XComUINT2(data);
        public static implicit operator XComUINT2(ushort[] data) => new XComUINT2(data);
    }
}
