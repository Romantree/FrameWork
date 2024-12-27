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
    public class S1F3_SelectedEquipmentStateRequest_Model : IXComModel
    {
        public string MODULEID { get; set; }

        public List<S1F3_SVs> SVs { get; set; }

        public S1F3_SelectedEquipmentStateRequest_Model()
        {
            this.Stream = 1;
            this.Function = 3;
            this.FullName = "SelectedEquipmentStateRequest";
            this.Name = "SelectedEquipmentStateRequest";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULEID = string.Empty;
            this.SVs = new List<S1F3_SVs>();
        }
    }

    public class S1F3_SVs
    {
        public Int32 SVID { get; set; }

        public S1F3_SVs()
        {
            this.SVID = 0;
        }
    }
}
