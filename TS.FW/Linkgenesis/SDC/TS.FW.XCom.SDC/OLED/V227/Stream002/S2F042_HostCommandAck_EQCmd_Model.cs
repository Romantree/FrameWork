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
    public class S2F42_HostCommandAck_EQCmd_Model : IXComModel
    {
        public Int32 RCMD { get; set; }

        public Byte HCACK { get; set; }

        public List<S2F42_EQCmd_LIST_1> EQCmd_LIST_1 { get; set; }

        public S2F42_HostCommandAck_EQCmd_Model()
        {
            this.Stream = 2;
            this.Function = 42;
            this.FullName = "HostCommandAck - EQCmd";
            this.Name = "HostCommandAck";
            this.SubName = "EQCmd";
            this.IsUnDefined = false;

            this.RCMD = 0;
            this.HCACK = 0;
            this.EQCmd_LIST_1 = new List<S2F42_EQCmd_LIST_1>();
        }
    }

    public class S2F42_EQCmd_LIST_1
    {
        public string MODULE_ID_NAME { get; set; }

        public string MODULE_ID { get; set; }

        public Byte CPACK { get; set; }

        public S2F42_EQCmd_LIST_1()
        {
            this.MODULE_ID_NAME = string.Empty;
            this.MODULE_ID = string.Empty;
            this.CPACK = 0;
        }
    }
}
