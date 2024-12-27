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
    public class S16F105_APC_DataDeleteCommand_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public List<S16F105_LIST_1> LIST_1 { get; set; }

        public S16F105_APC_DataDeleteCommand_Model()
        {
            this.Stream = 16;
            this.Function = 105;
            this.FullName = "APC_DataDeleteCommand";
            this.Name = "APC_DataDeleteCommand";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.LIST_1 = new List<S16F105_LIST_1>();
        }
    }

    public class S16F105_LIST_1
    {
        public string H_GLASSID { get; set; }

        public S16F105_LIST_1()
        {
            this.H_GLASSID = string.Empty;
        }
    }
}
