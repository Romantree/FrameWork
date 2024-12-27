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
    public class S2F42_HostCommandAck_ProcessCmd_Model : IXComModel
    {
        public Int32 RCMD { get; set; }

        public Byte HCACK { get; set; }

        public string JOBID_NAME { get; set; }

        public string JOBID { get; set; }

        public Byte CPACK_1 { get; set; }

        public string IPID_NAME { get; set; }

        public string IPID { get; set; }

        public Byte CPACK_2 { get; set; }

        public string OCID_NAME_1 { get; set; }

        public string OCID_1 { get; set; }

        public Byte CPACK_3 { get; set; }

        public string OPID_NAME { get; set; }

        public string OPID { get; set; }

        public Byte CPACK_4 { get; set; }

        public string OCID_NAME_2 { get; set; }

        public string OCID_2 { get; set; }

        public Byte CPACK_5 { get; set; }

        public string SLOTINFO_NAME { get; set; }

        public string SLOTINFO { get; set; }

        public Byte CPACK_6 { get; set; }

        public string ORDER_NAME { get; set; }

        public List<S2F42_ProcessCmd_LIST_1> ProcessCmd_LIST_1 { get; set; }

        public Byte CPACK_7 { get; set; }

        public S2F42_HostCommandAck_ProcessCmd_Model()
        {
            this.Stream = 2;
            this.Function = 42;
            this.FullName = "HostCommandAck - ProcessCmd";
            this.Name = "HostCommandAck";
            this.SubName = "ProcessCmd";
            this.IsUnDefined = false;

            this.RCMD = 0;
            this.HCACK = 0;
            this.JOBID_NAME = string.Empty;
            this.JOBID = string.Empty;
            this.CPACK_1 = 0;
            this.IPID_NAME = string.Empty;
            this.IPID = string.Empty;
            this.CPACK_2 = 0;
            this.OCID_NAME_1 = string.Empty;
            this.OCID_1 = string.Empty;
            this.CPACK_3 = 0;
            this.OPID_NAME = string.Empty;
            this.OPID = string.Empty;
            this.CPACK_4 = 0;
            this.OCID_NAME_2 = string.Empty;
            this.OCID_2 = string.Empty;
            this.CPACK_5 = 0;
            this.SLOTINFO_NAME = string.Empty;
            this.SLOTINFO = string.Empty;
            this.CPACK_6 = 0;
            this.ORDER_NAME = string.Empty;
            this.ProcessCmd_LIST_1 = new List<S2F42_ProcessCmd_LIST_1>();
            this.CPACK_7 = 0;
        }
    }

    public class S2F42_ProcessCmd_LIST_1
    {
        public Byte SLOTNO { get; set; }

        public S2F42_ProcessCmd_LIST_1()
        {
            this.SLOTNO = 0;
        }
    }
}
