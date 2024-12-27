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
    public class S10F3_TerminalDisplaySingle_Model : IXComModel
    {
        public Byte TID { get; set; }

        public string MODULE_ID { get; set; }

        public List<S10F3_LIST_1> LIST_1 { get; set; }

        public S10F3_TerminalDisplaySingle_Model()
        {
            this.Stream = 10;
            this.Function = 3;
            this.FullName = "TerminalDisplaySingle";
            this.Name = "TerminalDisplaySingle";
            this.SubName = "";
            this.IsUnDefined = false;

            this.TID = 0;
            this.MODULE_ID = string.Empty;
            this.LIST_1 = new List<S10F3_LIST_1>();
        }
    }

    public class S10F3_LIST_1
    {
        public string MSG { get; set; }

        public S10F3_LIST_1()
        {
            this.MSG = string.Empty;
        }
    }
}
