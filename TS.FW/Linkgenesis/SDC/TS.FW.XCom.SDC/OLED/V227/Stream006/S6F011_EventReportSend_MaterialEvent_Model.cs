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
    public class S6F11_EventReportSend_MaterialEvent_Model : IXComModel
    {
        public Byte DATA_ID { get; set; }

        public Int32 CEID { get; set; }

        public Byte RPTID { get; set; }

        public string MODULE_ID_1 { get; set; }

        public Byte MCMD { get; set; }

        public Byte MODULE_STATE { get; set; }

        public Byte PROC_STATE { get; set; }

        public Byte BYWHO { get; set; }

        public string OPERID { get; set; }

        public string MODULE_ID_2 { get; set; }

        public List<S6F11_MaterialEvent_LIST_1> MaterialEvent_LIST_1 { get; set; }

        public S6F11_EventReportSend_MaterialEvent_Model()
        {
            this.Stream = 6;
            this.Function = 11;
            this.FullName = "EventReportSend - MaterialEvent";
            this.Name = "EventReportSend";
            this.SubName = "MaterialEvent";
            this.IsUnDefined = false;

            this.DATA_ID = 0;
            this.CEID = 0;
            this.RPTID = 0;
            this.MODULE_ID_1 = string.Empty;
            this.MCMD = 0;
            this.MODULE_STATE = 0;
            this.PROC_STATE = 0;
            this.BYWHO = 0;
            this.OPERID = string.Empty;
            this.MODULE_ID_2 = string.Empty;
            this.MaterialEvent_LIST_1 = new List<S6F11_MaterialEvent_LIST_1>();
        }
    }

    public class S6F11_MaterialEvent_LIST_1
    {
        public string MATERIAL_ID { get; set; }

        public string PRODUCT_TYPE { get; set; }

        public string LIBRARYID { get; set; }

        public string STAGE_STATE { get; set; }

        public Int32 MATERIAL_STATE { get; set; }

        public string LOCATION { get; set; }

        public Int32[] MATERIAL_SIZE { get; set; }

        public string PRODUCTID { get; set; }

        public string STEPID { get; set; }

        public string PPID { get; set; }

        public S6F11_MaterialEvent_LIST_1()
        {
            this.MATERIAL_ID = string.Empty;
            this.PRODUCT_TYPE = string.Empty;
            this.LIBRARYID = string.Empty;
            this.STAGE_STATE = string.Empty;
            this.MATERIAL_STATE = 0;
            this.LOCATION = string.Empty;
            this.MATERIAL_SIZE = new Int32[2];
            this.PRODUCTID = string.Empty;
            this.STEPID = string.Empty;
            this.PPID = string.Empty;
        }
    }
}
