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
    public class S6F11_EventReportSend_MaskStockEQEvent_Model : IXComModel
    {
        public Byte DATA_ID { get; set; }

        public Int32 CEID { get; set; }

        public string MODULE_ID_1 { get; set; }

        public Byte MCMD { get; set; }

        public Byte MODULE_STATE { get; set; }

        public Byte PROC_STATE { get; set; }

        public Byte BYWHO { get; set; }

        public string OPERID { get; set; }

        public Byte ENVIRONMENT { get; set; }

        public Byte REUSE_MODE { get; set; }

        public Byte OLD_STATE { get; set; }

        public Byte NEW_STATE { get; set; }

        public string MODULE_ID_2 { get; set; }

        public Byte ALCD { get; set; }

        public Int64 ALID { get; set; }

        public string ALTX { get; set; }

        public S6F11_EventReportSend_MaskStockEQEvent_Model()
        {
            this.Stream = 6;
            this.Function = 11;
            this.FullName = "EventReportSend - MaskStockEQEvent";
            this.Name = "EventReportSend";
            this.SubName = "MaskStockEQEvent";
            this.IsUnDefined = false;

            this.DATA_ID = 0;
            this.CEID = 0;
            this.MODULE_ID_1 = string.Empty;
            this.MCMD = 0;
            this.MODULE_STATE = 0;
            this.PROC_STATE = 0;
            this.BYWHO = 0;
            this.OPERID = string.Empty;
            this.ENVIRONMENT = 0;
            this.REUSE_MODE = 0;
            this.OLD_STATE = 0;
            this.NEW_STATE = 0;
            this.MODULE_ID_2 = string.Empty;
            this.ALCD = 0;
            this.ALID = 0;
            this.ALTX = string.Empty;
        }
    }
}
