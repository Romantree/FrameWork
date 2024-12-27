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
    public class S16F122_RPC_ProcessChangeDataListAck_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Byte ACKC16 { get; set; }

        public List<S16F122_LIST_1> LIST_1 { get; set; }

        public S16F122_RPC_ProcessChangeDataListAck_Model()
        {
            this.Stream = 16;
            this.Function = 122;
            this.FullName = "RPC_ProcessChangeDataListAck";
            this.Name = "RPC_ProcessChangeDataListAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.ACKC16 = 0;
            this.LIST_1 = new List<S16F122_LIST_1>();
        }
    }

    public class S16F122_LIST_1
    {
        public string H_GLASSID { get; set; }

        public string JOBID { get; set; }

        public string RPC_PPID { get; set; }

        public string SET_TIME { get; set; }

        public Byte RPC_STATE { get; set; }

        public S16F122_LIST_1()
        {
            this.H_GLASSID = string.Empty;
            this.JOBID = string.Empty;
            this.RPC_PPID = string.Empty;
            this.SET_TIME = string.Empty;
            this.RPC_STATE = 0;
        }
    }
}
