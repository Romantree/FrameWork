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
    public class S5F1_AlarmReportSend_Model : IXComModel
    {
        public Byte SETCODE { get; set; }

        public string MODULE_ID { get; set; }

        public Byte ALCD { get; set; }

        public Int64 ALID { get; set; }

        public string ALTX { get; set; }

        public S5F1_AlarmReportSend_Model()
        {
            this.Stream = 5;
            this.Function = 1;
            this.FullName = "AlarmReportSend";
            this.Name = "AlarmReportSend";
            this.SubName = "";
            this.IsUnDefined = false;

            this.SETCODE = 0;
            this.MODULE_ID = string.Empty;
            this.ALCD = 0;
            this.ALID = 0;
            this.ALTX = string.Empty;
        }
    }
}
