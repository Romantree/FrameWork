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
    public class S6F11_EventReportSend_JobProcess_Model : IXComModel
    {
        public Byte DATAID { get; set; }

        public Int32 CEID { get; set; }

        public Byte RPTID1 { get; set; }

        public string MODULEID { get; set; }

        public Byte MCMD { get; set; }

        public Byte MODULE_STATE { get; set; }

        public Byte PROC_STATE { get; set; }

        public Byte BYWHO { get; set; }

        public string OPERID { get; set; }

        public Byte RPTID2 { get; set; }

        public string PORTID { get; set; }

        public Byte PORT_TYPE { get; set; }

        public string PORT_MODE { get; set; }

        public Byte SORT_TYPE { get; set; }

        public Byte RPTID3 { get; set; }

        public string CSTID { get; set; }

        public Byte CST_TYPE { get; set; }

        public Byte BATCH_ORDER { get; set; }

        public Byte RPTID4 { get; set; }

        public List<S6F11_JobGlsCnt> JobGlsCnt { get; set; }

        public S6F11_EventReportSend_JobProcess_Model()
        {
            this.Stream = 6;
            this.Function = 11;
            this.FullName = "EventReportSend_JobProcess";
            this.Name = "EventReportSend_JobProcess";
            this.SubName = "";
            this.IsUnDefined = false;

            this.DATAID = 0;
            this.CEID = 0;
            this.RPTID1 = 0;
            this.MODULEID = string.Empty;
            this.MCMD = 0;
            this.MODULE_STATE = 0;
            this.PROC_STATE = 0;
            this.BYWHO = 0;
            this.OPERID = string.Empty;
            this.RPTID2 = 0;
            this.PORTID = string.Empty;
            this.PORT_TYPE = 0;
            this.PORT_MODE = string.Empty;
            this.SORT_TYPE = 0;
            this.RPTID3 = 0;
            this.CSTID = string.Empty;
            this.CST_TYPE = 0;
            this.BATCH_ORDER = 0;
            this.RPTID4 = 0;
            this.JobGlsCnt = new List<S6F11_JobGlsCnt>();
        }
    }

    public class S6F11_JobGlsCnt
    {
        public string H_GLASSID { get; set; }

        public string E_GLASSID { get; set; }

        public string LOTID { get; set; }

        public string BATCHID { get; set; }

        public string JOBID { get; set; }

        public string PORTID_Glass { get; set; }

        public string SLOTNO { get; set; }

        public string PRODUCT_TYPE { get; set; }

        public string PRODUCT_KIND { get; set; }

        public string PRODUCTID { get; set; }

        public string RUNSPECID { get; set; }

        public string LAYERID { get; set; }

        public string STEPID { get; set; }

        public string PPID { get; set; }

        public string FLOWID { get; set; }

        public Int32[] GLASS_SIZE { get; set; }

        public Int32 GLASS_THICKNESS { get; set; }

        public Int32 GLASS_STATE { get; set; }

        public string GLASS_ORDER { get; set; }

        public string COMMENT { get; set; }

        public string USE_COUNT { get; set; }

        public string JUDGEMENT { get; set; }

        public string REASON_CODE { get; set; }

        public string INS_FLAG { get; set; }

        public string ENC_FLAG { get; set; }

        public string PRERUN_FLAG { get; set; }

        public string TURN_DIR { get; set; }

        public string FLIP_STATE { get; set; }

        public string WORK_STATE { get; set; }

        public string MULTI_USE { get; set; }

        public string PAIR_GLASSID { get; set; }

        public string PAIR_PPID { get; set; }

        public string OPTION_NAME1 { get; set; }

        public string OPTION_VALUE1 { get; set; }

        public string OPTION_NAME2 { get; set; }

        public string OPTION_VALUE2 { get; set; }

        public string OPTION_NAME3 { get; set; }

        public string OPTION_VALUE3 { get; set; }

        public string OPTION_NAME4 { get; set; }

        public string OPTION_VALUE4 { get; set; }

        public string OPTION_NAME5 { get; set; }

        public string OPTION_VALUE5 { get; set; }

        public S6F11_JobGlsCnt()
        {
            this.H_GLASSID = string.Empty;
            this.E_GLASSID = string.Empty;
            this.LOTID = string.Empty;
            this.BATCHID = string.Empty;
            this.JOBID = string.Empty;
            this.PORTID_Glass = string.Empty;
            this.SLOTNO = string.Empty;
            this.PRODUCT_TYPE = string.Empty;
            this.PRODUCT_KIND = string.Empty;
            this.PRODUCTID = string.Empty;
            this.RUNSPECID = string.Empty;
            this.LAYERID = string.Empty;
            this.STEPID = string.Empty;
            this.PPID = string.Empty;
            this.FLOWID = string.Empty;
            this.GLASS_SIZE = new Int32[2];
            this.GLASS_THICKNESS = 0;
            this.GLASS_STATE = 0;
            this.GLASS_ORDER = string.Empty;
            this.COMMENT = string.Empty;
            this.USE_COUNT = string.Empty;
            this.JUDGEMENT = string.Empty;
            this.REASON_CODE = string.Empty;
            this.INS_FLAG = string.Empty;
            this.ENC_FLAG = string.Empty;
            this.PRERUN_FLAG = string.Empty;
            this.TURN_DIR = string.Empty;
            this.FLIP_STATE = string.Empty;
            this.WORK_STATE = string.Empty;
            this.MULTI_USE = string.Empty;
            this.PAIR_GLASSID = string.Empty;
            this.PAIR_PPID = string.Empty;
            this.OPTION_NAME1 = string.Empty;
            this.OPTION_VALUE1 = string.Empty;
            this.OPTION_NAME2 = string.Empty;
            this.OPTION_VALUE2 = string.Empty;
            this.OPTION_NAME3 = string.Empty;
            this.OPTION_VALUE3 = string.Empty;
            this.OPTION_NAME4 = string.Empty;
            this.OPTION_VALUE4 = string.Empty;
            this.OPTION_NAME5 = string.Empty;
            this.OPTION_VALUE5 = string.Empty;
        }
    }
}
