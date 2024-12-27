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
    public class S2F103_EquipmentOnlineParameterChange_Model : IXComModel
    {
        public string MODULEID { get; set; }

        public List<S2F103_EOIDs> EOIDs { get; set; }

        public S2F103_EquipmentOnlineParameterChange_Model()
        {
            this.Stream = 2;
            this.Function = 103;
            this.FullName = "EquipmentOnlineParameterChange";
            this.Name = "EquipmentOnlineParameterChange";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULEID = string.Empty;
            this.EOIDs = new List<S2F103_EOIDs>();
        }
    }

    public class S2F103_EOIDs
    {
        public Byte EOID { get; set; }

        public List<S2F103_EOMDs> EOMDs { get; set; }

        public S2F103_EOIDs()
        {
            this.EOID = 0;
            this.EOMDs = new List<S2F103_EOMDs>();
        }
    }

    public class S2F103_EOMDs
    {
        public Byte EOMD { get; set; }

        public Byte EOV { get; set; }

        public S2F103_EOMDs()
        {
            this.EOMD = 0;
            this.EOV = 0;
        }
    }
}
