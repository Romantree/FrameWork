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
    public class S7F125_RunRecipeParamEVP_Req_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public string PPID { get; set; }

        public Byte PPID_TYPE { get; set; }

        public S7F125_RunRecipeParamEVP_Req_Model()
        {
            this.Stream = 7;
            this.Function = 125;
            this.FullName = "RunRecipeParamEVP_Req";
            this.Name = "RunRecipeParamEVP_Req";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.PPID = string.Empty;
            this.PPID_TYPE = 0;
        }
    }
}
