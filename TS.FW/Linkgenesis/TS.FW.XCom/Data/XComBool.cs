using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using XCOMLib;

namespace TS.FW.XCom.Data
{
    public class XComBool : IXComData<bool>
    {
        public XComBool() : base(eXComData.BOOL) { }

        public XComBool(bool data) : this()
        {
            this.Values.Add(data);
        }

        public XComBool(IEnumerable<bool> data) : this()
        {
            if (data == null || data.Count() <= 0) return;

            this.Values.AddRange(data);
        }

        public override void SetData(XCOM xcom, int msgID)
        {
            if (this.Length <= 1)
            {
                xcom.SetBool(msgID, this.Value, this.Length);
            }
            else
            {
                xcom.SetBool(msgID, this.Values.ToArray(), this.Length);
            }
        }

        public static implicit operator bool(XComBool data) => data.Value;
        public static implicit operator List<bool>(XComBool data) => data.Values.ToList();
        public static implicit operator bool[] (XComBool data) => data.Values.ToArray();

        public static implicit operator XComBool(bool data) => new XComBool(data);
        public static implicit operator XComBool(List<bool> data) => new XComBool(data);
        public static implicit operator XComBool(bool[] data) => new XComBool(data);
    }
}