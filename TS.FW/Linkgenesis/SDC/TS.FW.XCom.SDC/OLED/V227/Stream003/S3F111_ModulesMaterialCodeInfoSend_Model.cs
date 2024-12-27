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
    public class S3F111_ModulesMaterialCodeInfoSend_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public string BATCHID { get; set; }

        public string PRODUCTID { get; set; }

        public string STEPID { get; set; }

        public string PPID { get; set; }

        public List<S3F111_LIST_1> LIST_1 { get; set; }

        public string M_TYPE { get; set; }

        public string M_STEP { get; set; }

        public S3F111_ModulesMaterialCodeInfoSend_Model()
        {
            this.Stream = 3;
            this.Function = 111;
            this.FullName = "ModulesMaterialCodeInfoSend";
            this.Name = "ModulesMaterialCodeInfoSend";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.BATCHID = string.Empty;
            this.PRODUCTID = string.Empty;
            this.STEPID = string.Empty;
            this.PPID = string.Empty;
            this.LIST_1 = new List<S3F111_LIST_1>();
            this.M_TYPE = string.Empty;
            this.M_STEP = string.Empty;
        }
    }

    public class S3F111_LIST_1
    {
        public string M_CODE { get; set; }

        public S3F111_LIST_1()
        {
            this.M_CODE = string.Empty;
        }
    }
}
