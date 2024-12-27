using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using XCOMLib;

namespace TS.FW.XCom.Data
{
    public class XComUINT8 : IXComData<ulong>
    {
        public XComUINT8() : base(eXComData.UINT8) { }

        public XComUINT8(ulong data) : this()
        {
            this.Values.Add(data);
        }

        public XComUINT8(IEnumerable<ulong> data) : this()
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

        public static implicit operator ulong(XComUINT8 data) => data.Value;
        public static implicit operator List<ulong>(XComUINT8 data) => data.Values.ToList();
        public static implicit operator ulong[] (XComUINT8 data) => data.Values.ToArray();

        public static implicit operator XComUINT8(ulong data) => new XComUINT8(data);
        public static implicit operator XComUINT8(List<ulong> data) => new XComUINT8(data);
        public static implicit operator XComUINT8(ulong[] data) => new XComUINT8(data);
    }
}
