using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using XCOMLib;

namespace TS.FW.XCom.Data
{
    public class XComINT4 : IXComData<int>
    {
        public XComINT4() : base(eXComData.INT4) { }

        public XComINT4(int data) : this()
        {
            this.Values.Add(data);
        }

        public XComINT4(IEnumerable<int> data) : this()
        {
            if (data == null || data.Count() <= 0) return;

            this.Values.AddRange(data);
        }

        public override void SetData(XCOM xcom, int msgID)
        {
            if (this.Length <= 1)
            {
                xcom.SetINT4(msgID, this.Value, this.Length);
            }
            else
            {
                xcom.SetINT4(msgID, this.Values.ToArray(), this.Length);
            }
        }

        public static implicit operator int(XComINT4 data) => data.Value;
        public static implicit operator List<int>(XComINT4 data) => data.Values.ToList();
        public static implicit operator int[] (XComINT4 data) => data.Values.ToArray();

        public static implicit operator XComINT4(int data) => new XComINT4(data);
        public static implicit operator XComINT4(List<int> data) => new XComINT4(data);
        public static implicit operator XComINT4(int[] data) => new XComINT4(data);
    }
}
