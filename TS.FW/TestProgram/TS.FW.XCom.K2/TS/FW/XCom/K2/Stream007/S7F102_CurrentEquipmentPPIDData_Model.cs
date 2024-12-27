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
    public class S7F102_CurrentEquipmentPPIDData_Model : IXComModel
    {
        public string MODULEID { get; set; }

        public Byte PPID_TYPE { get; set; }

        public List<S7F102_PPIDs> PPIDs { get; set; }

        public S7F102_CurrentEquipmentPPIDData_Model()
        {
            this.Stream = 7;
            this.Function = 102;
            this.FullName = "CurrentEquipmentPPIDData";
            this.Name = "CurrentEquipmentPPIDData";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULEID = string.Empty;
            this.PPID_TYPE = 0;
            this.PPIDs = new List<S7F102_PPIDs>();
        }
    }

    public class S7F102_PPIDs
    {
        public string PPID { get; set; }

        public S7F102_PPIDs()
        {
            this.PPID = string.Empty;
        }
    }
}
