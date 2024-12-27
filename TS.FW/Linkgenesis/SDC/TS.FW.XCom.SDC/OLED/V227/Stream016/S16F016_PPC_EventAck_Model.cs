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
    public class S16F16_PPC_EventAck_Model : IXComModel
    {
        public Byte ACKC16 { get; set; }

        public S16F16_PPC_EventAck_Model()
        {
            this.Stream = 16;
            this.Function = 16;
            this.FullName = "PPC_EventAck";
            this.Name = "PPC_EventAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACKC16 = 0;
        }
    }
}
