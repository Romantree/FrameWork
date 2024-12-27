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
    public class S5F5_AlarmListRequest_Model : IXComModel
    {
        public string MODULEID { get; set; }

        public Int64 ALID { get; set; }

        public S5F5_AlarmListRequest_Model()
        {
            this.Stream = 5;
            this.Function = 5;
            this.FullName = "AlarmListRequest";
            this.Name = "AlarmListRequest";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULEID = string.Empty;
            this.ALID = 0;
        }
    }
}
