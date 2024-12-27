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
    public class S2F41_HostCommandSend_SortCmd_Model : IXComModel
    {
        public Int32 RCMD { get; set; }

        public string JOBID { get; set; }

        public List<S2F41_SortCmd_LIST_1> SortCmd_LIST_1 { get; set; }

        public S2F41_HostCommandSend_SortCmd_Model()
        {
            this.Stream = 2;
            this.Function = 41;
            this.FullName = "HostCommandSend - SortCmd";
            this.Name = "HostCommandSend";
            this.SubName = "SortCmd";
            this.IsUnDefined = false;

            this.RCMD = 0;
            this.JOBID = string.Empty;
            this.SortCmd_LIST_1 = new List<S2F41_SortCmd_LIST_1>();
        }
    }

    public class S2F41_SortCmd_LIST_1
    {
        public string SPID_NAME { get; set; }

        public string SPID { get; set; }

        public string SCID_NAME { get; set; }

        public string SCID { get; set; }

        public string TPID_NAME { get; set; }

        public string TPID { get; set; }

        public string TCID_NAME { get; set; }

        public string TCID { get; set; }

        public string VCRFLAG_NAME { get; set; }

        public Byte VCRFLAG { get; set; }

        public List<S2F41_SortCmd_LIST_2> SortCmd_LIST_2 { get; set; }

        public S2F41_SortCmd_LIST_1()
        {
            this.SPID_NAME = string.Empty;
            this.SPID = string.Empty;
            this.SCID_NAME = string.Empty;
            this.SCID = string.Empty;
            this.TPID_NAME = string.Empty;
            this.TPID = string.Empty;
            this.TCID_NAME = string.Empty;
            this.TCID = string.Empty;
            this.VCRFLAG_NAME = string.Empty;
            this.VCRFLAG = 0;
            this.SortCmd_LIST_2 = new List<S2F41_SortCmd_LIST_2>();
        }
    }

    public class S2F41_SortCmd_LIST_2
    {
        public string H_GLASSID { get; set; }

        public string SSLOTID { get; set; }

        public string TSLOTID { get; set; }

        public S2F41_SortCmd_LIST_2()
        {
            this.H_GLASSID = string.Empty;
            this.SSLOTID = string.Empty;
            this.TSLOTID = string.Empty;
        }
    }
}
