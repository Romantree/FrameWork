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
    public class S16F3_PPC_DataSet_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public List<S16F3_LIST_1> LIST_1 { get; set; }

        public S16F3_PPC_DataSet_Model()
        {
            this.Stream = 16;
            this.Function = 3;
            this.FullName = "PPC_DataSet";
            this.Name = "PPC_DataSet";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.LIST_1 = new List<S16F3_LIST_1>();
        }
    }

    public class S16F3_LIST_1
    {
        public string H_GLASSID { get; set; }

        public string JOBID { get; set; }

        public List<S16F3_LIST_2> LIST_2 { get; set; }

        public S16F3_LIST_1()
        {
            this.H_GLASSID = string.Empty;
            this.JOBID = string.Empty;
            this.LIST_2 = new List<S16F3_LIST_2>();
        }
    }

    public class S16F3_LIST_2
    {
        public string P_MODULE_ID { get; set; }

        public string P_ORDER { get; set; }

        public S16F3_LIST_2()
        {
            this.P_MODULE_ID = string.Empty;
            this.P_ORDER = string.Empty;
        }
    }
}
