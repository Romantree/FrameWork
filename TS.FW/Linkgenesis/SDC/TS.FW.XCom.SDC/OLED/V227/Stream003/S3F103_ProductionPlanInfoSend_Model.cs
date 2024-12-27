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
    public class S3F103_ProductionPlanInfoSend_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Int32 PLCD { get; set; }

        public List<S3F103_LIST_1> LIST_1 { get; set; }

        public S3F103_ProductionPlanInfoSend_Model()
        {
            this.Stream = 3;
            this.Function = 103;
            this.FullName = "ProductionPlanInfoSend";
            this.Name = "ProductionPlanInfoSend";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.PLCD = 0;
            this.LIST_1 = new List<S3F103_LIST_1>();
        }
    }

    public class S3F103_LIST_1
    {
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

        public S3F103_LIST_1()
        {
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
