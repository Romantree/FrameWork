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
    public class S3F122_GlassUnitInformationRely_Model : IXComModel
    {
        public Byte ACKC3 { get; set; }

        public S3F122_GlassUnitInformationRely_Model()
        {
            this.Stream = 3;
            this.Function = 122;
            this.FullName = "GlassUnitInformationRely";
            this.Name = "GlassUnitInformationRely";
            this.SubName = "";
            this.IsUnDefined = false;

            this.ACKC3 = 0;
        }
    }
}
