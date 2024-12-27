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
    public class S2F41_HostCommandSend_EQCmd_Model : IXComModel
    {
        public Int32 RCMD { get; set; }

        public List<S2F41_EQCmd_LIST_1> EQCmd_LIST_1 { get; set; }

        public S2F41_HostCommandSend_EQCmd_Model()
        {
            this.Stream = 2;
            this.Function = 41;
            this.FullName = "HostCommandSend - EQCmd";
            this.Name = "HostCommandSend";
            this.SubName = "EQCmd";
            this.IsUnDefined = false;

            this.RCMD = 0;
            this.EQCmd_LIST_1 = new List<S2F41_EQCmd_LIST_1>();
        }
    }

    public class S2F41_EQCmd_LIST_1
    {
        public string MODULE_ID_NAME { get; set; }

        public string MODULE_ID { get; set; }

        public S2F41_EQCmd_LIST_1()
        {
            this.MODULE_ID_NAME = string.Empty;
            this.MODULE_ID = string.Empty;
        }
    }
}
