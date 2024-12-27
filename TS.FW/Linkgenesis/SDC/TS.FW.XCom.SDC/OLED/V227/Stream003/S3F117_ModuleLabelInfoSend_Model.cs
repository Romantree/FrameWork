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
    public class S3F117_ModuleLabelInfoSend_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public List<S3F117_LIST_1> LIST_1 { get; set; }

        public S3F117_ModuleLabelInfoSend_Model()
        {
            this.Stream = 3;
            this.Function = 117;
            this.FullName = "ModuleLabelInfoSend";
            this.Name = "ModuleLabelInfoSend";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.LIST_1 = new List<S3F117_LIST_1>();
        }
    }

    public class S3F117_LIST_1
    {
        public string PRODUCTID { get; set; }

        public List<S3F117_LIST_2> LIST_2 { get; set; }

        public S3F117_LIST_1()
        {
            this.PRODUCTID = string.Empty;
            this.LIST_2 = new List<S3F117_LIST_2>();
        }
    }

    public class S3F117_LIST_2
    {
        public string ATTRID { get; set; }

        public string ATTRDATA { get; set; }

        public S3F117_LIST_2()
        {
            this.ATTRID = string.Empty;
            this.ATTRDATA = string.Empty;
        }
    }
}
