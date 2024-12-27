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
    public class S2F31_DateAndTimeSetRequest_Model : IXComModel
    {
        public string TIME { get; set; }

        public S2F31_DateAndTimeSetRequest_Model()
        {
            this.Stream = 2;
            this.Function = 31;
            this.FullName = "DateAndTimeSetRequest";
            this.Name = "DateAndTimeSetRequest";
            this.SubName = "";
            this.IsUnDefined = false;

            this.TIME = string.Empty;
        }
    }
}
