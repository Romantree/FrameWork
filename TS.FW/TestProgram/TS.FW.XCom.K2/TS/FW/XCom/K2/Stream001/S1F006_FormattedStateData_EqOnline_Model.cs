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
    public class S1F6_FormattedStateData_EqOnline_Model : IXComModel
    {
        public string MODULEID { get; set; }

        public Byte SFCD { get; set; }

        public List<S1F6_EOIDs> EOIDs { get; set; }

        public S1F6_FormattedStateData_EqOnline_Model()
        {
            this.Stream = 1;
            this.Function = 6;
            this.FullName = "FormattedStateData_EqOnline";
            this.Name = "FormattedStateData_EqOnline";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULEID = string.Empty;
            this.SFCD = 0;
            this.EOIDs = new List<S1F6_EOIDs>();
        }
    }

    public class S1F6_EOIDs
    {
        public Byte EOID { get; set; }

        public List<S1F6_EOMDs> EOMDs { get; set; }

        public S1F6_EOIDs()
        {
            this.EOID = 0;
            this.EOMDs = new List<S1F6_EOMDs>();
        }
    }

    public class S1F6_EOMDs
    {
        public Byte EOMD { get; set; }

        public Byte EOV { get; set; }

        public S1F6_EOMDs()
        {
            this.EOMD = 0;
            this.EOV = 0;
        }
    }
}
