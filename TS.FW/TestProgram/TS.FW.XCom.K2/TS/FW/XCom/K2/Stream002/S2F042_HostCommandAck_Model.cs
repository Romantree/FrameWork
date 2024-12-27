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
    public class S2F42_HostCommandAck_Model : IXComModel
    {
        public S2F42_HostCommandAck_Model()
        {
            this.Stream = 2;
            this.Function = 42;
            this.FullName = "HostCommandAck";
            this.Name = "HostCommandAck";
            this.SubName = "";
            this.IsUnDefined = true;
        }

        public override void RecvData(IXComData data)
        {
            
        }

        public override void SendData(XCOM xcom, int msgId)
        {
            
        }
    }
}
