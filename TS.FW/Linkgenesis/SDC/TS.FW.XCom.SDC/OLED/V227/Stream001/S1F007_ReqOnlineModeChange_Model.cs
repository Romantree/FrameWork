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
    public class S1F7_ReqOnlineModeChange_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Byte MCMD { get; set; }

        public S1F7_ReqOnlineModeChange_Model()
        {
            this.Stream = 1;
            this.Function = 7;
            this.FullName = "ReqOnlineModeChange";
            this.Name = "ReqOnlineModeChange";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.MCMD = 0;
        }
    }
}
