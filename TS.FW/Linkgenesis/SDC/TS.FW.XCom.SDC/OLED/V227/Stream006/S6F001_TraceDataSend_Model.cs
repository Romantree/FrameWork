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
    public class S6F1_TraceDataSend_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Int32 TRID { get; set; }

        public Int32 SMPLN { get; set; }

        public List<S6F1_LIST_1> LIST_1 { get; set; }

        public S6F1_TraceDataSend_Model()
        {
            this.Stream = 6;
            this.Function = 1;
            this.FullName = "TraceDataSend";
            this.Name = "TraceDataSend";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.TRID = 0;
            this.SMPLN = 0;
            this.LIST_1 = new List<S6F1_LIST_1>();
        }
    }

    public class S6F1_LIST_1
    {
        public string SCTIME { get; set; }

        public List<S6F1_LIST_2> LIST_2 { get; set; }

        public S6F1_LIST_1()
        {
            this.SCTIME = string.Empty;
            this.LIST_2 = new List<S6F1_LIST_2>();
        }
    }

    public class S6F1_LIST_2
    {
        public Int32 SVID { get; set; }

        public string SV { get; set; }

        public string SVNAME { get; set; }

        public S6F1_LIST_2()
        {
            this.SVID = 0;
            this.SV = string.Empty;
            this.SVNAME = string.Empty;
        }
    }
}
