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
    public class S1F6_FromatStateData_InEQPathDefinitionInquiry_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Byte SFCD { get; set; }

        public List<S1F6_InEQPathDefinitionInquiry_LIST_1> InEQPathDefinitionInquiry_LIST_1 { get; set; }

        public S1F6_FromatStateData_InEQPathDefinitionInquiry_Model()
        {
            this.Stream = 1;
            this.Function = 6;
            this.FullName = "FromatStateData - InEQPathDefinitionInquiry";
            this.Name = "FromatStateData";
            this.SubName = "InEQPathDefinitionInquiry";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.SFCD = 0;
            this.InEQPathDefinitionInquiry_LIST_1 = new List<S1F6_InEQPathDefinitionInquiry_LIST_1>();
        }
    }

    public class S1F6_InEQPathDefinitionInquiry_LIST_1
    {
        public string PATHNAME { get; set; }

        public List<S1F6_InEQPathDefinitionInquiry_LIST_2> InEQPathDefinitionInquiry_LIST_2 { get; set; }

        public S1F6_InEQPathDefinitionInquiry_LIST_1()
        {
            this.PATHNAME = string.Empty;
            this.InEQPathDefinitionInquiry_LIST_2 = new List<S1F6_InEQPathDefinitionInquiry_LIST_2>();
        }
    }

    public class S1F6_InEQPathDefinitionInquiry_LIST_2
    {
        public string PATH_MODULE_ID { get; set; }

        public string PATH_ORDER { get; set; }

        public S1F6_InEQPathDefinitionInquiry_LIST_2()
        {
            this.PATH_MODULE_ID = string.Empty;
            this.PATH_ORDER = string.Empty;
        }
    }
}
