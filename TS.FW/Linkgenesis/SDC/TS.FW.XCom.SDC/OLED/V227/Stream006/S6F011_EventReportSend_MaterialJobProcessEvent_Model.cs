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
    public class S6F11_EventReportSend_MaterialJobProcessEvent_Model : IXComModel
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

        public string PORTID_1 { get; set; }

        public Byte PORT_STATE { get; set; }

        public Byte PORT_TYPE { get; set; }

        public string PORT_MODE { get; set; }

        public Byte SORT_TYPE { get; set; }

        public Byte RPTID_3 { get; set; }

        public string CSTID { get; set; }

        public Byte CST_TYPE { get; set; }

        public string MAP_STIF { get; set; }

        public string CUR_STIF { get; set; }

        public Byte BATCH_ORDER { get; set; }

        public Byte RPTID_4 { get; set; }

        public List<S6F11_MaterialJobProcessEvent_LIST_1> MaterialJobProcessEvent_LIST_1 { get; set; }

        public S6F11_EventReportSend_MaterialJobProcessEvent_Model()
        {
            this.Stream = 6;
            this.Function = 11;
            this.FullName = "EventReportSend - MaterialJobProcessEvent";
            this.Name = "EventReportSend";
            this.SubName = "MaterialJobProcessEvent";
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
            this.PORTID_1 = string.Empty;
            this.PORT_STATE = 0;
            this.PORT_TYPE = 0;
            this.PORT_MODE = string.Empty;
            this.SORT_TYPE = 0;
            this.RPTID_3 = 0;
            this.CSTID = string.Empty;
            this.CST_TYPE = 0;
            this.MAP_STIF = string.Empty;
            this.CUR_STIF = string.Empty;
            this.BATCH_ORDER = 0;
            this.RPTID_4 = 0;
            this.MaterialJobProcessEvent_LIST_1 = new List<S6F11_MaterialJobProcessEvent_LIST_1>();
        }
    }

    public class S6F11_MaterialJobProcessEvent_LIST_1
    {
        public string MATERIAL_ID { get; set; }

        public string MATERIAL_SETID { get; set; }

        public string LOTID { get; set; }

        public string BATCHID { get; set; }

        public string JOBID { get; set; }

        public string PORTID_2 { get; set; }

        public string SLOTNO { get; set; }

        public string PRODUCT_TYPE { get; set; }

        public string PRODUCT_KIND { get; set; }

        public string PRODUCTID { get; set; }

        public string RUNSPECID { get; set; }

        public string LAYERID { get; set; }

        public string STEPID { get; set; }

        public string PPID { get; set; }

        public string FLOWID { get; set; }

        public Int32[] MATERIAL_SIZE { get; set; }

        public Int32 MATERIAL_THICKNESS { get; set; }

        public Int32 MATERIAL_STATE { get; set; }

        public string MATERIAL_ORDER { get; set; }

        public string COMMENT { get; set; }

        public string USE_COUNT { get; set; }

        public string JUDGEMENT { get; set; }

        public string REASON_CODE { get; set; }

        public string INS_FLAG { get; set; }

        public string LIBRARYID { get; set; }

        public string PRERUN_FLAG { get; set; }

        public string TURN_DIR { get; set; }

        public string FLIP_STATE { get; set; }

        public string WORK_STATE { get; set; }

        public string MULTI_USE { get; set; }

        public Int32 C_QTY_1 { get; set; }

        public Int32 O_QTY_1 { get; set; }

        public Int32 R_QTY_1 { get; set; }

        public Int32 N_QTY_1 { get; set; }

        public string STAGE_STATE { get; set; }

        public string LOCATION { get; set; }

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

        public List<S6F11_MaterialJobProcessEvent_LIST_2> MaterialJobProcessEvent_LIST_2 { get; set; }

        public S6F11_MaterialJobProcessEvent_LIST_1()
        {
            this.MATERIAL_ID = string.Empty;
            this.MATERIAL_SETID = string.Empty;
            this.LOTID = string.Empty;
            this.BATCHID = string.Empty;
            this.JOBID = string.Empty;
            this.PORTID_2 = string.Empty;
            this.SLOTNO = string.Empty;
            this.PRODUCT_TYPE = string.Empty;
            this.PRODUCT_KIND = string.Empty;
            this.PRODUCTID = string.Empty;
            this.RUNSPECID = string.Empty;
            this.LAYERID = string.Empty;
            this.STEPID = string.Empty;
            this.PPID = string.Empty;
            this.FLOWID = string.Empty;
            this.MATERIAL_SIZE = new Int32[2];
            this.MATERIAL_THICKNESS = 0;
            this.MATERIAL_STATE = 0;
            this.MATERIAL_ORDER = string.Empty;
            this.COMMENT = string.Empty;
            this.USE_COUNT = string.Empty;
            this.JUDGEMENT = string.Empty;
            this.REASON_CODE = string.Empty;
            this.INS_FLAG = string.Empty;
            this.LIBRARYID = string.Empty;
            this.PRERUN_FLAG = string.Empty;
            this.TURN_DIR = string.Empty;
            this.FLIP_STATE = string.Empty;
            this.WORK_STATE = string.Empty;
            this.MULTI_USE = string.Empty;
            this.C_QTY_1 = 0;
            this.O_QTY_1 = 0;
            this.R_QTY_1 = 0;
            this.N_QTY_1 = 0;
            this.STAGE_STATE = string.Empty;
            this.LOCATION = string.Empty;
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
            this.MaterialJobProcessEvent_LIST_2 = new List<S6F11_MaterialJobProcessEvent_LIST_2>();
        }
    }

    public class S6F11_MaterialJobProcessEvent_LIST_2
    {
        public string SUB_MATERIALID { get; set; }

        public string SUB_MATERIAL_KIND { get; set; }

        public string SUB_MATERIAL_TYPE { get; set; }

        public string SUB_MATERIAL_MODEL { get; set; }

        public string SUB_MATERIAL_MAKER { get; set; }

        public string SUB_MATERIAL_MATTER { get; set; }

        public Int32 SUB_MATERIAL_THICKNESS { get; set; }

        public Int32 SUB_MATERIAL_STATE { get; set; }

        public string SUB_POSITION { get; set; }

        public string SUB_LAYER { get; set; }

        public string SUB_DATE_FABIN { get; set; }

        public string SUB_DATE_DISCARD { get; set; }

        public string SUB_DATE_ETCHING { get; set; }

        public string SUB_DATE_SHIPPING { get; set; }

        public string SUB_JUDGEMENT { get; set; }

        public Int32 C_QTY_2 { get; set; }

        public Int32 O_QTY_2 { get; set; }

        public Int32 R_QTY_2 { get; set; }

        public Int32 N_QTY_2 { get; set; }

        public S6F11_MaterialJobProcessEvent_LIST_2()
        {
            this.SUB_MATERIALID = string.Empty;
            this.SUB_MATERIAL_KIND = string.Empty;
            this.SUB_MATERIAL_TYPE = string.Empty;
            this.SUB_MATERIAL_MODEL = string.Empty;
            this.SUB_MATERIAL_MAKER = string.Empty;
            this.SUB_MATERIAL_MATTER = string.Empty;
            this.SUB_MATERIAL_THICKNESS = 0;
            this.SUB_MATERIAL_STATE = 0;
            this.SUB_POSITION = string.Empty;
            this.SUB_LAYER = string.Empty;
            this.SUB_DATE_FABIN = string.Empty;
            this.SUB_DATE_DISCARD = string.Empty;
            this.SUB_DATE_ETCHING = string.Empty;
            this.SUB_DATE_SHIPPING = string.Empty;
            this.SUB_JUDGEMENT = string.Empty;
            this.C_QTY_2 = 0;
            this.O_QTY_2 = 0;
            this.R_QTY_2 = 0;
            this.N_QTY_2 = 0;
        }
    }
}
