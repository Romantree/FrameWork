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
    public class S3F1_MaterialStateReq_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public S3F1_MaterialStateReq_Model()
        {
            this.Stream = 3;
            this.Function = 1;
            this.FullName = "MaterialStateReq";
            this.Name = "MaterialStateReq";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
        }
    }
}
