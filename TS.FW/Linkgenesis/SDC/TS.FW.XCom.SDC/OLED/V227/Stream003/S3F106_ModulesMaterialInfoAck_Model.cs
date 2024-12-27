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
    public class S3F106_ModulesMaterialInfoAck_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Byte ACKC3 { get; set; }

        public S3F106_ModulesMaterialInfoAck_Model()
        {
            this.Stream = 3;
            this.Function = 106;
            this.FullName = "ModulesMaterialInfoAck";
            this.Name = "ModulesMaterialInfoAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.ACKC3 = 0;
        }
    }
}
