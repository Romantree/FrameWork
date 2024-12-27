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
    public class S1F3_SelectedEQStateReq_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public List<S1F3_LIST_1> LIST_1 { get; set; }

        public S1F3_SelectedEQStateReq_Model()
        {
            this.Stream = 1;
            this.Function = 3;
            this.FullName = "SelectedEQStateReq";
            this.Name = "SelectedEQStateReq";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.LIST_1 = new List<S1F3_LIST_1>();
        }
    }

    public class S1F3_LIST_1
    {
        public Int32 SVID { get; set; }

        public S1F3_LIST_1()
        {
            this.SVID = 0;
        }
    }
}
