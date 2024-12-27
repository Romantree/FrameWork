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
    public class S7F124_RecipeModificationEVP_Ack_Model : IXComModel
    {
        public Byte ACKC7 { get; set; }

        public S7F124_RecipeModificationEVP_Ack_Model()
        {
            this.Stream = 7;
            this.Function = 124;
            this.FullName = "RecipeModificationEVP_Ack";
            this.Name = "RecipeModificationEVP_Ack";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACKC7 = 0;
        }
    }
}
