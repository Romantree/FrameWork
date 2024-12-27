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
    public class S16F121_RPC_ProcessChangeDataListReq_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public List<S16F121_LIST_1> LIST_1 { get; set; }

        public S16F121_RPC_ProcessChangeDataListReq_Model()
        {
            this.Stream = 16;
            this.Function = 121;
            this.FullName = "RPC_ProcessChangeDataListReq";
            this.Name = "RPC_ProcessChangeDataListReq";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.LIST_1 = new List<S16F121_LIST_1>();
        }
    }

    public class S16F121_LIST_1
    {
        public string H_GLASSID { get; set; }

        public S16F121_LIST_1()
        {
            this.H_GLASSID = string.Empty;
        }
    }
}
