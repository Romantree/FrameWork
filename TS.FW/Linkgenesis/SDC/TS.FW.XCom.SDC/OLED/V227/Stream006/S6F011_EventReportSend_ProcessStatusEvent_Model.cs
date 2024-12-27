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
    public class S6F11_EventReportSend_ProcessStatusEvent_Model : IXComModel
    {
        public Byte DATA_ID { get; set; }

        public Int32 CEID { get; set; }

        public string MODULE_ID { get; set; }

        public Byte MCMD { get; set; }

        public Byte MODULE_STATE { get; set; }

        public Byte PROC_STATE { get; set; }

        public Byte STEPNO { get; set; }

        public Byte PREV_STEPNO { get; set; }

        public List<S6F11_ProcessStatusEvent_LIST_1> ProcessStatusEvent_LIST_1 { get; set; }

        public S6F11_EventReportSend_ProcessStatusEvent_Model()
        {
            this.Stream = 6;
            this.Function = 11;
            this.FullName = "EventReportSend - ProcessStatusEvent";
            this.Name = "EventReportSend";
            this.SubName = "ProcessStatusEvent";
            this.IsUnDefined = false;

            this.DATA_ID = 0;
            this.CEID = 0;
            this.MODULE_ID = string.Empty;
            this.MCMD = 0;
            this.MODULE_STATE = 0;
            this.PROC_STATE = 0;
            this.STEPNO = 0;
            this.PREV_STEPNO = 0;
            this.ProcessStatusEvent_LIST_1 = new List<S6F11_ProcessStatusEvent_LIST_1>();
        }
    }

    public class S6F11_ProcessStatusEvent_LIST_1
    {
        public string IDENTIFICATION { get; set; }

        public Byte POSITION { get; set; }

        public string PROCESS_ACT { get; set; }

        public string PROCESSED_PROD { get; set; }

        public S6F11_ProcessStatusEvent_LIST_1()
        {
            this.IDENTIFICATION = string.Empty;
            this.POSITION = 0;
            this.PROCESS_ACT = string.Empty;
            this.PROCESSED_PROD = string.Empty;
        }
    }
}
