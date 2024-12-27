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
    public class S1F4_SelectedEquipmentStateData_Model : IXComModel
    {
        public string MODULEID { get; set; }

        public List<S1F4_SV> SV { get; set; }

        public S1F4_SelectedEquipmentStateData_Model()
        {
            this.Stream = 1;
            this.Function = 4;
            this.FullName = "SelectedEquipmentStateData";
            this.Name = "SelectedEquipmentStateData";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULEID = string.Empty;
            this.SV = new List<S1F4_SV>();
        }
    }

    public class S1F4_SV
    {
        public Int32 SVID { get; set; }

        public string SV { get; set; }

        public string SVNAME { get; set; }

        public string DATATYPE { get; set; }

        public string MODULEID { get; set; }

        public S1F4_SV()
        {
            this.SVID = 0;
            this.SV = string.Empty;
            this.SVNAME = string.Empty;
            this.DATATYPE = string.Empty;
            this.MODULEID = string.Empty;
        }
    }
}
