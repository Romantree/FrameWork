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
    public class S2F42_HostCommandAck_MaterialProcessCmd_Model : IXComModel
    {
        public Int32 RCMD { get; set; }

        public Byte HCACK { get; set; }

        public string MODULE_ID_NAME { get; set; }

        public string MODULE_ID { get; set; }

        public Byte CPACK_1 { get; set; }

        public string MATERIALID_NAME { get; set; }

        public string MATERIALID { get; set; }

        public Byte CPACK_2 { get; set; }

        public string SLOT_NO_NAME { get; set; }

        public List<S2F42_MaterialProcessCmd_LIST_1> MaterialProcessCmd_LIST_1 { get; set; }

        public Byte CPACK_3 { get; set; }

        public S2F42_HostCommandAck_MaterialProcessCmd_Model()
        {
            this.Stream = 2;
            this.Function = 42;
            this.FullName = "HostCommandAck - MaterialProcessCmd";
            this.Name = "HostCommandAck";
            this.SubName = "MaterialProcessCmd";
            this.IsUnDefined = false;

            this.RCMD = 0;
            this.HCACK = 0;
            this.MODULE_ID_NAME = string.Empty;
            this.MODULE_ID = string.Empty;
            this.CPACK_1 = 0;
            this.MATERIALID_NAME = string.Empty;
            this.MATERIALID = string.Empty;
            this.CPACK_2 = 0;
            this.SLOT_NO_NAME = string.Empty;
            this.MaterialProcessCmd_LIST_1 = new List<S2F42_MaterialProcessCmd_LIST_1>();
            this.CPACK_3 = 0;
        }
    }

    public class S2F42_MaterialProcessCmd_LIST_1
    {
        public Byte SLOT_NO { get; set; }

        public S2F42_MaterialProcessCmd_LIST_1()
        {
            this.SLOT_NO = 0;
        }
    }
}
