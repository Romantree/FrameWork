using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using XCOMLib;

namespace TS.FW.XCom.Data
{
    public class XComFLOAT8 : IXComData<double>
    {
        public XComFLOAT8() : base(eXComData.FLOAT8) { }

        public XComFLOAT8(double data) : this()
        {
            this.Values.Add(data);
        }

        public XComFLOAT8(IEnumerable<double> data) : this()
        {
            if (data == null || data.Count() <= 0) return;

            this.Values.AddRange(data);
        }

        public override void SetData(XCOM xcom, int msgID)
        {
            if (this.Length <= 1)
            {
                xcom.SetFLOAT8(msgID, this.Value, this.Length);
            }
            else
            {
                xcom.SetFLOAT8(msgID, this.Values.ToArray(), this.Length);
            }
        }

        public static implicit operator double(XComFLOAT8 data) => data.Value;
        public static implicit operator List<double>(XComFLOAT8 data) => data.Values.ToList();
        public static implicit operator double[] (XComFLOAT8 data) => data.Values.ToArray();

        public static implicit operator XComFLOAT8(double data) => new XComFLOAT8(data);
        public static implicit operator XComFLOAT8(List<double> data) => new XComFLOAT8(data);
        public static implicit operator XComFLOAT8(double[] data) => new XComFLOAT8(data);
    }
}
