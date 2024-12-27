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
    public class S16F136_RPC_EventAck_Model : IXComModel
    {
        public Byte ACKC16 { get; set; }

        public S16F136_RPC_EventAck_Model()
        {
            this.Stream = 16;
            this.Function = 136;
            this.FullName = "RPC_EventAck";
            this.Name = "RPC_EventAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACKC16 = 0;
        }
    }
}
