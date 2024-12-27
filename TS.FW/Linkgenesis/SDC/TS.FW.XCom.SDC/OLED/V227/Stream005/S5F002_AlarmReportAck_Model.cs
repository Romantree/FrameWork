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
    public class S5F2_AlarmReportAck_Model : IXComModel
    {
        public Byte ACKC5 { get; set; }

        public S5F2_AlarmReportAck_Model()
        {
            this.Stream = 5;
            this.Function = 2;
            this.FullName = "AlarmReportAck";
            this.Name = "AlarmReportAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACKC5 = 0;
        }
    }
}
