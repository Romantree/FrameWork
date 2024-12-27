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
    public class S2F42_HostCommandAck_InEQPathChangeCommand_Model : IXComModel
    {
        public Int32 RCMD { get; set; }

        public Byte HCACK { get; set; }

        public List<S2F42_InEQPathChangeCommand_LIST_1> InEQPathChangeCommand_LIST_1 { get; set; }

        public S2F42_HostCommandAck_InEQPathChangeCommand_Model()
        {
            this.Stream = 2;
            this.Function = 42;
            this.FullName = "HostCommandAck - InEQPathChangeCommand";
            this.Name = "HostCommandAck";
            this.SubName = "InEQPathChangeCommand";
            this.IsUnDefined = false;

            this.RCMD = 0;
            this.HCACK = 0;
            this.InEQPathChangeCommand_LIST_1 = new List<S2F42_InEQPathChangeCommand_LIST_1>();
        }
    }

    public class S2F42_InEQPathChangeCommand_LIST_1
    {
        public string PATHNAME { get; set; }

        public List<S2F42_InEQPathChangeCommand_LIST_2> InEQPathChangeCommand_LIST_2 { get; set; }

        public S2F42_InEQPathChangeCommand_LIST_1()
        {
            this.PATHNAME = string.Empty;
            this.InEQPathChangeCommand_LIST_2 = new List<S2F42_InEQPathChangeCommand_LIST_2>();
        }
    }

    public class S2F42_InEQPathChangeCommand_LIST_2
    {
        public string PATH_MODULE_ID { get; set; }

        public string PATH_ORDER { get; set; }

        public S2F42_InEQPathChangeCommand_LIST_2()
        {
            this.PATH_MODULE_ID = string.Empty;
            this.PATH_ORDER = string.Empty;
        }
    }
}
