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
    public class S2F29_EquipmentConstantNamelistRequest_Model : IXComModel
    {
        public string MODULEID { get; set; }

        public List<S2F29_ECIDs> ECIDs { get; set; }

        public S2F29_EquipmentConstantNamelistRequest_Model()
        {
            this.Stream = 2;
            this.Function = 29;
            this.FullName = "EquipmentConstantNamelistRequest";
            this.Name = "EquipmentConstantNamelistRequest";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULEID = string.Empty;
            this.ECIDs = new List<S2F29_ECIDs>();
        }
    }

    public class S2F29_ECIDs
    {
        public Int32 ECID { get; set; }

        public S2F29_ECIDs()
        {
            this.ECID = 0;
        }
    }
}
