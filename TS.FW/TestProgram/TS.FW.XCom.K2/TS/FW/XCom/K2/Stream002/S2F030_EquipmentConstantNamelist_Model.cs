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
    public class S2F30_EquipmentConstantNamelist_Model : IXComModel
    {
        public string MODULEID { get; set; }

        public List<S2F30_ECIDs> ECIDs { get; set; }

        public S2F30_EquipmentConstantNamelist_Model()
        {
            this.Stream = 2;
            this.Function = 30;
            this.FullName = "EquipmentConstantNamelist";
            this.Name = "EquipmentConstantNamelist";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULEID = string.Empty;
            this.ECIDs = new List<S2F30_ECIDs>();
        }
    }

    public class S2F30_ECIDs
    {
        public Int32 ECID { get; set; }

        public string ECNAME { get; set; }

        public string ECDEF { get; set; }

        public string ECSLL { get; set; }

        public string ECSUL { get; set; }

        public string ECWLL { get; set; }

        public string ECWUL { get; set; }

        public S2F30_ECIDs()
        {
            this.ECID = 0;
            this.ECNAME = string.Empty;
            this.ECDEF = string.Empty;
            this.ECSLL = string.Empty;
            this.ECSUL = string.Empty;
            this.ECWLL = string.Empty;
            this.ECWUL = string.Empty;
        }
    }
}
