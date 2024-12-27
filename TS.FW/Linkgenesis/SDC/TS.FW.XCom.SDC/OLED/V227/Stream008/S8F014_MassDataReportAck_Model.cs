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
    public class S8F14_MassDataReportAck_Model : IXComModel
    {
        public Byte ACK { get; set; }

        public S8F14_MassDataReportAck_Model()
        {
            this.Stream = 8;
            this.Function = 14;
            this.FullName = "MassDataReportAck";
            this.Name = "MassDataReportAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACK = 0;
        }
    }
}
