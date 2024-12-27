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
    public class S16F15_PPC_Event_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Byte MODE { get; set; }

        public Byte BYWHO { get; set; }

        public string H_GLASSID { get; set; }

        public string JOBID { get; set; }

        public string SET_TIME { get; set; }

        public List<S16F15_LIST_1> LIST_1 { get; set; }

        public S16F15_PPC_Event_Model()
        {
            this.Stream = 16;
            this.Function = 15;
            this.FullName = "PPC_Event";
            this.Name = "PPC_Event";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.MODE = 0;
            this.BYWHO = 0;
            this.H_GLASSID = string.Empty;
            this.JOBID = string.Empty;
            this.SET_TIME = string.Empty;
            this.LIST_1 = new List<S16F15_LIST_1>();
        }
    }

    public class S16F15_LIST_1
    {
        public string P_MODULE_ID { get; set; }

        public string P_ORDER { get; set; }

        public Byte P_STATUS { get; set; }

        public S16F15_LIST_1()
        {
            this.P_MODULE_ID = string.Empty;
            this.P_ORDER = string.Empty;
            this.P_STATUS = 0;
        }
    }
}
