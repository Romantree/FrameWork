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
    public class S7F110_CurrentRunningEQPPIDData_Model : IXComModel
    {
        public Byte ACK { get; set; }

        public string MODULE_ID { get; set; }

        public string PPID { get; set; }

        public Byte PPID_TYPE { get; set; }

        public S7F110_CurrentRunningEQPPIDData_Model()
        {
            this.Stream = 7;
            this.Function = 110;
            this.FullName = "CurrentRunningEQPPIDData";
            this.Name = "CurrentRunningEQPPIDData";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACK = 0;
            this.MODULE_ID = string.Empty;
            this.PPID = string.Empty;
            this.PPID_TYPE = 0;
        }
    }
}
