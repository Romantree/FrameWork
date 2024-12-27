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
    public class S6F11_EventReportSend_ModuleMaterialAssembleEvent_Model : IXComModel
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

        public List<S6F11_ModuleMaterialAssembleEvent_LIST_1> ModuleMaterialAssembleEvent_LIST_1 { get; set; }

        public Byte RPTID_3 { get; set; }

        public List<S6F11_ModuleMaterialAssembleEvent_LIST_3> ModuleMaterialAssembleEvent_LIST_3 { get; set; }

        public S6F11_EventReportSend_ModuleMaterialAssembleEvent_Model()
        {
            this.Stream = 6;
            this.Function = 11;
            this.FullName = "EventReportSend - ModuleMaterialAssembleEvent";
            this.Name = "EventReportSend";
            this.SubName = "ModuleMaterialAssembleEvent";
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
            this.ModuleMaterialAssembleEvent_LIST_1 = new List<S6F11_ModuleMaterialAssembleEvent_LIST_1>();
            this.RPTID_3 = 0;
            this.ModuleMaterialAssembleEvent_LIST_3 = new List<S6F11_ModuleMaterialAssembleEvent_LIST_3>();
        }
    }

    public class S6F11_ModuleMaterialAssembleEvent_LIST_1
    {
        public string M_TRAYID { get; set; }

        public string M_BATCHID { get; set; }

        public Byte M_KIND { get; set; }

        public string M_TYPE { get; set; }

        public string M_MAKER { get; set; }

        public string M_CODE { get; set; }

        public string M_REVNO { get; set; }

        public string TRAY_STATE { get; set; }

        public Int64 TOTAL_QTY { get; set; }

        public Int64 USED_QTY { get; set; }

        public Int64 INEQP_QTY { get; set; }

        public Int64 REMAIN_QTY { get; set; }

        public Int64 NG_QTY { get; set; }

        public Int64 ASSEMBLE_QTY { get; set; }

        public string PORTID_1 { get; set; }

        public string PRODUCT_TYPE_1 { get; set; }

        public string PRODUCT_KIND { get; set; }

        public string PRODUCTID { get; set; }

        public string M_STEP { get; set; }

        public string COMMENT_1 { get; set; }

        public List<S6F11_ModuleMaterialAssembleEvent_LIST_2> ModuleMaterialAssembleEvent_LIST_2 { get; set; }

        public S6F11_ModuleMaterialAssembleEvent_LIST_1()
        {
            this.M_TRAYID = string.Empty;
            this.M_BATCHID = string.Empty;
            this.M_KIND = 0;
            this.M_TYPE = string.Empty;
            this.M_MAKER = string.Empty;
            this.M_CODE = string.Empty;
            this.M_REVNO = string.Empty;
            this.TRAY_STATE = string.Empty;
            this.TOTAL_QTY = 0;
            this.USED_QTY = 0;
            this.INEQP_QTY = 0;
            this.REMAIN_QTY = 0;
            this.NG_QTY = 0;
            this.ASSEMBLE_QTY = 0;
            this.PORTID_1 = string.Empty;
            this.PRODUCT_TYPE_1 = string.Empty;
            this.PRODUCT_KIND = string.Empty;
            this.PRODUCTID = string.Empty;
            this.M_STEP = string.Empty;
            this.COMMENT_1 = string.Empty;
            this.ModuleMaterialAssembleEvent_LIST_2 = new List<S6F11_ModuleMaterialAssembleEvent_LIST_2>();
        }
    }

    public class S6F11_ModuleMaterialAssembleEvent_LIST_2
    {
        public string M_ID { get; set; }

        public string M_LOC { get; set; }

        public string M_SUBLOC { get; set; }

        public string M_SLOT { get; set; }

        public string D_CODE { get; set; }

        public string TAG { get; set; }

        public S6F11_ModuleMaterialAssembleEvent_LIST_2()
        {
            this.M_ID = string.Empty;
            this.M_LOC = string.Empty;
            this.M_SUBLOC = string.Empty;
            this.M_SLOT = string.Empty;
            this.D_CODE = string.Empty;
            this.TAG = string.Empty;
        }
    }

    public class S6F11_ModuleMaterialAssembleEvent_LIST_3
    {
        public string H_GLASSID { get; set; }

        public string E_GLASSID { get; set; }

        public string LOTID { get; set; }

        public string BATCHID { get; set; }

        public string JOBID { get; set; }

        public string PORTID_2 { get; set; }

        public string SLOTNO { get; set; }

        public string PRODUCT_TYPE_2 { get; set; }

        public string PORDUCT_KIND { get; set; }

        public string PORDUCTID { get; set; }

        public string RUNSPECID { get; set; }

        public string LAYERID { get; set; }

        public string STEPID { get; set; }

        public string PPID { get; set; }

        public string FLOWID { get; set; }

        public Int32[] GLASS_SIZE { get; set; }

        public Int32 GLASS_THICKNESS { get; set; }

        public Int32 GLASS_STATE { get; set; }

        public string GLASS_ORDER { get; set; }

        public string COMMENT_2 { get; set; }

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

        public string OPTION_NAME_1 { get; set; }

        public string OPTION_VALUE_1 { get; set; }

        public string OPTION_NAME_2 { get; set; }

        public string OPTION_VALUE_2 { get; set; }

        public string OPTION_NAME_3 { get; set; }

        public string OPTION_VALUE_3 { get; set; }

        public string OPTION_NAME_4 { get; set; }

        public string OPTION_VALUE_4 { get; set; }

        public string OPTION_NAME_5 { get; set; }

        public string OPTION_VALUE_5 { get; set; }

        public S6F11_ModuleMaterialAssembleEvent_LIST_3()
        {
            this.H_GLASSID = string.Empty;
            this.E_GLASSID = string.Empty;
            this.LOTID = string.Empty;
            this.BATCHID = string.Empty;
            this.JOBID = string.Empty;
            this.PORTID_2 = string.Empty;
            this.SLOTNO = string.Empty;
            this.PRODUCT_TYPE_2 = string.Empty;
            this.PORDUCT_KIND = string.Empty;
            this.PORDUCTID = string.Empty;
            this.RUNSPECID = string.Empty;
            this.LAYERID = string.Empty;
            this.STEPID = string.Empty;
            this.PPID = string.Empty;
            this.FLOWID = string.Empty;
            this.GLASS_SIZE = new Int32[2];
            this.GLASS_THICKNESS = 0;
            this.GLASS_STATE = 0;
            this.GLASS_ORDER = string.Empty;
            this.COMMENT_2 = string.Empty;
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
            this.OPTION_NAME_1 = string.Empty;
            this.OPTION_VALUE_1 = string.Empty;
            this.OPTION_NAME_2 = string.Empty;
            this.OPTION_VALUE_2 = string.Empty;
            this.OPTION_NAME_3 = string.Empty;
            this.OPTION_VALUE_3 = string.Empty;
            this.OPTION_NAME_4 = string.Empty;
            this.OPTION_VALUE_4 = string.Empty;
            this.OPTION_NAME_5 = string.Empty;
            this.OPTION_VALUE_5 = string.Empty;
        }
    }
}
