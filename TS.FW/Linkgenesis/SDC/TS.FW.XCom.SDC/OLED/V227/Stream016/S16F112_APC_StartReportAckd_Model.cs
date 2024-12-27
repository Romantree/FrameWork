using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.XCom;
using Newtonsoft.Json;
using XCOMLib;

namespace TS.FW.XCom.SDC.OLED.V227
{
    public class S16F112_APC_StartReportAckd_Model : IXComModel
    {
        public Byte TCACK { get; set; }

        public S16F112_APC_StartReportAckd_Model()
        {
            this.Stream = 16;
            this.Function = 112;
            this.FullName = "APC_StartReportAckd";
            this.Name = "APC_StartReportAckd";
            this.SubName = "";
            this.IsUnDefined = false;

            this.TCACK = 0;
        }
    }
}
