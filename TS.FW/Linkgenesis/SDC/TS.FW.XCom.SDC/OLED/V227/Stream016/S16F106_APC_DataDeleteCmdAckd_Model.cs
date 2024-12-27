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
    public class S16F106_APC_DataDeleteCmdAckd_Model : IXComModel
    {
        public Byte TCACK { get; set; }

        public S16F106_APC_DataDeleteCmdAckd_Model()
        {
            this.Stream = 16;
            this.Function = 106;
            this.FullName = "APC_DataDeleteCmdAckd";
            this.Name = "APC_DataDeleteCmdAckd";
            this.SubName = "";
            this.IsUnDefined = false;

            this.TCACK = 0;
        }
    }
}
