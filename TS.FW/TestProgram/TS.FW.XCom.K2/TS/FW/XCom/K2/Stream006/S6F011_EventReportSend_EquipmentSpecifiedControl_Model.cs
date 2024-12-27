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
    public class S6F11_EventReportSend_EquipmentSpecifiedControl_Model : IXComModel
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

        public Byte RPTID7 { get; set; }

        public List<S6F11_Count> Count { get; set; }

        public S6F11_EventReportSend_EquipmentSpecifiedControl_Model()
        {
            this.Stream = 6;
            this.Function = 11;
            this.FullName = "EventReportSend_EquipmentSpecifiedControl";
            this.Name = "EventReportSend_EquipmentSpecifiedControl";
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
            this.RPTID7 = 0;
            this.Count = new List<S6F11_Count>();
        }
    }

    public class S6F11_Count
    {
        public string ITEM_NAME { get; set; }

        public string ITEM_VALUE { get; set; }

        public S6F11_Count()
        {
            this.ITEM_NAME = string.Empty;
            this.ITEM_VALUE = string.Empty;
        }
    }
}
