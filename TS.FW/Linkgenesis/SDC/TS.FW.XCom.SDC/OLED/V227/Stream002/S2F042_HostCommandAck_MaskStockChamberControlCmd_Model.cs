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
    public class S2F42_HostCommandAck_MaskStockChamberControlCmd_Model : IXComModel
    {
        public Int32 RCMD { get; set; }

        public Byte HCACK { get; set; }

        public string MODULE_ID_NAME { get; set; }

        public string MODULE_ID { get; set; }

        public string CSTID_NAME { get; set; }

        public string CSTID { get; set; }

        public string CUR_POS_NAME { get; set; }

        public string CUR_POS { get; set; }

        public string ACTION_NAME { get; set; }

        public string ACTION { get; set; }

        public S2F42_HostCommandAck_MaskStockChamberControlCmd_Model()
        {
            this.Stream = 2;
            this.Function = 42;
            this.FullName = "HostCommandAck - MaskStockChamberControlCmd";
            this.Name = "HostCommandAck";
            this.SubName = "MaskStockChamberControlCmd";
            this.IsUnDefined = false;

            this.RCMD = 0;
            this.HCACK = 0;
            this.MODULE_ID_NAME = string.Empty;
            this.MODULE_ID = string.Empty;
            this.CSTID_NAME = string.Empty;
            this.CSTID = string.Empty;
            this.CUR_POS_NAME = string.Empty;
            this.CUR_POS = string.Empty;
            this.ACTION_NAME = string.Empty;
            this.ACTION = string.Empty;
        }
    }
}
