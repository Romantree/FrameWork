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
    public class S7F32_RemoteFPP_EVP_Ack_Model : IXComModel
    {
        public Byte ACKC7 { get; set; }

        public S7F32_RemoteFPP_EVP_Ack_Model()
        {
            this.Stream = 7;
            this.Function = 32;
            this.FullName = "RemoteFPP_EVP_Ack";
            this.Name = "RemoteFPP_EVP_Ack";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACKC7 = 0;
        }
    }
}
