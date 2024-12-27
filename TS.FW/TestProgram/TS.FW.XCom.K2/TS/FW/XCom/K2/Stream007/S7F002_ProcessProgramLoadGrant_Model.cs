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
    public class S7F2_ProcessProgramLoadGrant_Model : IXComModel
    {
        public Byte PPGNT { get; set; }

        public S7F2_ProcessProgramLoadGrant_Model()
        {
            this.Stream = 7;
            this.Function = 2;
            this.FullName = "ProcessProgramLoadGrant";
            this.Name = "ProcessProgramLoadGrant";
            this.SubName = "";
            this.IsUnDefined = false;

            this.PPGNT = 0;
        }
    }
}
