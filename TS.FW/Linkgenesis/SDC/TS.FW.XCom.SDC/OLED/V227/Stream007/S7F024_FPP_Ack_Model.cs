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
    public class S7F24_FPP_Ack_Model : IXComModel
    {
        public Byte ACKC7 { get; set; }

        public S7F24_FPP_Ack_Model()
        {
            this.Stream = 7;
            this.Function = 24;
            this.FullName = "FPP_Ack";
            this.Name = "FPP_Ack";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACKC7 = 0;
        }
    }
}
