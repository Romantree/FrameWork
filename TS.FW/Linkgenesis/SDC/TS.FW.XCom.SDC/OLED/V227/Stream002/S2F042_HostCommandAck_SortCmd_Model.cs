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
    public class S2F42_HostCommandAck_SortCmd_Model : IXComModel
    {
        public Int32 RCMD { get; set; }

        public Byte HCACK { get; set; }

        public string JOBID { get; set; }

        public List<S2F42_SortCmd_LIST_1> SortCmd_LIST_1 { get; set; }

        public S2F42_HostCommandAck_SortCmd_Model()
        {
            this.Stream = 2;
            this.Function = 42;
            this.FullName = "HostCommandAck - SortCmd";
            this.Name = "HostCommandAck";
            this.SubName = "SortCmd";
            this.IsUnDefined = false;

            this.RCMD = 0;
            this.HCACK = 0;
            this.JOBID = string.Empty;
            this.SortCmd_LIST_1 = new List<S2F42_SortCmd_LIST_1>();
        }
    }

    public class S2F42_SortCmd_LIST_1
    {
        public string SPID_NAME { get; set; }

        public string SPID { get; set; }

        public Byte CPACK_1 { get; set; }

        public string SCID_NAME { get; set; }

        public string SCID { get; set; }

        public Byte CPACK_2 { get; set; }

        public string TPID_NAME { get; set; }

        public string TPID { get; set; }

        public Byte CPACK_3 { get; set; }

        public string TCID_NAME { get; set; }

        public string TCID { get; set; }

        public Byte CPACK_4 { get; set; }

        public string VCRFLAG_NAME { get; set; }

        public Byte VCRFLAG { get; set; }

        public Byte CPACK_5 { get; set; }

        public string H_GLASSID { get; set; }

        public string SSLOTID { get; set; }

        public string TSLOTID { get; set; }

        public Byte CPACK_6 { get; set; }

        public S2F42_SortCmd_LIST_1()
        {
            this.SPID_NAME = string.Empty;
            this.SPID = string.Empty;
            this.CPACK_1 = 0;
            this.SCID_NAME = string.Empty;
            this.SCID = string.Empty;
            this.CPACK_2 = 0;
            this.TPID_NAME = string.Empty;
            this.TPID = string.Empty;
            this.CPACK_3 = 0;
            this.TCID_NAME = string.Empty;
            this.TCID = string.Empty;
            this.CPACK_4 = 0;
            this.VCRFLAG_NAME = string.Empty;
            this.VCRFLAG = 0;
            this.CPACK_5 = 0;
            this.H_GLASSID = string.Empty;
            this.SSLOTID = string.Empty;
            this.TSLOTID = string.Empty;
            this.CPACK_6 = 0;
        }
    }
}
