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
    public class S8F3_Multi_UseDataInquiry_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public List<S8F3_LIST_1> LIST_1 { get; set; }

        public S8F3_Multi_UseDataInquiry_Model()
        {
            this.Stream = 8;
            this.Function = 3;
            this.FullName = "Multi UseDataInquiry";
            this.Name = "Multi UseDataInquiry";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.LIST_1 = new List<S8F3_LIST_1>();
        }
    }

    public class S8F3_LIST_1
    {
        public string DATA_TYPE { get; set; }

        public List<S8F3_LIST_2> LIST_2 { get; set; }

        public S8F3_LIST_1()
        {
            this.DATA_TYPE = string.Empty;
            this.LIST_2 = new List<S8F3_LIST_2>();
        }
    }

    public class S8F3_LIST_2
    {
        public string ITEM_NAME { get; set; }

        public S8F3_LIST_2()
        {
            this.ITEM_NAME = string.Empty;
        }
    }
}
