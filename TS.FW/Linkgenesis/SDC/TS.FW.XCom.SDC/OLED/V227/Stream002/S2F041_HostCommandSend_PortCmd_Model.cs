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
    public class S2F41_HostCommandSend_PortCmd_Model : IXComModel
    {
        public Int32 RCMD { get; set; }

        public List<S2F41_PortCmd_LIST_1> PortCmd_LIST_1 { get; set; }

        public S2F41_HostCommandSend_PortCmd_Model()
        {
            this.Stream = 2;
            this.Function = 41;
            this.FullName = "HostCommandSend - PortCmd";
            this.Name = "HostCommandSend";
            this.SubName = "PortCmd";
            this.IsUnDefined = false;

            this.RCMD = 0;
            this.PortCmd_LIST_1 = new List<S2F41_PortCmd_LIST_1>();
        }
    }

    public class S2F41_PortCmd_LIST_1
    {
        public string PORTID_NAME { get; set; }

        public string PORTID { get; set; }

        public S2F41_PortCmd_LIST_1()
        {
            this.PORTID_NAME = string.Empty;
            this.PORTID = string.Empty;
        }
    }
}
