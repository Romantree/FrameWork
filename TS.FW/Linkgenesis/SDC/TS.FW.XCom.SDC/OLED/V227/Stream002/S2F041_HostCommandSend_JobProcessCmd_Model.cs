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
    public class S2F41_HostCommandSend_JobProcessCmd_Model : IXComModel
    {
        public Int32 JobProcessCmd_UINT2_1 { get; set; }

        public string JOBID_NAME { get; set; }

        public string JOBID { get; set; }

        public string IPID_NAME { get; set; }

        public string IPID { get; set; }

        public string ICID_NAME { get; set; }

        public string ICID { get; set; }

        public string OPID_NAME { get; set; }

        public string OPID { get; set; }

        public string OCID_NAME { get; set; }

        public string OCID { get; set; }

        public string SLOTINFO_NAME { get; set; }

        public string SLOTINFO { get; set; }

        public string ORDER_NAME { get; set; }

        public List<S2F41_JobProcessCmd_LIST_1> JobProcessCmd_LIST_1 { get; set; }

        public S2F41_HostCommandSend_JobProcessCmd_Model()
        {
            this.Stream = 2;
            this.Function = 41;
            this.FullName = "HostCommandSend - JobProcessCmd";
            this.Name = "HostCommandSend";
            this.SubName = "JobProcessCmd";
            this.IsUnDefined = false;

            this.JobProcessCmd_UINT2_1 = 0;
            this.JOBID_NAME = string.Empty;
            this.JOBID = string.Empty;
            this.IPID_NAME = string.Empty;
            this.IPID = string.Empty;
            this.ICID_NAME = string.Empty;
            this.ICID = string.Empty;
            this.OPID_NAME = string.Empty;
            this.OPID = string.Empty;
            this.OCID_NAME = string.Empty;
            this.OCID = string.Empty;
            this.SLOTINFO_NAME = string.Empty;
            this.SLOTINFO = string.Empty;
            this.ORDER_NAME = string.Empty;
            this.JobProcessCmd_LIST_1 = new List<S2F41_JobProcessCmd_LIST_1>();
        }
    }

    public class S2F41_JobProcessCmd_LIST_1
    {
        public Byte SLOTNO { get; set; }

        public S2F41_JobProcessCmd_LIST_1()
        {
            this.SLOTNO = 0;
        }
    }
}
