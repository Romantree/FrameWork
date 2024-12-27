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
    public class S3F116_MaterialAck_Model : IXComModel
    {
        public Byte ACKC3 { get; set; }

        public S3F116_MaterialAck_Model()
        {
            this.Stream = 3;
            this.Function = 116;
            this.FullName = "MaterialAck";
            this.Name = "MaterialAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACKC3 = 0;
        }
    }
}
