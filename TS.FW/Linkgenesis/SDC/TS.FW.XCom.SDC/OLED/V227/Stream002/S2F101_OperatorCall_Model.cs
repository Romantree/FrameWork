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
    public class S2F101_OperatorCall_Model : IXComModel
    {
        public Byte UINT1_1 { get; set; }

        public string MODULE_ID { get; set; }

        public List<S2F101_LIST_1> LIST_1 { get; set; }

        public S2F101_OperatorCall_Model()
        {
            this.Stream = 2;
            this.Function = 101;
            this.FullName = "OperatorCall";
            this.Name = "OperatorCall";
            this.SubName = "";
            this.IsUnDefined = false;

            this.UINT1_1 = 0;
            this.MODULE_ID = string.Empty;
            this.LIST_1 = new List<S2F101_LIST_1>();
        }
    }

    public class S2F101_LIST_1
    {
        public string MSG { get; set; }

        public S2F101_LIST_1()
        {
            this.MSG = string.Empty;
        }
    }
}
