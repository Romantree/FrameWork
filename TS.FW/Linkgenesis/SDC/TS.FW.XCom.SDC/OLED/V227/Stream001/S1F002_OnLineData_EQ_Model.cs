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
    public class S1F2_OnLineData_EQ_Model : IXComModel
    {
        public string VERSION { get; set; }

        public string SPEC_CODE { get; set; }

        public string MODULE_ID { get; set; }

        public Byte MCMD { get; set; }

        public S1F2_OnLineData_EQ_Model()
        {
            this.Stream = 1;
            this.Function = 2;
            this.FullName = "OnLineData - EQ";
            this.Name = "OnLineData";
            this.SubName = "EQ";
            this.IsUnDefined = false;

            this.VERSION = string.Empty;
            this.SPEC_CODE = string.Empty;
            this.MODULE_ID = string.Empty;
            this.MCMD = 0;
        }
    }
}
