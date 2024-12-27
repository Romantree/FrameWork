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
    public class S7F108_PPBodyModifyReportAck_Model : IXComModel
    {
        public Byte ACK7 { get; set; }

        public S7F108_PPBodyModifyReportAck_Model()
        {
            this.Stream = 7;
            this.Function = 108;
            this.FullName = "PPBodyModifyReportAck";
            this.Name = "PPBodyModifyReportAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACK7 = 0;
        }
    }
}
