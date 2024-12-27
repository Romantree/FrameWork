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
    public class S1F5_FormattedStateRequest_Model : IXComModel
    {
        public string MODULEID { get; set; }

        public Byte SFCD { get; set; }

        public S1F5_FormattedStateRequest_Model()
        {
            this.Stream = 1;
            this.Function = 5;
            this.FullName = "FormattedStateRequest";
            this.Name = "FormattedStateRequest";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULEID = string.Empty;
            this.SFCD = 0;
        }
    }
}
