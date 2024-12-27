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
    public class S8F2_Multi_UseDataSetAck_Model : IXComModel
    {
        public Byte ACK { get; set; }

        public List<S8F2_LIST_1> LIST_1 { get; set; }

        public S8F2_Multi_UseDataSetAck_Model()
        {
            this.Stream = 8;
            this.Function = 2;
            this.FullName = "Multi UseDataSetAck";
            this.Name = "Multi UseDataSetAck";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACK = 0;
            this.LIST_1 = new List<S8F2_LIST_1>();
        }
    }

    public class S8F2_LIST_1
    {
        public string DATA_TYPE { get; set; }

        public string ITEM_NAME { get; set; }

        public string ITEM_VALUE { get; set; }

        public string REFRENCE { get; set; }

        public Byte EAC { get; set; }

        public S8F2_LIST_1()
        {
            this.DATA_TYPE = string.Empty;
            this.ITEM_NAME = string.Empty;
            this.ITEM_VALUE = string.Empty;
            this.REFRENCE = string.Empty;
            this.EAC = 0;
        }
    }
}
