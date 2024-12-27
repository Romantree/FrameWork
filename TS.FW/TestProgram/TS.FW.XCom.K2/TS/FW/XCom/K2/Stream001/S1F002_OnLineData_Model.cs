using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.XCom;
using Newtonsoft.Json;
using XCOMLib;

namespace TS.FW.XCom.K2
{
    public class S1F2_OnLineData_Model : IXComModel
    {
        public string VERSION { get; set; }
        public string SPEC_CODE { get; set; }
        public string MODULEID { get; set; }
        public short MCMD { get; set; }

        public S1F2_OnLineData_Model()
        {
            this.Stream = 1;
            this.Function = 2;
            this.FullName = "OnLineData";
            this.Name = "OnLineData";
            this.SubName = "";
            this.IsUnDefined = true;
        }

        public override void RecvData(IXComData data)
        {
            var depth1 = data.List;
        }

        public override void SendData(XCOM xcom, int msgId)
        {
            xcom.SetListItem(msgId, 1);
            {
                xcom.SetString(msgId, VERSION, 20);
                xcom.SetString(msgId, SPEC_CODE, 20);
                xcom.SetString(msgId, MODULEID, 28);
                xcom.SetUINT1(msgId, MCMD);
            }
        }
    }
}
