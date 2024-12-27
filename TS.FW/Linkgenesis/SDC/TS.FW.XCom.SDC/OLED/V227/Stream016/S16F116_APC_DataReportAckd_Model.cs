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
    public class S16F116_APC_DataReportAckd_Model : IXComModel
    {
        public Byte TCACK { get; set; }

        public S16F116_APC_DataReportAckd_Model()
        {
            this.Stream = 16;
            this.Function = 116;
            this.FullName = "APC_DataReportAckd";
            this.Name = "APC_DataReportAckd";
            this.SubName = "";
            this.IsUnDefined = false;

            this.TCACK = 0;
        }
    }
}
