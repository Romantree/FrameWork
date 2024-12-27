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
    public class S1F4_SelectedEQStateData_Model : IXComModel
    {
        public string MODULE_ID_1 { get; set; }

        public List<S1F4_LIST_1> LIST_1 { get; set; }

        public S1F4_SelectedEQStateData_Model()
        {
            this.Stream = 1;
            this.Function = 4;
            this.FullName = "SelectedEQStateData";
            this.Name = "SelectedEQStateData";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID_1 = string.Empty;
            this.LIST_1 = new List<S1F4_LIST_1>();
        }
    }

    public class S1F4_LIST_1
    {
        public Int32 SVID { get; set; }

        public string SV { get; set; }

        public string SVNAME { get; set; }

        public string DATA_TYPE { get; set; }

        public string MODULE_ID_2 { get; set; }

        public S1F4_LIST_1()
        {
            this.SVID = 0;
            this.SV = string.Empty;
            this.SVNAME = string.Empty;
            this.DATA_TYPE = string.Empty;
            this.MODULE_ID_2 = string.Empty;
        }
    }
}
