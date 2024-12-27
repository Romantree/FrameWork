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
    public class S3F104_ProductionPlanInfoAck_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Int32 PLCD { get; set; }

        public Byte ACKC3 { get; set; }

        public S3F104_ProductionPlanInfoAck_Model()
        {
            this.Stream = 3;
            this.Function = 104;
            this.FullName = "ProductionPlanInfoAck";
            this.Name = "ProductionPlanInfoAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.PLCD = 0;
            this.ACKC3 = 0;
        }
    }
}
