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
    public class S1F1_AreyouThereReq_Model : IXComModel
    {
        public S1F1_AreyouThereReq_Model()
        {
            this.Stream = 1;
            this.Function = 1;
            this.FullName = "AreyouThereReq";
            this.Name = "AreyouThereReq";
            this.SubName = "";
            this.IsUnDefined = false;
        }
    }
}
