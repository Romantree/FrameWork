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
    public class S2F16_NewEquipmentConstantAck_Model : IXComModel
    {
        public string MODULEID { get; set; }

        public Byte MIACK { get; set; }

        public List<S2F16_ECIDs> ECIDs { get; set; }

        public S2F16_NewEquipmentConstantAck_Model()
        {
            this.Stream = 2;
            this.Function = 16;
            this.FullName = "NewEquipmentConstantAck";
            this.Name = "NewEquipmentConstantAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.MODULEID = string.Empty;
            this.MIACK = 0;
            this.ECIDs = new List<S2F16_ECIDs>();
        }
    }

    public class S2F16_ECIDs
    {
        public Byte TEAC { get; set; }

        public Int32 ECID { get; set; }

        public Byte EAC1 { get; set; }

        public Int32 ECNAME { get; set; }

        public Byte EAC2 { get; set; }

        public Int32 ECDEF { get; set; }

        public Byte EAC3 { get; set; }

        public Int32 ECSLL { get; set; }

        public Byte EAC4 { get; set; }

        public Int32 ECSUL { get; set; }

        public Byte EAC5 { get; set; }

        public Int32 ECWLL { get; set; }

        public Byte EAC6 { get; set; }

        public Int32 ECWUL { get; set; }

        public Byte EAC7 { get; set; }

        public S2F16_ECIDs()
        {
            this.TEAC = 0;
            this.ECID = 0;
            this.EAC1 = 0;
            this.ECNAME = 0;
            this.EAC2 = 0;
            this.ECDEF = 0;
            this.EAC3 = 0;
            this.ECSLL = 0;
            this.EAC4 = 0;
            this.ECSUL = 0;
            this.EAC5 = 0;
            this.ECWLL = 0;
            this.EAC6 = 0;
            this.ECWUL = 0;
            this.EAC7 = 0;
        }
    }
}
