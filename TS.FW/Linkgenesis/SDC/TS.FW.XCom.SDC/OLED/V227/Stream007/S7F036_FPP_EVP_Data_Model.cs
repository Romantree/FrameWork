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
    public class S7F36_FPP_EVP_Data_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public string PPID { get; set; }

        public Byte PPID_TYPE { get; set; }

        public List<S7F36_LIST_1> LIST_1 { get; set; }

        public S7F36_FPP_EVP_Data_Model()
        {
            this.Stream = 7;
            this.Function = 36;
            this.FullName = "FPP_EVP_Data";
            this.Name = "FPP_EVP_Data";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.PPID = string.Empty;
            this.PPID_TYPE = 0;
            this.LIST_1 = new List<S7F36_LIST_1>();
        }
    }

    public class S7F36_LIST_1
    {
        public Int32 CCODE { get; set; }

        public List<S7F36_LIST_2> LIST_2 { get; set; }

        public S7F36_LIST_1()
        {
            this.CCODE = 0;
            this.LIST_2 = new List<S7F36_LIST_2>();
        }
    }

    public class S7F36_LIST_2
    {
        public string P_PARM_NAME { get; set; }

        public string P_PARM { get; set; }

        public S7F36_LIST_2()
        {
            this.P_PARM_NAME = string.Empty;
            this.P_PARM = string.Empty;
        }
    }
}
