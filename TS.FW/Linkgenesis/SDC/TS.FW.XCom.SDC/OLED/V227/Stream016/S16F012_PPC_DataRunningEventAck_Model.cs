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
    public class S16F12_PPC_DataRunningEventAck_Model : IXComModel
    {
        public Byte ACKC16 { get; set; }

        public S16F12_PPC_DataRunningEventAck_Model()
        {
            this.Stream = 16;
            this.Function = 12;
            this.FullName = "PPC_DataRunningEventAck";
            this.Name = "PPC_DataRunningEventAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACKC16 = 0;
        }
    }
}
