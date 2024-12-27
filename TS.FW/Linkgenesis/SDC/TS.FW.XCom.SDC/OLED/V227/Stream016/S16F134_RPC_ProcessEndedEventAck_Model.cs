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
    public class S16F134_RPC_ProcessEndedEventAck_Model : IXComModel
    {
        public Byte ACKC16 { get; set; }

        public S16F134_RPC_ProcessEndedEventAck_Model()
        {
            this.Stream = 16;
            this.Function = 134;
            this.FullName = "RPC_ProcessEndedEventAck";
            this.Name = "RPC_ProcessEndedEventAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACKC16 = 0;
        }
    }
}
