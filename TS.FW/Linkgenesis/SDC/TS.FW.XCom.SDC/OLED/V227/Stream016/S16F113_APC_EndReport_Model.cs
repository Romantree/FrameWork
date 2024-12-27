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
    public class S16F113_APC_EndReport_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public List<S16F113_LIST_1> LIST_1 { get; set; }

        public S16F113_APC_EndReport_Model()
        {
            this.Stream = 16;
            this.Function = 113;
            this.FullName = "APC_EndReport";
            this.Name = "APC_EndReport";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.LIST_1 = new List<S16F113_LIST_1>();
        }
    }

    public class S16F113_LIST_1
    {
        public string H_GLASSID { get; set; }

        public string JOBID { get; set; }

        public string RECIPE { get; set; }

        public string SET_TIME { get; set; }

        public List<S16F113_LIST_2> LIST_2 { get; set; }

        public S16F113_LIST_1()
        {
            this.H_GLASSID = string.Empty;
            this.JOBID = string.Empty;
            this.RECIPE = string.Empty;
            this.SET_TIME = string.Empty;
            this.LIST_2 = new List<S16F113_LIST_2>();
        }
    }

    public class S16F113_LIST_2
    {
        public string P_PARM_NAME { get; set; }

        public string P_PARM_VALUE { get; set; }

        public S16F113_LIST_2()
        {
            this.P_PARM_NAME = string.Empty;
            this.P_PARM_VALUE = string.Empty;
        }
    }
}
