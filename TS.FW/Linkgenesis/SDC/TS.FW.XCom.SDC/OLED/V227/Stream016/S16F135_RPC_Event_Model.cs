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
    public class S16F135_RPC_Event_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Byte MODE { get; set; }

        public List<S16F135_LIST_1> LIST_1 { get; set; }

        public S16F135_RPC_Event_Model()
        {
            this.Stream = 16;
            this.Function = 135;
            this.FullName = "RPC_Event";
            this.Name = "RPC_Event";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.MODE = 0;
            this.LIST_1 = new List<S16F135_LIST_1>();
        }
    }

    public class S16F135_LIST_1
    {
        public string H_GLASSID { get; set; }

        public string JOBID { get; set; }

        public string RPC_PPID { get; set; }

        public string SET_TIME { get; set; }

        public Byte BYWHO { get; set; }

        public S16F135_LIST_1()
        {
            this.H_GLASSID = string.Empty;
            this.JOBID = string.Empty;
            this.RPC_PPID = string.Empty;
            this.SET_TIME = string.Empty;
            this.BYWHO = 0;
        }
    }
}
