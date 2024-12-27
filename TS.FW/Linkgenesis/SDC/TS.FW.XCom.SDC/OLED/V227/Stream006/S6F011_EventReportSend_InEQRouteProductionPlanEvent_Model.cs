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
    public class S6F11_EventReportSend_InEQRouteProductionPlanEvent_Model : IXComModel
    {
        public Byte DATA_ID { get; set; }

        public Int32 CEID { get; set; }

        public Byte RPTID { get; set; }

        public string MODULE_ID { get; set; }

        public Byte MCMD { get; set; }

        public Byte MODULE_STATE { get; set; }

        public Byte PROC_STATE { get; set; }

        public Byte BYWHO { get; set; }

        public string OPERID { get; set; }

        public string OLDPATH { get; set; }

        public string PATHNAME_OLD { get; set; }

        public List<S6F11_InEQRouteProductionPlanEvent_LIST_1> InEQRouteProductionPlanEvent_LIST_1 { get; set; }

        public string NEWPATH { get; set; }

        public string PATHNAME_NEW { get; set; }

        public List<S6F11_InEQRouteProductionPlanEvent_LIST_2> InEQRouteProductionPlanEvent_LIST_2 { get; set; }

        public S6F11_EventReportSend_InEQRouteProductionPlanEvent_Model()
        {
            this.Stream = 6;
            this.Function = 11;
            this.FullName = "EventReportSend - InEQRouteProductionPlanEvent";
            this.Name = "EventReportSend";
            this.SubName = "InEQRouteProductionPlanEvent";
            this.IsUnDefined = false;

            this.DATA_ID = 0;
            this.CEID = 0;
            this.RPTID = 0;
            this.MODULE_ID = string.Empty;
            this.MCMD = 0;
            this.MODULE_STATE = 0;
            this.PROC_STATE = 0;
            this.BYWHO = 0;
            this.OPERID = string.Empty;
            this.OLDPATH = string.Empty;
            this.PATHNAME_OLD = string.Empty;
            this.InEQRouteProductionPlanEvent_LIST_1 = new List<S6F11_InEQRouteProductionPlanEvent_LIST_1>();
            this.NEWPATH = string.Empty;
            this.PATHNAME_NEW = string.Empty;
            this.InEQRouteProductionPlanEvent_LIST_2 = new List<S6F11_InEQRouteProductionPlanEvent_LIST_2>();
        }
    }

    public class S6F11_InEQRouteProductionPlanEvent_LIST_1
    {
        public string OLD_PATH_MODULE_ID { get; set; }

        public string OLD_PATH_ORDER { get; set; }

        public S6F11_InEQRouteProductionPlanEvent_LIST_1()
        {
            this.OLD_PATH_MODULE_ID = string.Empty;
            this.OLD_PATH_ORDER = string.Empty;
        }
    }

    public class S6F11_InEQRouteProductionPlanEvent_LIST_2
    {
        public string NEW_PATH_MODULE_ID { get; set; }

        public string NEW_PATH_ORDER { get; set; }

        public S6F11_InEQRouteProductionPlanEvent_LIST_2()
        {
            this.NEW_PATH_MODULE_ID = string.Empty;
            this.NEW_PATH_ORDER = string.Empty;
        }
    }
}
