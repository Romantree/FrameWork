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
    public class S6F101_VariableDataNameListReportReq_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Int32 RPTUNIT { get; set; }

        public List<S6F101_LIST_1> LIST_1 { get; set; }

        public S6F101_VariableDataNameListReportReq_Model()
        {
            this.Stream = 6;
            this.Function = 101;
            this.FullName = "VariableDataNameListReportReq";
            this.Name = "VariableDataNameListReportReq";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.RPTUNIT = 0;
            this.LIST_1 = new List<S6F101_LIST_1>();
        }
    }

    public class S6F101_LIST_1
    {
        public string ITEM_NAME { get; set; }

        public S6F101_LIST_1()
        {
            this.ITEM_NAME = string.Empty;
        }
    }
}
