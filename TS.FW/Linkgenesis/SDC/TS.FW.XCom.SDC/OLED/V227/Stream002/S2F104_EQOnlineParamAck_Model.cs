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
    public class S2F104_EQOnlineParamAck_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Byte MIACK { get; set; }

        public List<S2F104_LIST_1> LIST_1 { get; set; }

        public S2F104_EQOnlineParamAck_Model()
        {
            this.Stream = 2;
            this.Function = 104;
            this.FullName = "EQOnlineParamAck";
            this.Name = "EQOnlineParamAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.MIACK = 0;
            this.LIST_1 = new List<S2F104_LIST_1>();
        }
    }

    public class S2F104_LIST_1
    {
        public Byte EOID { get; set; }

        public List<S2F104_LIST_2> LIST_2 { get; set; }

        public S2F104_LIST_1()
        {
            this.EOID = 0;
            this.LIST_2 = new List<S2F104_LIST_2>();
        }
    }

    public class S2F104_LIST_2
    {
        public Byte EOMD { get; set; }

        public Byte EAC { get; set; }

        public S2F104_LIST_2()
        {
            this.EOMD = 0;
            this.EAC = 0;
        }
    }
}
