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
    public class S7F104_PPIDExistenceCheckAck_Model : IXComModel
    {
        public Byte ACKC7 { get; set; }

        public S7F104_PPIDExistenceCheckAck_Model()
        {
            this.Stream = 7;
            this.Function = 104;
            this.FullName = "PPIDExistenceCheckAck";
            this.Name = "PPIDExistenceCheckAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACKC7 = 0;
        }
    }
}
