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
    public class S6F13_NameListVariableDataSend_MaterialData_Model : IXComModel
    {
        public Byte DATA_ID { get; set; }

        public Int32 CEID { get; set; }

        public Byte RPTID_1 { get; set; }

        public List<S6F13_MaterialData_LIST_1> MaterialData_LIST_1 { get; set; }

        public Byte RPTID_2 { get; set; }

        public List<S6F13_MaterialData_LIST_2> MaterialData_LIST_2 { get; set; }

        public Byte RPTID_3 { get; set; }

        public List<S6F13_MaterialData_LIST_4> MaterialData_LIST_4 { get; set; }

        public S6F13_NameListVariableDataSend_MaterialData_Model()
        {
            this.Stream = 6;
            this.Function = 13;
            this.FullName = "NameListVariableDataSend - MaterialData";
            this.Name = "NameListVariableDataSend";
            this.SubName = "MaterialData";
            this.IsUnDefined = false;

            this.DATA_ID = 0;
            this.CEID = 0;
            this.RPTID_1 = 0;
            this.MaterialData_LIST_1 = new List<S6F13_MaterialData_LIST_1>();
            this.RPTID_2 = 0;
            this.MaterialData_LIST_2 = new List<S6F13_MaterialData_LIST_2>();
            this.RPTID_3 = 0;
            this.MaterialData_LIST_4 = new List<S6F13_MaterialData_LIST_4>();
        }
    }

    public class S6F13_MaterialData_LIST_1
    {
        public string MATERIAL_ID { get; set; }

        public string MATERIAL_SETID { get; set; }

        public string LOTID { get; set; }

        public string BATCHID { get; set; }

        public string JOBID { get; set; }

        public string PORTID { get; set; }

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

        public S6F13_MaterialData_LIST_1()
        {
            this.MATERIAL_ID = string.Empty;
            this.MATERIAL_SETID = string.Empty;
            this.LOTID = string.Empty;
            this.BATCHID = string.Empty;
            this.JOBID = string.Empty;
            this.PORTID = string.Empty;
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
        }
    }

    public class S6F13_MaterialData_LIST_2
    {
        public string MODULE_ID { get; set; }

        public List<S6F13_MaterialData_LIST_3> MaterialData_LIST_3 { get; set; }

        public S6F13_MaterialData_LIST_2()
        {
            this.MODULE_ID = string.Empty;
            this.MaterialData_LIST_3 = new List<S6F13_MaterialData_LIST_3>();
        }
    }

    public class S6F13_MaterialData_LIST_3
    {
        public string ITEM_NAME { get; set; }

        public string ITEM_VALUE { get; set; }

        public S6F13_MaterialData_LIST_3()
        {
            this.ITEM_NAME = string.Empty;
            this.ITEM_VALUE = string.Empty;
        }
    }

    public class S6F13_MaterialData_LIST_4
    {
        public string FLOW_PATH { get; set; }

        public string FLOW_MODULE_ID { get; set; }

        public S6F13_MaterialData_LIST_4()
        {
            this.FLOW_PATH = string.Empty;
            this.FLOW_MODULE_ID = string.Empty;
        }
    }
}
