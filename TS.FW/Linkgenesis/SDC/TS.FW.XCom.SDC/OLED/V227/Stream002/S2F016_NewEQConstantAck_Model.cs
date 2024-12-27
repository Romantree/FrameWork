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
    public class S2F16_NewEQConstantAck_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Byte MIACK { get; set; }

        public List<S2F16_LIST_1> LIST_1 { get; set; }

        public S2F16_NewEQConstantAck_Model()
        {
            this.Stream = 2;
            this.Function = 16;
            this.FullName = "NewEQConstantAck";
            this.Name = "NewEQConstantAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.MIACK = 0;
            this.LIST_1 = new List<S2F16_LIST_1>();
        }
    }

    public class S2F16_LIST_1
    {
        public Byte TEAC { get; set; }

        public Int32 ECID { get; set; }

        public Byte EAC_1 { get; set; }

        public string ECNAME { get; set; }

        public Byte EAC_2 { get; set; }

        public string ECDEF { get; set; }

        public Byte EAC_3 { get; set; }

        public string ECSLL { get; set; }

        public Byte EAC_4 { get; set; }

        public string ECSUL { get; set; }

        public Byte EAC_5 { get; set; }

        public string ECWLL { get; set; }

        public Byte EAC_6 { get; set; }

        public string ECWUL { get; set; }

        public Byte EAC_7 { get; set; }

        public S2F16_LIST_1()
        {
            this.TEAC = 0;
            this.ECID = 0;
            this.EAC_1 = 0;
            this.ECNAME = string.Empty;
            this.EAC_2 = 0;
            this.ECDEF = string.Empty;
            this.EAC_3 = 0;
            this.ECSLL = string.Empty;
            this.EAC_4 = 0;
            this.ECSUL = string.Empty;
            this.EAC_5 = 0;
            this.ECWLL = string.Empty;
            this.EAC_6 = 0;
            this.ECWUL = string.Empty;
            this.EAC_7 = 0;
        }
    }
}
