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
    public class S6F11_EventReportSend_ModuleLabelPrintEvent_Model : IXComModel
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

        public List<S6F11_ModuleLabelPrintEvent_LIST_1> ModuleLabelPrintEvent_LIST_1 { get; set; }

        public S6F11_EventReportSend_ModuleLabelPrintEvent_Model()
        {
            this.Stream = 6;
            this.Function = 11;
            this.FullName = "EventReportSend - ModuleLabelPrintEvent";
            this.Name = "EventReportSend";
            this.SubName = "ModuleLabelPrintEvent";
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
            this.ModuleLabelPrintEvent_LIST_1 = new List<S6F11_ModuleLabelPrintEvent_LIST_1>();
        }
    }

    public class S6F11_ModuleLabelPrintEvent_LIST_1
    {
        public Byte L_KIND { get; set; }

        public string PRODUCTID { get; set; }

        public string BATCHID { get; set; }

        public string SERIAL_NO { get; set; }

        public List<S6F11_ModuleLabelPrintEvent_LIST_2> ModuleLabelPrintEvent_LIST_2 { get; set; }

        public S6F11_ModuleLabelPrintEvent_LIST_1()
        {
            this.L_KIND = 0;
            this.PRODUCTID = string.Empty;
            this.BATCHID = string.Empty;
            this.SERIAL_NO = string.Empty;
            this.ModuleLabelPrintEvent_LIST_2 = new List<S6F11_ModuleLabelPrintEvent_LIST_2>();
        }
    }

    public class S6F11_ModuleLabelPrintEvent_LIST_2
    {
        public string H_GLASSID { get; set; }

        public S6F11_ModuleLabelPrintEvent_LIST_2()
        {
            this.H_GLASSID = string.Empty;
        }
    }
}
