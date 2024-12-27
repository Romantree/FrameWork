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
    public class S7F102_CurrentEQPPIDData_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Byte PPID_TYPE { get; set; }

        public List<S7F102_LIST_1> LIST_1 { get; set; }

        public S7F102_CurrentEQPPIDData_Model()
        {
            this.Stream = 7;
            this.Function = 102;
            this.FullName = "CurrentEQPPIDData";
            this.Name = "CurrentEQPPIDData";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.PPID_TYPE = 0;
            this.LIST_1 = new List<S7F102_LIST_1>();
        }
    }

    public class S7F102_LIST_1
    {
        public string PPID { get; set; }

        public S7F102_LIST_1()
        {
            this.PPID = string.Empty;
        }
    }
}
