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
    public class S8F12_MassData_TransferAck_Model : IXComModel
    {
        public Byte ACK { get; set; }

        public S8F12_MassData_TransferAck_Model()
        {
            this.Stream = 8;
            this.Function = 12;
            this.FullName = "MassData TransferAck";
            this.Name = "MassData TransferAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACK = 0;
        }
    }
}
