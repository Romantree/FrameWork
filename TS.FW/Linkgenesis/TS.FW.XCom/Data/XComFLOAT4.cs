using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using XCOMLib;

namespace TS.FW.XCom.Data
{
    public class XComFLOAT4 : IXComData<float>
    {
        public XComFLOAT4() : base(eXComData.FLOAT4) { }

        public XComFLOAT4(float data) : this()
        {
            this.Values.Add(data);
        }

        public XComFLOAT4(IEnumerable<float> data) : this()
        {
            if (data == null || data.Count() <= 0) return;

            this.Values.AddRange(data);
        }

        public override void SetData(XCOM xcom, int msgID)
        {
            if (this.Length <= 1)
            {
                xcom.SetFLOAT4(msgID, this.Value, this.Length);
            }
            else
            {
                xcom.SetFLOAT4(msgID, this.Values.ToArray(), this.Length);
            }
        }

        public static implicit operator float(XComFLOAT4 data) => data.Value;
        public static implicit operator List<float>(XComFLOAT4 data) => data.Values.ToList();
        public static implicit operator float[] (XComFLOAT4 data) => data.Values.ToArray();

        public static implicit operator XComFLOAT4(float data) => new XComFLOAT4(data);
        public static implicit operator XComFLOAT4(List<float> data) => new XComFLOAT4(data);
        public static implicit operator XComFLOAT4(float[] data) => new XComFLOAT4(data);
    }
}