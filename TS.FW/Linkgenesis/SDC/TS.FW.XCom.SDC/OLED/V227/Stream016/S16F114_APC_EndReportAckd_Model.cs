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
    public class S16F114_APC_EndReportAckd_Model : IXComModel
    {
        public Byte TCACK { get; set; }

        public S16F114_APC_EndReportAckd_Model()
        {
            this.Stream = 16;
            this.Function = 114;
            this.FullName = "APC_EndReportAckd";
            this.Name = "APC_EndReportAckd";
            this.SubName = "";
            this.IsUnDefined = false;

            this.TCACK = 0;
        }
    }
}
