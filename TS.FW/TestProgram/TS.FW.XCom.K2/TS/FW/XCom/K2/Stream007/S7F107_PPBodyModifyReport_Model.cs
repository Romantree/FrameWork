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
    public class S7F107_PPBodyModifyReport_Model : IXComModel
    {
        public Byte MODE { get; set; }

        public string MODULEID { get; set; }

        public string PPID { get; set; }

        public Byte PPID_TYPE { get; set; }

        public Int32 CCODE { get; set; }

        public List<S7F107_Params> Params { get; set; }

        public S7F107_PPBodyModifyReport_Model()
        {
            this.Stream = 7;
            this.Function = 107;
            this.FullName = "PPBodyModifyReport";
            this.Name = "PPBodyModifyReport";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODE = 0;
            this.MODULEID = string.Empty;
            this.PPID = string.Empty;
            this.PPID_TYPE = 0;
            this.CCODE = 0;
            this.Params = new List<S7F107_Params>();
        }
    }

    public class S7F107_Params
    {
        public string P_PARM_NAME { get; set; }

        public string P_PARM { get; set; }

        public S7F107_Params()
        {
            this.P_PARM_NAME = string.Empty;
            this.P_PARM = string.Empty;
        }
    }
}
