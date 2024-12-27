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
    public class S16F6_PPC_DataDeletionCmdAckd_Model : IXComModel
    {
        public Byte ACK16 { get; set; }

        public S16F6_PPC_DataDeletionCmdAckd_Model()
        {
            this.Stream = 16;
            this.Function = 6;
            this.FullName = "PPC_DataDeletionCmdAckd";
            this.Name = "PPC_DataDeletionCmdAckd";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACK16 = 0;
        }
    }
}
