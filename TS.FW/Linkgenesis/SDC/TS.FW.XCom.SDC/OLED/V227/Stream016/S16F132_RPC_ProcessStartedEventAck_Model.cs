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
    public class S16F132_RPC_ProcessStartedEventAck_Model : IXComModel
    {
        public Byte ACKC16 { get; set; }

        public S16F132_RPC_ProcessStartedEventAck_Model()
        {
            this.Stream = 16;
            this.Function = 132;
            this.FullName = "RPC_ProcessStartedEventAck";
            this.Name = "RPC_ProcessStartedEventAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACKC16 = 0;
        }
    }
}
