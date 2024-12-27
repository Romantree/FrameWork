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
    public class S3F120_ModuleLabelSerialQuantityInfoAck_Model : IXComModel
    {
        public Byte ACKC3 { get; set; }

        public S3F120_ModuleLabelSerialQuantityInfoAck_Model()
        {
            this.Stream = 3;
            this.Function = 120;
            this.FullName = "ModuleLabelSerialQuantityInfoAck";
            this.Name = "ModuleLabelSerialQuantityInfoAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACKC3 = 0;
        }
    }
}
