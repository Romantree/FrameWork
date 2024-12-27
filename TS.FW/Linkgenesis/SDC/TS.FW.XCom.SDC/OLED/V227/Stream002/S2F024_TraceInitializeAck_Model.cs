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
    public class S2F24_TraceInitializeAck_Model : IXComModel
    {
        public Byte TIACK { get; set; }

        public S2F24_TraceInitializeAck_Model()
        {
            this.Stream = 2;
            this.Function = 24;
            this.FullName = "TraceInitializeAck";
            this.Name = "TraceInitializeAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.TIACK = 0;
        }
    }
}
