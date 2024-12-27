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
    public class S1F2_OnLineData_Host_Model : IXComModel
    {
        public S1F2_OnLineData_Host_Model()
        {
            this.Stream = 1;
            this.Function = 2;
            this.FullName = "OnLineData - Host";
            this.Name = "OnLineData";
            this.SubName = "Host";
            this.IsUnDefined = false;

        }
    }
}
