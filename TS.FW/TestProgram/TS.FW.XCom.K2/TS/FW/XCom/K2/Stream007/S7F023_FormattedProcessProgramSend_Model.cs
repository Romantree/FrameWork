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
    public class S7F23_FormattedProcessProgramSend_Model : IXComModel
    {
        public string MODULEID { get; set; }

        public string PPID { get; set; }

        public Byte PPID_TYPE { get; set; }

        public Int32 CCODE { get; set; }

        public List<S7F23_Params> Params { get; set; }

        public S7F23_FormattedProcessProgramSend_Model()
        {
            this.Stream = 7;
            this.Function = 23;
            this.FullName = "FormattedProcessProgramSend";
            this.Name = "FormattedProcessProgramSend";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULEID = string.Empty;
            this.PPID = string.Empty;
            this.PPID_TYPE = 0;
            this.CCODE = 0;
            this.Params = new List<S7F23_Params>();
        }
    }

    public class S7F23_Params
    {
        public string P_PARM_NAME { get; set; }

        public string P_PARM { get; set; }

        public S7F23_Params()
        {
            this.P_PARM_NAME = string.Empty;
            this.P_PARM = string.Empty;
        }
    }
}
