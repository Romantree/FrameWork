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
    public class S5F6_AlarmListAck_Model : IXComModel
    {
        public string MODULE_ID_1 { get; set; }

        public List<S5F6_LIST_1> LIST_1 { get; set; }

        public S5F6_AlarmListAck_Model()
        {
            this.Stream = 5;
            this.Function = 6;
            this.FullName = "AlarmListAck";
            this.Name = "AlarmListAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID_1 = string.Empty;
            this.LIST_1 = new List<S5F6_LIST_1>();
        }
    }

    public class S5F6_LIST_1
    {
        public string MODULE_ID_2 { get; set; }

        public Byte ALCD { get; set; }

        public Int64 ALID { get; set; }

        public string ALTX { get; set; }

        public S5F6_LIST_1()
        {
            this.MODULE_ID_2 = string.Empty;
            this.ALCD = 0;
            this.ALID = 0;
            this.ALTX = string.Empty;
        }
    }
}
