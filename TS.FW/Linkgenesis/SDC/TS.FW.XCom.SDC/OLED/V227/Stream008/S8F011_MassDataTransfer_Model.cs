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
    public class S8F11_MassDataTransfer_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public List<S8F11_LIST_1> LIST_1 { get; set; }

        public S8F11_MassDataTransfer_Model()
        {
            this.Stream = 8;
            this.Function = 11;
            this.FullName = "MassDataTransfer";
            this.Name = "MassDataTransfer";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.LIST_1 = new List<S8F11_LIST_1>();
        }
    }

    public class S8F11_LIST_1
    {
        public string CATEGORY { get; set; }

        public string REF_MODULE_ID { get; set; }

        public string REFERENCE { get; set; }

        public List<S8F11_LIST_2> LIST_2 { get; set; }

        public S8F11_LIST_1()
        {
            this.CATEGORY = string.Empty;
            this.REF_MODULE_ID = string.Empty;
            this.REFERENCE = string.Empty;
            this.LIST_2 = new List<S8F11_LIST_2>();
        }
    }

    public class S8F11_LIST_2
    {
        public string DATA_SET { get; set; }

        public S8F11_LIST_2()
        {
            this.DATA_SET = string.Empty;
        }
    }
}
