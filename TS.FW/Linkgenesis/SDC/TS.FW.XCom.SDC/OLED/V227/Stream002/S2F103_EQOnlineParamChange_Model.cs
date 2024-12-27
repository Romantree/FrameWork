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
    public class S2F103_EQOnlineParamChange_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public List<S2F103_LIST_1> LIST_1 { get; set; }

        public S2F103_EQOnlineParamChange_Model()
        {
            this.Stream = 2;
            this.Function = 103;
            this.FullName = "EQOnlineParamChange";
            this.Name = "EQOnlineParamChange";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.LIST_1 = new List<S2F103_LIST_1>();
        }
    }

    public class S2F103_LIST_1
    {
        public Byte EOID { get; set; }

        public List<S2F103_LIST_2> LIST_2 { get; set; }

        public S2F103_LIST_1()
        {
            this.EOID = 0;
            this.LIST_2 = new List<S2F103_LIST_2>();
        }
    }

    public class S2F103_LIST_2
    {
        public Byte EOMD { get; set; }

        public Byte EOV { get; set; }

        public S2F103_LIST_2()
        {
            this.EOMD = 0;
            this.EOV = 0;
        }
    }
}
