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
    public class S1F6_FromatStateData_EQOnlineParam_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Byte SFCD { get; set; }

        public List<S1F6_EQOnlineParam_LIST_1> EQOnlineParam_LIST_1 { get; set; }

        public S1F6_FromatStateData_EQOnlineParam_Model()
        {
            this.Stream = 1;
            this.Function = 6;
            this.FullName = "FromatStateData - EQOnlineParam";
            this.Name = "FromatStateData";
            this.SubName = "EQOnlineParam";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.SFCD = 0;
            this.EQOnlineParam_LIST_1 = new List<S1F6_EQOnlineParam_LIST_1>();
        }
    }

    public class S1F6_EQOnlineParam_LIST_1
    {
        public Byte EOID { get; set; }

        public List<S1F6_EQOnlineParam_LIST_2> EQOnlineParam_LIST_2 { get; set; }

        public S1F6_EQOnlineParam_LIST_1()
        {
            this.EOID = 0;
            this.EQOnlineParam_LIST_2 = new List<S1F6_EQOnlineParam_LIST_2>();
        }
    }

    public class S1F6_EQOnlineParam_LIST_2
    {
        public Byte EOMD { get; set; }

        public Byte EOV { get; set; }

        public S1F6_EQOnlineParam_LIST_2()
        {
            this.EOMD = 0;
            this.EOV = 0;
        }
    }
}
