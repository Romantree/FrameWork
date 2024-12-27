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
    public class S5F5_AlarmListReq_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public List<S5F5_LIST_1> LIST_1 { get; set; }

        public S5F5_AlarmListReq_Model()
        {
            this.Stream = 5;
            this.Function = 5;
            this.FullName = "AlarmListReq";
            this.Name = "AlarmListReq";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.LIST_1 = new List<S5F5_LIST_1>();
        }
    }

    public class S5F5_LIST_1
    {
        public Int64 ALID { get; set; }

        public S5F5_LIST_1()
        {
            this.ALID = 0;
        }
    }
}
