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
    public class S7F2_PPL_Grant_Model : IXComModel
    {
        public Byte PPGNT { get; set; }

        public S7F2_PPL_Grant_Model()
        {
            this.Stream = 7;
            this.Function = 2;
            this.FullName = "PPL_Grant";
            this.Name = "PPL_Grant";
            this.SubName = "";
            this.IsUnDefined = false;

            this.PPGNT = 0;
        }
    }
}
