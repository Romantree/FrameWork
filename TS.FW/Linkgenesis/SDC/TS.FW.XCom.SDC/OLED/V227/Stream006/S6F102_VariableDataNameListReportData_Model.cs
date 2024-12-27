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
    public class S6F102_VariableDataNameListReportData_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Int32 RPTUNIT { get; set; }

        public List<S6F102_LIST_1> LIST_1 { get; set; }

        public S6F102_VariableDataNameListReportData_Model()
        {
            this.Stream = 6;
            this.Function = 102;
            this.FullName = "VariableDataNameListReportData";
            this.Name = "VariableDataNameListReportData";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.RPTUNIT = 0;
            this.LIST_1 = new List<S6F102_LIST_1>();
        }
    }

    public class S6F102_LIST_1
    {
        public string DATA_ITEM { get; set; }

        public S6F102_LIST_1()
        {
            this.DATA_ITEM = string.Empty;
        }
    }
}
