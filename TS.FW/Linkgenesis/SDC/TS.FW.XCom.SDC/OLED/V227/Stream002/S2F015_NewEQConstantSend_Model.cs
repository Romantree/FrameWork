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
    public class S2F15_NewEQConstantSend_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public List<S2F15_LIST_1> LIST_1 { get; set; }

        public S2F15_NewEQConstantSend_Model()
        {
            this.Stream = 2;
            this.Function = 15;
            this.FullName = "NewEQConstantSend";
            this.Name = "NewEQConstantSend";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.LIST_1 = new List<S2F15_LIST_1>();
        }
    }

    public class S2F15_LIST_1
    {
        public Int32 ECID { get; set; }

        public string ECNAME { get; set; }

        public string ECDEF { get; set; }

        public string ECSLL { get; set; }

        public string ECSUL { get; set; }

        public string ECWLL { get; set; }

        public string ECWUL { get; set; }

        public S2F15_LIST_1()
        {
            this.ECID = 0;
            this.ECNAME = string.Empty;
            this.ECDEF = string.Empty;
            this.ECSLL = string.Empty;
            this.ECSUL = string.Empty;
            this.ECWLL = string.Empty;
            this.ECWUL = string.Empty;
        }
    }
}
