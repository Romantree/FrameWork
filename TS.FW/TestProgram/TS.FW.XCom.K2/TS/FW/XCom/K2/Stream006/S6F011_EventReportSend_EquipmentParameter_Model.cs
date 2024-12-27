using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.XCom;
using Newtonsoft.Json;
using XCOMLib;

namespace TS.FW.XCom.K2
{
    public class S6F11_EventReportSend_EquipmentParameter_Model : IXComModel
    {
        public Byte DATAID { get; set; }

        public Int32 CEID { get; set; }

        public Byte RPTID1 { get; set; }

        public string MODULEID { get; set; }

        public Byte MCMD { get; set; }

        public Byte MODULE_STATE { get; set; }

        public Byte PROC_STATE { get; set; }

        public Byte BYWHO { get; set; }

        public string OPERID { get; set; }

        public Byte RPTID5 { get; set; }

        public Byte EOID { get; set; }

        public Byte EOMD { get; set; }

        public Byte EOV { get; set; }

        public Byte RPTID6 { get; set; }

        public List<S6F11_ECIDs> ECIDs { get; set; }

        public S6F11_EventReportSend_EquipmentParameter_Model()
        {
            this.Stream = 6;
            this.Function = 11;
            this.FullName = "EventReportSend_EquipmentParameter";
            this.Name = "EventReportSend_EquipmentParameter";
            this.SubName = "";
            this.IsUnDefined = false;

            this.DATAID = 0;
            this.CEID = 0;
            this.RPTID1 = 0;
            this.MODULEID = string.Empty;
            this.MCMD = 0;
            this.MODULE_STATE = 0;
            this.PROC_STATE = 0;
            this.BYWHO = 0;
            this.OPERID = string.Empty;
            this.RPTID5 = 0;
            this.EOID = 0;
            this.EOMD = 0;
            this.EOV = 0;
            this.RPTID6 = 0;
            this.ECIDs = new List<S6F11_ECIDs>();
        }
    }

    public class S6F11_ECIDs
    {
        public Int32 ECID { get; set; }

        public string ECNAME { get; set; }

        public string ECDEF { get; set; }

        public string ECSLL { get; set; }

        public string ECSUL { get; set; }

        public string ECWLL { get; set; }

        public string ECWUL { get; set; }

        public S6F11_ECIDs()
        {
            this.ECID = 0;
            this.ECNAME = string.Empty;
            this.ECDEF = string.Empty;
            this.ECSLL = string.Empty;
            this.ECSUL = string.Empty;
            this.ECWLL = string.Empty;
            this.ECWUL = string.Empty;
        }
    }
}
