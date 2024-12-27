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
    public class S6F11_EventReportSend_MaterialLoadEvent_Model : IXComModel
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

        public string PORTID { get; set; }

        public Byte PORT_STATE { get; set; }

        public Byte PORT_TYPE { get; set; }

        public string PORT_MODE { get; set; }

        public Byte SORT_TYPE { get; set; }

        public Byte RPTID_3 { get; set; }

        public List<S6F11_MaterialLoadEvent_LIST_1> MaterialLoadEvent_LIST_1 { get; set; }

        public S6F11_EventReportSend_MaterialLoadEvent_Model()
        {
            this.Stream = 6;
            this.Function = 11;
            this.FullName = "EventReportSend - MaterialLoadEvent";
            this.Name = "EventReportSend";
            this.SubName = "MaterialLoadEvent";
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
            this.PORTID = string.Empty;
            this.PORT_STATE = 0;
            this.PORT_TYPE = 0;
            this.PORT_MODE = string.Empty;
            this.SORT_TYPE = 0;
            this.RPTID_3 = 0;
            this.MaterialLoadEvent_LIST_1 = new List<S6F11_MaterialLoadEvent_LIST_1>();
        }
    }

    public class S6F11_MaterialLoadEvent_LIST_1
    {
        public string MATERIAL_KIND { get; set; }

        public string MATERIAL_ID { get; set; }

        public string SLOTNO { get; set; }

        public S6F11_MaterialLoadEvent_LIST_1()
        {
            this.MATERIAL_KIND = string.Empty;
            this.MATERIAL_ID = string.Empty;
            this.SLOTNO = string.Empty;
        }
    }
}
