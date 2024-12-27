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
    public class S1F6_FromatStateData_CurrentRecipeInquiry_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Byte SFCD { get; set; }

        public List<S1F6_CurrentRecipeInquiry_LIST_1> CurrentRecipeInquiry_LIST_1 { get; set; }

        public S1F6_FromatStateData_CurrentRecipeInquiry_Model()
        {
            this.Stream = 1;
            this.Function = 6;
            this.FullName = "FromatStateData - CurrentRecipeInquiry";
            this.Name = "FromatStateData";
            this.SubName = "CurrentRecipeInquiry";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.SFCD = 0;
            this.CurrentRecipeInquiry_LIST_1 = new List<S1F6_CurrentRecipeInquiry_LIST_1>();
        }
    }

    public class S1F6_CurrentRecipeInquiry_LIST_1
    {
        public string SUB_MODULE_ID { get; set; }

        public string CUR_RECIPE_TYPE2 { get; set; }

        public string CUR_RECIPE_TYPE1 { get; set; }

        public S1F6_CurrentRecipeInquiry_LIST_1()
        {
            this.SUB_MODULE_ID = string.Empty;
            this.CUR_RECIPE_TYPE2 = string.Empty;
            this.CUR_RECIPE_TYPE1 = string.Empty;
        }
    }
}
