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
    public class S1F6_FromatStateData_InquireRecipeRelationinEQs_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Byte SFCD { get; set; }

        public List<S1F6_InquireRecipeRelationinEQs_LIST_1> InquireRecipeRelationinEQs_LIST_1 { get; set; }

        public S1F6_FromatStateData_InquireRecipeRelationinEQs_Model()
        {
            this.Stream = 1;
            this.Function = 6;
            this.FullName = "FromatStateData - InquireRecipeRelationinEQs";
            this.Name = "FromatStateData";
            this.SubName = "InquireRecipeRelationinEQs";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.SFCD = 0;
            this.InquireRecipeRelationinEQs_LIST_1 = new List<S1F6_InquireRecipeRelationinEQs_LIST_1>();
        }
    }

    public class S1F6_InquireRecipeRelationinEQs_LIST_1
    {
        public string RECIPEID { get; set; }

        public string PPID { get; set; }

        public S1F6_InquireRecipeRelationinEQs_LIST_1()
        {
            this.RECIPEID = string.Empty;
            this.PPID = string.Empty;
        }
    }
}
