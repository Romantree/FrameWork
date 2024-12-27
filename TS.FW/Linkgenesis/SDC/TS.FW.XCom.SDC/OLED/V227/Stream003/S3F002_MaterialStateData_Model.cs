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
    public class S3F2_MaterialStateData_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public List<S3F2_LIST_1> LIST_1 { get; set; }

        public S3F2_MaterialStateData_Model()
        {
            this.Stream = 3;
            this.Function = 2;
            this.FullName = "MaterialStateData";
            this.Name = "MaterialStateData";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.LIST_1 = new List<S3F2_LIST_1>();
        }
    }

    public class S3F2_LIST_1
    {
        public string MATERIAL_ID { get; set; }

        public string PRODUCT_TYPE { get; set; }

        public string LIBRARYID { get; set; }

        public string STAGE_STATE { get; set; }

        public Int32 MATERIAL_STATE { get; set; }

        public string LOCATION { get; set; }

        public Int32[] MATERIAL_SIZE { get; set; }

        public List<S3F2_LIST_2> LIST_2 { get; set; }

        public S3F2_LIST_1()
        {
            this.MATERIAL_ID = string.Empty;
            this.PRODUCT_TYPE = string.Empty;
            this.LIBRARYID = string.Empty;
            this.STAGE_STATE = string.Empty;
            this.MATERIAL_STATE = 0;
            this.LOCATION = string.Empty;
            this.MATERIAL_SIZE = new Int32[2];
            this.LIST_2 = new List<S3F2_LIST_2>();
        }
    }

    public class S3F2_LIST_2
    {
        public string PRODUCTID { get; set; }

        public string STEPID { get; set; }

        public string PPID { get; set; }

        public S3F2_LIST_2()
        {
            this.PRODUCTID = string.Empty;
            this.STEPID = string.Empty;
            this.PPID = string.Empty;
        }
    }
}
