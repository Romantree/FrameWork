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
    public class S6F13_NameListVariableDataSend_LotDataCollection_Model : IXComModel
    {
        public Byte DATAID { get; set; }

        public Int32 CEID { get; set; }

        public Byte RPTID10 { get; set; }

        public List<S6F13_LotGlsCnt> LotGlsCnt { get; set; }

        public Byte RPTID9 { get; set; }

        public List<S6F13_LotModuleCnt> LotModuleCnt { get; set; }

        public S6F13_NameListVariableDataSend_LotDataCollection_Model()
        {
            this.Stream = 6;
            this.Function = 13;
            this.FullName = "NameListVariableDataSend_LotDataCollection";
            this.Name = "NameListVariableDataSend_LotDataCollection";
            this.SubName = "";
            this.IsUnDefined = false;

            this.DATAID = 0;
            this.CEID = 0;
            this.RPTID10 = 0;
            this.LotGlsCnt = new List<S6F13_LotGlsCnt>();
            this.RPTID9 = 0;
            this.LotModuleCnt = new List<S6F13_LotModuleCnt>();
        }
    }

    public class S6F13_LotGlsCnt
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

        public S6F13_LotGlsCnt()
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

    public class S6F13_LotModuleCnt
    {
        public string MODULEID { get; set; }

        public List<S6F13_LotDataCnt> LotDataCnt { get; set; }

        public S6F13_LotModuleCnt()
        {
            this.MODULEID = string.Empty;
            this.LotDataCnt = new List<S6F13_LotDataCnt>();
        }
    }

    public class S6F13_LotDataCnt
    {
        public string DATA_ITEM { get; set; }

        public string DATA_VALUE { get; set; }

        public S6F13_LotDataCnt()
        {
            this.DATA_ITEM = string.Empty;
            this.DATA_VALUE = string.Empty;
        }
    }
}
