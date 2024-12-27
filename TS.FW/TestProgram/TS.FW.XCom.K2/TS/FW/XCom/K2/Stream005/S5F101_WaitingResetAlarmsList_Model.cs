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
    public class S5F101_WaitingResetAlarmsList_Model : IXComModel
    {
        public string MODULEID { get; set; }

        public S5F101_WaitingResetAlarmsList_Model()
        {
            this.Stream = 5;
            this.Function = 101;
            this.FullName = "WaitingResetAlarmsList";
            this.Name = "WaitingResetAlarmsList";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULEID = string.Empty;
        }
    }
}
