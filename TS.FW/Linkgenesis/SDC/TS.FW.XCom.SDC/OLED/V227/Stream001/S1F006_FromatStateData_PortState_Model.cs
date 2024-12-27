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
    public class S1F6_FromatStateData_PortState_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Byte SFCD { get; set; }

        public List<S1F6_PortState_LIST_1> PortState_LIST_1 { get; set; }

        public S1F6_FromatStateData_PortState_Model()
        {
            this.Stream = 1;
            this.Function = 6;
            this.FullName = "FromatStateData - PortState";
            this.Name = "FromatStateData";
            this.SubName = "PortState";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.SFCD = 0;
            this.PortState_LIST_1 = new List<S1F6_PortState_LIST_1>();
        }
    }

    public class S1F6_PortState_LIST_1
    {
        public string PORTID { get; set; }

        public Byte PORT_STATE { get; set; }

        public Byte PORT_TYPE { get; set; }

        public string PORT_MODE { get; set; }

        public Byte SORT_TYPE { get; set; }

        public string CSTID { get; set; }

        public Byte CST_TYPE { get; set; }

        public string MAP_STIF { get; set; }

        public string CUR_STIF { get; set; }

        public Byte BATCH_ORDER { get; set; }

        public S1F6_PortState_LIST_1()
        {
            this.PORTID = string.Empty;
            this.PORT_STATE = 0;
            this.PORT_TYPE = 0;
            this.PORT_MODE = string.Empty;
            this.SORT_TYPE = 0;
            this.CSTID = string.Empty;
            this.CST_TYPE = 0;
            this.MAP_STIF = string.Empty;
            this.CUR_STIF = string.Empty;
            this.BATCH_ORDER = 0;
        }
    }
}
