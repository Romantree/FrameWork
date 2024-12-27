using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.XCom;
using Newtonsoft.Json;
using XCOMLib;

namespace TS.FW.XCom.K2
{
    public class S2F104_EquipmentOnlineParameterAck_Model : IXComModel
    {
        public string MODULEID { get; set; }

        public Byte MIACK { get; set; }

        public List<S2F104_EOIDs> EOIDs { get; set; }

        public S2F104_EquipmentOnlineParameterAck_Model()
        {
            this.Stream = 2;
            this.Function = 104;
            this.FullName = "EquipmentOnlineParameterAck";
            this.Name = "EquipmentOnlineParameterAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULEID = string.Empty;
            this.MIACK = 0;
            this.EOIDs = new List<S2F104_EOIDs>();
        }
    }

    public class S2F104_EOIDs
    {
        public Byte EOID { get; set; }

        public List<S2F104_EOMDs> EOMDs { get; set; }

        public S2F104_EOIDs()
        {
            this.EOID = 0;
            this.EOMDs = new List<S2F104_EOMDs>();
        }
    }

    public class S2F104_EOMDs
    {
        public Byte EOMD { get; set; }

        public Byte EAC { get; set; }

        public S2F104_EOMDs()
        {
            this.EOMD = 0;
            this.EAC = 0;
        }
    }
}
