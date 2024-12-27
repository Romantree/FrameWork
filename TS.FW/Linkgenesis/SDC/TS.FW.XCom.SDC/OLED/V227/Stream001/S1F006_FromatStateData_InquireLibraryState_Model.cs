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
    public class S1F6_FromatStateData_InquireLibraryState_Model : IXComModel
    {
        public string MODULE_ID { get; set; }

        public Byte SFCD { get; set; }

        public string TRANS_POSSIBLE_STATE { get; set; }

        public List<S1F6_InquireLibraryState_LIST_1> InquireLibraryState_LIST_1 { get; set; }

        public S1F6_FromatStateData_InquireLibraryState_Model()
        {
            this.Stream = 1;
            this.Function = 6;
            this.FullName = "FromatStateData - InquireLibraryState";
            this.Name = "FromatStateData";
            this.SubName = "InquireLibraryState";
            this.IsUnDefined = false;

            this.MODULE_ID = string.Empty;
            this.SFCD = 0;
            this.TRANS_POSSIBLE_STATE = string.Empty;
            this.InquireLibraryState_LIST_1 = new List<S1F6_InquireLibraryState_LIST_1>();
        }
    }

    public class S1F6_InquireLibraryState_LIST_1
    {
        public string LIBRARYID { get; set; }

        public string STAGE_STATE { get; set; }

        public string LOCATION { get; set; }

        public string MATERIAL_ID { get; set; }

        public Int32 MATERIAL_STATE { get; set; }

        public string INS_FLAG { get; set; }

        public S1F6_InquireLibraryState_LIST_1()
        {
            this.LIBRARYID = string.Empty;
            this.STAGE_STATE = string.Empty;
            this.LOCATION = string.Empty;
            this.MATERIAL_ID = string.Empty;
            this.MATERIAL_STATE = 0;
            this.INS_FLAG = string.Empty;
        }
    }
}
