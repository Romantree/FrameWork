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
    public class S6F12_Event_ReportAck_Model : IXComModel
    {
        public Byte ACKC6 { get; set; }

        public S6F12_Event_ReportAck_Model()
        {
            this.Stream = 6;
            this.Function = 12;
            this.FullName = "Event ReportAck";
            this.Name = "Event ReportAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACKC6 = 0;
        }
    }
}
