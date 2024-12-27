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
    public class S1F1_AreYouThereRequest_Model : IXComModel
    {
        public S1F1_AreYouThereRequest_Model()
        {
            this.Stream = 1;
            this.Function = 1;
            this.FullName = "AreYouThereRequest";
            this.Name = "AreYouThereRequest";
            this.SubName = "";
            this.IsUnDefined = false;
        }
    }
}
