using System.Collections.Generic;
using System.Runtime.Serialization;
using XCOMLib;

namespace TS.FW.XCom.Data
{
    public class XComList : IXComData<IXComData>
    {
        private Queue<IXComData> _queuList;

        public IXComData Next => this.Dequeue();

        public new XComList List => this.Next as XComList;

        public new XComString ASCII => this.Next as XComString;

        public new XComBinary Binary => this.Next as XComBinary;

        public new XComBool Bool => this.Next as XComBool;

        public new XComINT1 INT1 => this.Next as XComINT1;

        public new XComINT2 INT2 => this.Next as XComINT2;

        public new XComINT4 INT4 => this.Next as XComINT4;

        public new XComINT8 INT8 => this.Next as XComINT8;

        public new XComUINT1 UINT1 => this.Next as XComUINT1;

        public new XComUINT2 UINT2 => this.Next as XComUINT2;

        public new XComUINT4 UINT4 => this.Next as XComUINT4;

        public new XComUINT8 UINT8 => this.Next as XComUINT8;

        public new XComFLOAT4 FLOAT4 => this.Next as XComFLOAT4;

        public new XComFLOAT8 FLOAT8 => this.Next as XComFLOAT8;

        public XComList() : base(eXComData.LIST) { }

        public void Init()
        {
            if (this._queuList == null)
            {
                this._queuList = new Queue<IXComData>(this.Values);
            }
            else
            {
                this._queuList.Clear();

                foreach (var item in this.Values)
                {
                    this._queuList.Enqueue(item);
                }
            }
        }

        public IXComData Dequeue()
        {
            if (this._queuList == null) this._queuList = new Queue<IXComData>(this.Values);

            return this._queuList.Dequeue();
        }

        public override void SetData(XCOM xcom, int msgID)
        {
            xcom.SetList(msgID, this.Length);
            {
                foreach (var item in this.Values)
                {
                    item.SetData(xcom, msgID);
                }
            }
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", this.Type, this.Length);
        }
    }
}
