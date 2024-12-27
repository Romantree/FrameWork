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
    public class S7F103_PPIDExistenceCheck_Model : IXComModel
    {
        public string MODULEID { get; set; }

        public string PPID { get; set; }

        public Byte PPID_TYPE { get; set; }

        public S7F103_PPIDExistenceCheck_Model()
        {
            this.Stream = 7;
            this.Function = 103;
            this.FullName = "PPIDExistenceCheck";
            this.Name = "PPIDExistenceCheck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULEID = string.Empty;
            this.PPID = string.Empty;
            this.PPID_TYPE = 0;
        }
    }
}
