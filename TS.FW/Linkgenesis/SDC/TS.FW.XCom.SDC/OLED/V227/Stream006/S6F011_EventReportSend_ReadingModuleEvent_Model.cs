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
    public class S6F11_EventReportSend_ReadingModuleEvent_Model : IXComModel
    {
        public Byte DATA_ID { get; set; }

        public Int32 CEID { get; set; }

        public Byte RPTID { get; set; }

        public string MODULE_ID { get; set; }

        public Byte MCMD { get; set; }

        public Byte MODULE_STATE { get; set; }

        public Byte PROC_STATE { get; set; }

        public Byte BYWHO { get; set; }

        public string OPERID { get; set; }

        public string READING_RATE { get; set; }

        public string TOTAL_COUNT { get; set; }

        public string SUCCESS_COUNT { get; set; }

        public List<S6F11_ReadingModuleEvent_LIST_1> ReadingModuleEvent_LIST_1 { get; set; }

        public S6F11_EventReportSend_ReadingModuleEvent_Model()
        {
            this.Stream = 6;
            this.Function = 11;
            this.FullName = "EventReportSend - ReadingModuleEvent";
            this.Name = "EventReportSend";
            this.SubName = "ReadingModuleEvent";
            this.IsUnDefined = false;

            this.DATA_ID = 0;
            this.CEID = 0;
            this.RPTID = 0;
            this.MODULE_ID = string.Empty;
            this.MCMD = 0;
            this.MODULE_STATE = 0;
            this.PROC_STATE = 0;
            this.BYWHO = 0;
            this.OPERID = string.Empty;
            this.READING_RATE = string.Empty;
            this.TOTAL_COUNT = string.Empty;
            this.SUCCESS_COUNT = string.Empty;
            this.ReadingModuleEvent_LIST_1 = new List<S6F11_ReadingModuleEvent_LIST_1>();
        }
    }

    public class S6F11_ReadingModuleEvent_LIST_1
    {
        public string READING_TIME { get; set; }

        public string IDENTIFICATION { get; set; }

        public string TRY_COUNT { get; set; }

        public S6F11_ReadingModuleEvent_LIST_1()
        {
            this.READING_TIME = string.Empty;
            this.IDENTIFICATION = string.Empty;
            this.TRY_COUNT = string.Empty;
        }
    }
}
