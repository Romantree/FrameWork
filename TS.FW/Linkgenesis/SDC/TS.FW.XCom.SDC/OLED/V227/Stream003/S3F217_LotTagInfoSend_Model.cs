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
    public class S3F217_LotTagInfoSend_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public List<S3F217_LIST_1> LIST_1 { get; set; }

        public S3F217_LotTagInfoSend_Model()
        {
            this.Stream = 3;
            this.Function = 217;
            this.FullName = "LotTagInfoSend";
            this.Name = "LotTagInfoSend";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.LIST_1 = new List<S3F217_LIST_1>();
        }
    }

    public class S3F217_LIST_1
    {
        public string LOT_ID { get; set; }

        public string PRODUCTID { get; set; }

        public string PROD_DES { get; set; }

        public string T_C_QTY { get; set; }

        public string LOT_GRADE { get; set; }

        public string PRODUCT_TYPE { get; set; }

        public string PRODUCT_PARTNO { get; set; }

        public string TOSITE { get; set; }

        public string SERIAL_NO { get; set; }

        public string RESULT1 { get; set; }

        public string RESULT2 { get; set; }

        public string RESULT3 { get; set; }

        public string RESULT4 { get; set; }

        public string PROCESS_ORDER { get; set; }

        public string PROCESS_NAME { get; set; }

        public List<S3F217_LIST_2> LIST_2 { get; set; }

        public S3F217_LIST_1()
        {
            this.LOT_ID = string.Empty;
            this.PRODUCTID = string.Empty;
            this.PROD_DES = string.Empty;
            this.T_C_QTY = string.Empty;
            this.LOT_GRADE = string.Empty;
            this.PRODUCT_TYPE = string.Empty;
            this.PRODUCT_PARTNO = string.Empty;
            this.TOSITE = string.Empty;
            this.SERIAL_NO = string.Empty;
            this.RESULT1 = string.Empty;
            this.RESULT2 = string.Empty;
            this.RESULT3 = string.Empty;
            this.RESULT4 = string.Empty;
            this.PROCESS_ORDER = string.Empty;
            this.PROCESS_NAME = string.Empty;
            this.LIST_2 = new List<S3F217_LIST_2>();
        }
    }

    public class S3F217_LIST_2
    {
        public string ITEM_NAME { get; set; }

        public string ITEM_VALUE { get; set; }

        public S3F217_LIST_2()
        {
            this.ITEM_NAME = string.Empty;
            this.ITEM_VALUE = string.Empty;
        }
    }
}
