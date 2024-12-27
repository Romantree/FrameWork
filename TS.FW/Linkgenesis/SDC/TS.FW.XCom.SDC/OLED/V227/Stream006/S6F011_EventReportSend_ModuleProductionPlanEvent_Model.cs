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
    public class S6F11_EventReportSend_ModuleProductionPlanEvent_Model : IXComModel
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

        public string PLAN_ID { get; set; }

        public Int32 PLAN_ORDER { get; set; }

        public string PLAN_STATE { get; set; }

        public Byte PRIORITY { get; set; }

        public string PRODUCT_TYPE { get; set; }

        public string PRODUCT_KIND { get; set; }

        public string PRODUCTID { get; set; }

        public string RUNSPECID { get; set; }

        public string LAYERID { get; set; }

        public string STEPID { get; set; }

        public string PPID { get; set; }

        public Int64 PLAN_SIZE { get; set; }

        public string MAKER { get; set; }

        public string THICKNESS { get; set; }

        public string INPUT_LINE { get; set; }

        public string COMMENT { get; set; }

        public Int64 TOTAL_QTY { get; set; }

        public Int64 USED_QTY { get; set; }

        public Int64 INEQP_QTY { get; set; }

        public Int64 REMAIN_QTY { get; set; }

        public Int64 NG_QTY { get; set; }

        public Int64 ASSEMBLE_QTY { get; set; }

        public S6F11_EventReportSend_ModuleProductionPlanEvent_Model()
        {
            this.Stream = 6;
            this.Function = 11;
            this.FullName = "EventReportSend - ModuleProductionPlanEvent";
            this.Name = "EventReportSend";
            this.SubName = "ModuleProductionPlanEvent";
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
            this.PLAN_ID = string.Empty;
            this.PLAN_ORDER = 0;
            this.PLAN_STATE = string.Empty;
            this.PRIORITY = 0;
            this.PRODUCT_TYPE = string.Empty;
            this.PRODUCT_KIND = string.Empty;
            this.PRODUCTID = string.Empty;
            this.RUNSPECID = string.Empty;
            this.LAYERID = string.Empty;
            this.STEPID = string.Empty;
            this.PPID = string.Empty;
            this.PLAN_SIZE = 0;
            this.MAKER = string.Empty;
            this.THICKNESS = string.Empty;
            this.INPUT_LINE = string.Empty;
            this.COMMENT = string.Empty;
            this.TOTAL_QTY = 0;
            this.USED_QTY = 0;
            this.INEQP_QTY = 0;
            this.REMAIN_QTY = 0;
            this.NG_QTY = 0;
            this.ASSEMBLE_QTY = 0;
        }
    }
}
