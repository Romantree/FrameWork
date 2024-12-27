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
    public class S3F119_ModuleLabelSerialQuantityInfoSend_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public string PRODUCTID { get; set; }

        public List<S3F119_LIST_1> LIST_1 { get; set; }

        public List<S3F119_LIST_2> LIST_2 { get; set; }

        public S3F119_ModuleLabelSerialQuantityInfoSend_Model()
        {
            this.Stream = 3;
            this.Function = 119;
            this.FullName = "ModuleLabelSerialQuantityInfoSend";
            this.Name = "ModuleLabelSerialQuantityInfoSend";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.PRODUCTID = string.Empty;
            this.LIST_1 = new List<S3F119_LIST_1>();
            this.LIST_2 = new List<S3F119_LIST_2>();
        }
    }

    public class S3F119_LIST_1
    {
        public string SERIAL { get; set; }

        public S3F119_LIST_1()
        {
            this.SERIAL = string.Empty;
        }
    }

    public class S3F119_LIST_2
    {
        public string OPTION_NAME { get; set; }

        public string OPTION_VALUE { get; set; }

        public S3F119_LIST_2()
        {
            this.OPTION_NAME = string.Empty;
            this.OPTION_VALUE = string.Empty;
        }
    }
}
