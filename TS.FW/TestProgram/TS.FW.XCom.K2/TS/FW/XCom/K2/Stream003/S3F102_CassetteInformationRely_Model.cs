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
    public class S3F102_CassetteInformationRely_Model : IXComModel
    {
        public Byte ACKC3 { get; set; }

        public S3F102_CassetteInformationRely_Model()
        {
            this.Stream = 3;
            this.Function = 102;
            this.FullName = "CassetteInformationRely";
            this.Name = "CassetteInformationRely";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACKC3 = 0;
        }
    }
}
