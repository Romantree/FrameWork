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
    public class S16F126_RPC_DataDeleteCmdAck_Model : IXComModel
    {
        public Byte ACK16 { get; set; }

        public S16F126_RPC_DataDeleteCmdAck_Model()
        {
            this.Stream = 16;
            this.Function = 126;
            this.FullName = "RPC_DataDeleteCmdAck";
            this.Name = "RPC_DataDeleteCmdAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACK16 = 0;
        }
    }
}
