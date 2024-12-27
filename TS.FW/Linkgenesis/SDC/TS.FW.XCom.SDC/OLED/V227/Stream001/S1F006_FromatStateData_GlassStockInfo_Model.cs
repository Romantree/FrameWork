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
    public class S1F6_FromatStateData_GlassStockInfo_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Byte SFCD { get; set; }

        public List<S1F6_GlassStockInfo_LIST_1> GlassStockInfo_LIST_1 { get; set; }

        public S1F6_FromatStateData_GlassStockInfo_Model()
        {
            this.Stream = 1;
            this.Function = 6;
            this.FullName = "FromatStateData - GlassStockInfo";
            this.Name = "FromatStateData";
            this.SubName = "GlassStockInfo";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.SFCD = 0;
            this.GlassStockInfo_LIST_1 = new List<S1F6_GlassStockInfo_LIST_1>();
        }
    }

    public class S1F6_GlassStockInfo_LIST_1
    {
        public string PORTID_1 { get; set; }

        public Byte PORT_STATE { get; set; }

        public Byte PORT_TYPE { get; set; }

        public string PORT_MODE { get; set; }

        public Byte SORT_TYPE { get; set; }

        public string CSTID { get; set; }

        public Byte CST_TYPE { get; set; }

        public string MAP_STIF { get; set; }

        public string CUR_STIF { get; set; }

        public Byte BATCH_ORDER { get; set; }

        public List<S1F6_GlassStockInfo_LIST_2> GlassStockInfo_LIST_2 { get; set; }

        public S1F6_GlassStockInfo_LIST_1()
        {
            this.PORTID_1 = string.Empty;
            this.PORT_STATE = 0;
            this.PORT_TYPE = 0;
            this.PORT_MODE = string.Empty;
            this.SORT_TYPE = 0;
            this.CSTID = string.Empty;
            this.CST_TYPE = 0;
            this.MAP_STIF = string.Empty;
            this.CUR_STIF = string.Empty;
            this.BATCH_ORDER = 0;
            this.GlassStockInfo_LIST_2 = new List<S1F6_GlassStockInfo_LIST_2>();
        }
    }

    public class S1F6_GlassStockInfo_LIST_2
    {
        public string H_GLASSID { get; set; }

        public string E_GLASSID { get; set; }

        public string LOTID { get; set; }

        public string BATCHID { get; set; }

        public string JOBID { get; set; }

        public string PORTID_2 { get; set; }

        public string SLOTNO { get; set; }

        public string PRODUCT_TYPE { get; set; }

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

        public S1F6_GlassStockInfo_LIST_2()
        {
            this.H_GLASSID = string.Empty;
            this.E_GLASSID = string.Empty;
            this.LOTID = string.Empty;
            this.BATCHID = string.Empty;
            this.JOBID = string.Empty;
            this.PORTID_2 = string.Empty;
            this.SLOTNO = string.Empty;
            this.PRODUCT_TYPE = string.Empty;
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
