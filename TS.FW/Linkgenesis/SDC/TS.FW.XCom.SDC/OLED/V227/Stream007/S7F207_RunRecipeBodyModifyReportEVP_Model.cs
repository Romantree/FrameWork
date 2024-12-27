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
    public class S7F207_RunRecipeBodyModifyReportEVP_Model : IXComModel
    {
        public Byte MODE { get; set; }

        public string MODULE_ID { get; set; }

        public string PPID { get; set; }

        public Byte PPID_TYPE { get; set; }

        public List<S7F207_LIST_1> LIST_1 { get; set; }

        public S7F207_RunRecipeBodyModifyReportEVP_Model()
        {
            this.Stream = 7;
            this.Function = 207;
            this.FullName = "RunRecipeBodyModifyReportEVP";
            this.Name = "RunRecipeBodyModifyReportEVP";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODE = 0;
            this.MODULE_ID = string.Empty;
            this.PPID = string.Empty;
            this.PPID_TYPE = 0;
            this.LIST_1 = new List<S7F207_LIST_1>();
        }
    }

    public class S7F207_LIST_1
    {
        public string GROUP_CODE { get; set; }

        public Int32 PARTIAL_NO { get; set; }

        public Int32 TOTAL_NO { get; set; }

        public string SUB_MODULE_ID { get; set; }

        public List<S7F207_LIST_2> LIST_2 { get; set; }

        public S7F207_LIST_1()
        {
            this.GROUP_CODE = string.Empty;
            this.PARTIAL_NO = 0;
            this.TOTAL_NO = 0;
            this.SUB_MODULE_ID = string.Empty;
            this.LIST_2 = new List<S7F207_LIST_2>();
        }
    }

    public class S7F207_LIST_2
    {
        public string P_PARM_NAME { get; set; }

        public string P_PARM { get; set; }

        public S7F207_LIST_2()
        {
            this.P_PARM_NAME = string.Empty;
            this.P_PARM = string.Empty;
        }
    }
}
