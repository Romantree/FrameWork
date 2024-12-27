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
    public class S7F118_PPBodyChangedReportEVP_Ack_Model : IXComModel
    {
        public Byte ACK7 { get; set; }

        public S7F118_PPBodyChangedReportEVP_Ack_Model()
        {
            this.Stream = 7;
            this.Function = 118;
            this.FullName = "PPBodyChangedReportEVP_Ack";
            this.Name = "PPBodyChangedReportEVP_Ack";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACK7 = 0;
        }
    }
}
