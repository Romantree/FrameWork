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
    public class S2F29_EQConstantNamelistReq_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public List<S2F29_LIST_1> LIST_1 { get; set; }

        public S2F29_EQConstantNamelistReq_Model()
        {
            this.Stream = 2;
            this.Function = 29;
            this.FullName = "EQConstantNamelistReq";
            this.Name = "EQConstantNamelistReq";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.LIST_1 = new List<S2F29_LIST_1>();
        }
    }

    public class S2F29_LIST_1
    {
        public Int32 ECID { get; set; }

        public S2F29_LIST_1()
        {
            this.ECID = 0;
        }
    }
}
