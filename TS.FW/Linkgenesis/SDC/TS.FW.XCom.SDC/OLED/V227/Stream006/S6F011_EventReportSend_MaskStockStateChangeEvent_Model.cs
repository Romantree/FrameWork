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
    public class S6F11_EventReportSend_MaskStockStateChangeEvent_Model : IXComModel
    {
        public Byte DATA_ID { get; set; }

        public Int32 CEID { get; set; }

        public string MODULE_ID { get; set; }

        public Byte MCMD { get; set; }

        public Byte MODULE_STATE { get; set; }

        public Byte PROC_STATE { get; set; }

        public Byte BYWHO { get; set; }

        public string OPERID { get; set; }

        public string PORTID { get; set; }

        public Byte PORT_STATE { get; set; }

        public Byte PORT_TYPE { get; set; }

        public string PORT_MODE { get; set; }

        public Byte SORT_TYPE { get; set; }

        public Byte ENVIRONMENT { get; set; }

        public Byte REUSE_MODE { get; set; }

        public List<S6F11_MaskStockStateChangeEvent_LIST_1> MaskStockStateChangeEvent_LIST_1 { get; set; }

        public S6F11_EventReportSend_MaskStockStateChangeEvent_Model()
        {
            this.Stream = 6;
            this.Function = 11;
            this.FullName = "EventReportSend - MaskStockStateChangeEvent";
            this.Name = "EventReportSend";
            this.SubName = "MaskStockStateChangeEvent";
            this.IsUnDefined = false;

            this.DATA_ID = 0;
            this.CEID = 0;
            this.MODULE_ID = string.Empty;
            this.MCMD = 0;
            this.MODULE_STATE = 0;
            this.PROC_STATE = 0;
            this.BYWHO = 0;
            this.OPERID = string.Empty;
            this.PORTID = string.Empty;
            this.PORT_STATE = 0;
            this.PORT_TYPE = 0;
            this.PORT_MODE = string.Empty;
            this.SORT_TYPE = 0;
            this.ENVIRONMENT = 0;
            this.REUSE_MODE = 0;
            this.MaskStockStateChangeEvent_LIST_1 = new List<S6F11_MaskStockStateChangeEvent_LIST_1>();
        }
    }

    public class S6F11_MaskStockStateChangeEvent_LIST_1
    {
        public string CSTID { get; set; }

        public Byte CST_TYPE { get; set; }

        public string MAP_STIF { get; set; }

        public string CUR_STIF { get; set; }

        public Byte BATCH_ORDER { get; set; }

        public Byte POSITION { get; set; }

        public List<S6F11_MaskStockStateChangeEvent_LIST_2> MaskStockStateChangeEvent_LIST_2 { get; set; }

        public S6F11_MaskStockStateChangeEvent_LIST_1()
        {
            this.CSTID = string.Empty;
            this.CST_TYPE = 0;
            this.MAP_STIF = string.Empty;
            this.CUR_STIF = string.Empty;
            this.BATCH_ORDER = 0;
            this.POSITION = 0;
            this.MaskStockStateChangeEvent_LIST_2 = new List<S6F11_MaskStockStateChangeEvent_LIST_2>();
        }
    }

    public class S6F11_MaskStockStateChangeEvent_LIST_2
    {
        public string MATERIAL_ID { get; set; }

        public string SLOTNO { get; set; }

        public S6F11_MaskStockStateChangeEvent_LIST_2()
        {
            this.MATERIAL_ID = string.Empty;
            this.SLOTNO = string.Empty;
        }
    }
}
