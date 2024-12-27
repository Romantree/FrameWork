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
    public class S6F1_TraceDataSend_Model : IXComModel
    {
        public string MODULEID { get; set; }

        public Int32 TRID { get; set; }

        public Int32 SMPLN { get; set; }

        public List<S6F1_REPGSZs> REPGSZs { get; set; }

        public S6F1_TraceDataSend_Model()
        {
            this.Stream = 6;
            this.Function = 1;
            this.FullName = "TraceDataSend";
            this.Name = "TraceDataSend";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULEID = string.Empty;
            this.TRID = 0;
            this.SMPLN = 0;
            this.REPGSZs = new List<S6F1_REPGSZs>();
        }
    }

    public class S6F1_REPGSZs
    {
        public string SCTIME { get; set; }

        public List<S6F1_SVs> SVs { get; set; }

        public S6F1_REPGSZs()
        {
            this.SCTIME = string.Empty;
            this.SVs = new List<S6F1_SVs>();
        }
    }

    public class S6F1_SVs
    {
        public Int32 SVID { get; set; }

        public string SV { get; set; }

        public string SVNAME { get; set; }

        public S6F1_SVs()
        {
            this.SVID = 0;
            this.SV = string.Empty;
            this.SVNAME = string.Empty;
        }
    }
}
