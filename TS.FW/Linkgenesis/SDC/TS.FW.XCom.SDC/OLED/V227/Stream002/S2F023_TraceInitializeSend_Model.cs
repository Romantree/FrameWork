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
    public class S2F23_TraceInitializeSend_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Int32 TRID { get; set; }

        public string SMPTIME { get; set; }

        public Int32 TOTSMP { get; set; }

        public Int32 GRSIZE { get; set; }

        public Int32 SVID { get; set; }

        public S2F23_TraceInitializeSend_Model()
        {
            this.Stream = 2;
            this.Function = 23;
            this.FullName = "TraceInitializeSend";
            this.Name = "TraceInitializeSend";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.TRID = 0;
            this.SMPTIME = string.Empty;
            this.TOTSMP = 0;
            this.GRSIZE = 0;
            this.SVID = 0;
        }
    }
}
