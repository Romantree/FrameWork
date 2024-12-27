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
    public class S16F104_APC_DataSetAck_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Byte TCACK { get; set; }

        public List<S16F104_LIST_1> LIST_1 { get; set; }

        public S16F104_APC_DataSetAck_Model()
        {
            this.Stream = 16;
            this.Function = 104;
            this.FullName = "APC_DataSetAck";
            this.Name = "APC_DataSetAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.TCACK = 0;
            this.LIST_1 = new List<S16F104_LIST_1>();
        }
    }

    public class S16F104_LIST_1
    {
        public string H_GLASSID { get; set; }

        public string JOBID { get; set; }

        public string RECIPE { get; set; }

        public string SET_TIME { get; set; }

        public List<S16F104_LIST_2> LIST_2 { get; set; }

        public S16F104_LIST_1()
        {
            this.H_GLASSID = string.Empty;
            this.JOBID = string.Empty;
            this.RECIPE = string.Empty;
            this.SET_TIME = string.Empty;
            this.LIST_2 = new List<S16F104_LIST_2>();
        }
    }

    public class S16F104_LIST_2
    {
        public string P_PARM_NAME { get; set; }

        public Byte PACK_1 { get; set; }

        public string P_PARM_VALUE { get; set; }

        public Byte PACK_2 { get; set; }

        public S16F104_LIST_2()
        {
            this.P_PARM_NAME = string.Empty;
            this.PACK_1 = 0;
            this.P_PARM_VALUE = string.Empty;
            this.PACK_2 = 0;
        }
    }
}
