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
    public class S1F6_FromatStateData_MaskStock_Model : IXComModel
    {
        public string MODULE_ID_1 { get; set; }

        public Byte SFCD { get; set; }

        public List<S1F6_MaskStock_LIST_1> MaskStock_LIST_1 { get; set; }

        public S1F6_FromatStateData_MaskStock_Model()
        {
            this.Stream = 1;
            this.Function = 6;
            this.FullName = "FromatStateData - MaskStock";
            this.Name = "FromatStateData";
            this.SubName = "MaskStock";
            this.IsUnDefined = false;

            this.MODULE_ID_1 = string.Empty;
            this.SFCD = 0;
            this.MaskStock_LIST_1 = new List<S1F6_MaskStock_LIST_1>();
        }
    }

    public class S1F6_MaskStock_LIST_1
    {
        public string MODULE_ID_2 { get; set; }

        public Byte MCMD { get; set; }

        public Byte MODULE_STATE { get; set; }

        public Byte PROC_STATE { get; set; }

        public Byte BYWHO { get; set; }

        public string OPERID { get; set; }

        public string PORTID_1 { get; set; }

        public Byte PORT_STATE { get; set; }

        public Byte PORT_TYPE { get; set; }

        public string PORT_MODE { get; set; }

        public Byte SORT_TYPE { get; set; }

        public Byte ENVIRONMENT { get; set; }

        public Byte REUSE_MODE { get; set; }

        public List<S1F6_MaskStock_LIST_2> MaskStock_LIST_2 { get; set; }

        public S1F6_MaskStock_LIST_1()
        {
            this.MODULE_ID_2 = string.Empty;
            this.MCMD = 0;
            this.MODULE_STATE = 0;
            this.PROC_STATE = 0;
            this.BYWHO = 0;
            this.OPERID = string.Empty;
            this.PORTID_1 = string.Empty;
            this.PORT_STATE = 0;
            this.PORT_TYPE = 0;
            this.PORT_MODE = string.Empty;
            this.SORT_TYPE = 0;
            this.ENVIRONMENT = 0;
            this.REUSE_MODE = 0;
            this.MaskStock_LIST_2 = new List<S1F6_MaskStock_LIST_2>();
        }
    }

    public class S1F6_MaskStock_LIST_2
    {
        public string CSTID { get; set; }

        public Byte CST_TYPE { get; set; }

        public string MAP_STIF { get; set; }

        public string CUR_STIF { get; set; }

        public Byte BATCH_ORDER { get; set; }

        public Byte POSITION { get; set; }

        public List<S1F6_MaskStock_LIST_3> MaskStock_LIST_3 { get; set; }

        public S1F6_MaskStock_LIST_2()
        {
            this.CSTID = string.Empty;
            this.CST_TYPE = 0;
            this.MAP_STIF = string.Empty;
            this.CUR_STIF = string.Empty;
            this.BATCH_ORDER = 0;
            this.POSITION = 0;
            this.MaskStock_LIST_3 = new List<S1F6_MaskStock_LIST_3>();
        }
    }

    public class S1F6_MaskStock_LIST_3
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

        public S1F6_MaskStock_LIST_3()
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
}
