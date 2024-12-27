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
    public class S2F101_OperatorCall_Model : IXComModel
    {
        public Int32 TID { get; set; }

        public string MODULEID { get; set; }

        public List<S2F101_Msgs> Msgs { get; set; }

        public S2F101_OperatorCall_Model()
        {
            this.Stream = 2;
            this.Function = 101;
            this.FullName = "OperatorCall";
            this.Name = "OperatorCall";
            this.SubName = "";
            this.IsUnDefined = false;

            this.TID = 0;
            this.MODULEID = string.Empty;
            this.Msgs = new List<S2F101_Msgs>();
        }
    }

    public class S2F101_Msgs
    {
        public string MSG { get; set; }

        public S2F101_Msgs()
        {
            this.MSG = string.Empty;
        }
    }
}
