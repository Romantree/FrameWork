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
    public class S6F11_EventReportSend_ReadingRate_Model : IXComModel
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

        public string READING_RATE { get; set; }

        public string TOTAL_COUNT { get; set; }

        public string SUCCESS_COUNT { get; set; }

        public List<S6F11_GLASSs> GLASSs { get; set; }

        public S6F11_EventReportSend_ReadingRate_Model()
        {
            this.Stream = 6;
            this.Function = 11;
            this.FullName = "EventReportSend_ReadingRate";
            this.Name = "EventReportSend_ReadingRate";
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
            this.READING_RATE = string.Empty;
            this.TOTAL_COUNT = string.Empty;
            this.SUCCESS_COUNT = string.Empty;
            this.GLASSs = new List<S6F11_GLASSs>();
        }
    }

    public class S6F11_GLASSs
    {
        public string READING_TIME { get; set; }

        public string IDENTIFICATION { get; set; }

        public string TRY_COUNT { get; set; }

        public S6F11_GLASSs()
        {
            this.READING_TIME = string.Empty;
            this.IDENTIFICATION = string.Empty;
            this.TRY_COUNT = string.Empty;
        }
    }
}
