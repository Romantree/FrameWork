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
    public class S2F102_OperatorCallAck_Model : IXComModel
    {
        public Byte ACKC2 { get; set; }

        public S2F102_OperatorCallAck_Model()
        {
            this.Stream = 2;
            this.Function = 102;
            this.FullName = "OperatorCallAck";
            this.Name = "OperatorCallAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACKC2 = 0;
        }
    }
}
