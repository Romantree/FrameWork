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
    public class S1F17_RequestOnlineModeChange_Model : IXComModel
    {
        public string MODULEID { get; set; }

        public Byte MCMD { get; set; }

        public S1F17_RequestOnlineModeChange_Model()
        {
            this.Stream = 1;
            this.Function = 17;
            this.FullName = "RequestOnlineModeChange";
            this.Name = "RequestOnlineModeChange";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULEID = string.Empty;
            this.MCMD = 0;
        }
    }
}
