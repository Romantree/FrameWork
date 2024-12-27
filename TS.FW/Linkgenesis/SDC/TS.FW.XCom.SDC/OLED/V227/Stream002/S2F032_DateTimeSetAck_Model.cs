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
    public class S2F32_DateTimeSetAck_Model : IXComModel
    {
        public Byte ACKC2 { get; set; }

        public S2F32_DateTimeSetAck_Model()
        {
            this.Stream = 2;
            this.Function = 32;
            this.FullName = "DateTimeSetAck";
            this.Name = "DateTimeSetAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACKC2 = 0;
        }
    }
}
