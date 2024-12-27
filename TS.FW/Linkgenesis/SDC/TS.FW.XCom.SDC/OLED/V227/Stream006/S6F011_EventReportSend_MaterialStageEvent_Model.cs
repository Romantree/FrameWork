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
    public class S6F11_EventReportSend_MaterialStageEvent_Model : IXComModel
    {
        public Byte DATA_ID { get; set; }

        public Int32 CEID { get; set; }

        public Byte RPTID_1 { get; set; }

        public string MODULE_ID { get; set; }

        public Byte MCMD { get; set; }

        public Byte MODULE_STATE { get; set; }

        public Byte PROC_STATE { get; set; }

        public Byte BYWHO { get; set; }

        public string OPERID { get; set; }

        public Byte RPTID_2 { get; set; }

        public Byte OLD_STATE { get; set; }

        public Byte NEW_STATE { get; set; }

        public Byte RPTID_3 { get; set; }

        public string TRANS_POSSIBLE_STATE { get; set; }

        public string LIBRARYID { get; set; }

        public string STAGE_STATE { get; set; }

        public string LOCATION { get; set; }

        public string MATERIAL_ID { get; set; }

        public Int32 MATERIAL_STATE { get; set; }

        public string INS_FLAG { get; set; }

        public S6F11_EventReportSend_MaterialStageEvent_Model()
        {
            this.Stream = 6;
            this.Function = 11;
            this.FullName = "EventReportSend - MaterialStageEvent";
            this.Name = "EventReportSend";
            this.SubName = "MaterialStageEvent";
            this.IsUnDefined = false;

            this.DATA_ID = 0;
            this.CEID = 0;
            this.RPTID_1 = 0;
            this.MODULE_ID = string.Empty;
            this.MCMD = 0;
            this.MODULE_STATE = 0;
            this.PROC_STATE = 0;
            this.BYWHO = 0;
            this.OPERID = string.Empty;
            this.RPTID_2 = 0;
            this.OLD_STATE = 0;
            this.NEW_STATE = 0;
            this.RPTID_3 = 0;
            this.TRANS_POSSIBLE_STATE = string.Empty;
            this.LIBRARYID = string.Empty;
            this.STAGE_STATE = string.Empty;
            this.LOCATION = string.Empty;
            this.MATERIAL_ID = string.Empty;
            this.MATERIAL_STATE = 0;
            this.INS_FLAG = string.Empty;
        }
    }
}
