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
    public class S6F14_AnnotatedEventReportAck_Model : IXComModel
    {
        public Byte ACKC6 { get; set; }

        public S6F14_AnnotatedEventReportAck_Model()
        {
            this.Stream = 6;
            this.Function = 14;
            this.FullName = "AnnotatedEventReportAck";
            this.Name = "AnnotatedEventReportAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACKC6 = 0;
        }
    }
}
