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
    public class S6F11_EventReportSend_EQParamEvent_Model : IXComModel
    {
        public Byte DATA_ID { get; set; }

        public Int32 CEID { get; set; }

        public Byte RPTID_1 { get; set; }

        public string MODULE_ID { get; set; }

        public Byte MCMD { get; set; }

        public Byte MODULE_STATE { get; set; }

        public Byte PROC_STATE { get; set; }

        public Byte BYWHO { get; set; }

        public string OPERID { get; set; }

        public Byte RPTID_2 { get; set; }

        public List<S6F11_EQParamEvent_LIST_1> EQParamEvent_LIST_1 { get; set; }

        public Byte RPTID_3 { get; set; }

        public List<S6F11_EQParamEvent_LIST_3> EQParamEvent_LIST_3 { get; set; }

        public S6F11_EventReportSend_EQParamEvent_Model()
        {
            this.Stream = 6;
            this.Function = 11;
            this.FullName = "EventReportSend - EQParamEvent";
            this.Name = "EventReportSend";
            this.SubName = "EQParamEvent";
            this.IsUnDefined = false;

            this.DATA_ID = 0;
            this.CEID = 0;
            this.RPTID_1 = 0;
            this.MODULE_ID = string.Empty;
            this.MCMD = 0;
            this.MODULE_STATE = 0;
            this.PROC_STATE = 0;
            this.BYWHO = 0;
            this.OPERID = string.Empty;
            this.RPTID_2 = 0;
            this.EQParamEvent_LIST_1 = new List<S6F11_EQParamEvent_LIST_1>();
            this.RPTID_3 = 0;
            this.EQParamEvent_LIST_3 = new List<S6F11_EQParamEvent_LIST_3>();
        }
    }

    public class S6F11_EQParamEvent_LIST_1
    {
        public Byte EOID { get; set; }

        public List<S6F11_EQParamEvent_LIST_2> EQParamEvent_LIST_2 { get; set; }

        public S6F11_EQParamEvent_LIST_1()
        {
            this.EOID = 0;
            this.EQParamEvent_LIST_2 = new List<S6F11_EQParamEvent_LIST_2>();
        }
    }

    public class S6F11_EQParamEvent_LIST_2
    {
        public Byte EOMD { get; set; }

        public Byte EOV { get; set; }

        public S6F11_EQParamEvent_LIST_2()
        {
            this.EOMD = 0;
            this.EOV = 0;
        }
    }

    public class S6F11_EQParamEvent_LIST_3
    {
        public Int32 ECID { get; set; }

        public string ECNAME { get; set; }

        public string ECDEF { get; set; }

        public string ECSLL { get; set; }

        public string ECSUL { get; set; }

        public string ECWLL { get; set; }

        public string ECWUL { get; set; }

        public S6F11_EQParamEvent_LIST_3()
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
