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
    public class S2F41_HostCommandSend_MaterialProcessCmd_Model : IXComModel
    {
        public Int32 RCMD { get; set; }

        public string MODULE_ID_NAME { get; set; }

        public string MODULE_ID { get; set; }

        public string MATERIALID_NAME { get; set; }

        public string MATERIALID { get; set; }

        public string SLOTNO_NAME { get; set; }

        public List<S2F41_MaterialProcessCmd_LIST_1> MaterialProcessCmd_LIST_1 { get; set; }

        public S2F41_HostCommandSend_MaterialProcessCmd_Model()
        {
            this.Stream = 2;
            this.Function = 41;
            this.FullName = "HostCommandSend - MaterialProcessCmd";
            this.Name = "HostCommandSend";
            this.SubName = "MaterialProcessCmd";
            this.IsUnDefined = false;

            this.RCMD = 0;
            this.MODULE_ID_NAME = string.Empty;
            this.MODULE_ID = string.Empty;
            this.MATERIALID_NAME = string.Empty;
            this.MATERIALID = string.Empty;
            this.SLOTNO_NAME = string.Empty;
            this.MaterialProcessCmd_LIST_1 = new List<S2F41_MaterialProcessCmd_LIST_1>();
        }
    }

    public class S2F41_MaterialProcessCmd_LIST_1
    {
        public Byte SLOTNO { get; set; }

        public S2F41_MaterialProcessCmd_LIST_1()
        {
            this.SLOTNO = 0;
        }
    }
}
