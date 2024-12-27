using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.XCom;
using Newtonsoft.Json;
using XCOMLib;

namespace TS.FW.XCom.K2
{
    public class S10F3_TerminalDisplaySingle_Model : IXComModel
    {
        public Int32 TID { get; set; }

        public string MODULEID { get; set; }

        public List<S10F3_Msgs> Msgs { get; set; }

        public S10F3_TerminalDisplaySingle_Model()
        {
            this.Stream = 10;
            this.Function = 3;
            this.FullName = "TerminalDisplaySingle";
            this.Name = "TerminalDisplaySingle";
            this.SubName = "";
            this.IsUnDefined = false;

            this.TID = 0;
            this.MODULEID = string.Empty;
            this.Msgs = new List<S10F3_Msgs>();
        }
    }

    public class S10F3_Msgs
    {
        public string MSG { get; set; }

        public S10F3_Msgs()
        {
            this.MSG = string.Empty;
        }
    }
}
