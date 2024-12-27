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
    public class S8F1_Multi_UseDataSet_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public List<S8F1_LIST_1> LIST_1 { get; set; }

        public S8F1_Multi_UseDataSet_Model()
        {
            this.Stream = 8;
            this.Function = 1;
            this.FullName = "Multi UseDataSet";
            this.Name = "Multi UseDataSet";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.LIST_1 = new List<S8F1_LIST_1>();
        }
    }

    public class S8F1_LIST_1
    {
        public string DATA_TYPE { get; set; }

        public string ITEM_NAME { get; set; }

        public string ITEM_VALUE { get; set; }

        public string REFRENCE { get; set; }

        public S8F1_LIST_1()
        {
            this.DATA_TYPE = string.Empty;
            this.ITEM_NAME = string.Empty;
            this.ITEM_VALUE = string.Empty;
            this.REFRENCE = string.Empty;
        }
    }
}
