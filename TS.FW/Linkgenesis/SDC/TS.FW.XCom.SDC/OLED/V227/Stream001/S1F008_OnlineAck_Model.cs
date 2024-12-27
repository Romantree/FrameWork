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
    public class S1F8_OnlineAck_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Byte MCMD { get; set; }

        public Byte ONLACK { get; set; }

        public S1F8_OnlineAck_Model()
        {
            this.Stream = 1;
            this.Function = 8;
            this.FullName = "OnlineAck";
            this.Name = "OnlineAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.MCMD = 0;
            this.ONLACK = 0;
        }
    }
}
