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
    public class S1F6_FormattedStateData_ModuleState_Model : IXComModel
    {
        public Byte SFCD { get; set; }

        public string MODULEID { get; set; }

        public Byte MODULE_STATE { get; set; }

        public Byte PROC_STATE { get; set; }

        public Byte MCMD { get; set; }

        public List<S1F6_ModuleLayer1Count> ModuleLayer1Count { get; set; }

        public S1F6_FormattedStateData_ModuleState_Model()
        {
            this.Stream = 1;
            this.Function = 6;
            this.FullName = "FormattedStateData_ModuleState";
            this.Name = "FormattedStateData_ModuleState";
            this.SubName = "";
            this.IsUnDefined = false;

            this.SFCD = 0;
            this.MODULEID = string.Empty;
            this.MODULE_STATE = 0;
            this.PROC_STATE = 0;
            this.MCMD = 0;
            this.ModuleLayer1Count = new List<S1F6_ModuleLayer1Count>();
        }
    }

    public class S1F6_ModuleLayer1Count
    {
        public string MODULEID_1 { get; set; }

        public Byte MODULE_STATE_1 { get; set; }

        public Byte PROC_STATE_1 { get; set; }

        public List<S1F6_ModuleLayer2Count> ModuleLayer2Count { get; set; }

        public S1F6_ModuleLayer1Count()
        {
            this.MODULEID_1 = string.Empty;
            this.MODULE_STATE_1 = 0;
            this.PROC_STATE_1 = 0;
            this.ModuleLayer2Count = new List<S1F6_ModuleLayer2Count>();
        }
    }

    public class S1F6_ModuleLayer2Count
    {
        public string MODULEID_2 { get; set; }

        public Byte MODULE_STATE_2 { get; set; }

        public Byte PROC_STATE_2 { get; set; }

        public List<S1F6_ModuleLayer3Count> ModuleLayer3Count { get; set; }

        public S1F6_ModuleLayer2Count()
        {
            this.MODULEID_2 = string.Empty;
            this.MODULE_STATE_2 = 0;
            this.PROC_STATE_2 = 0;
            this.ModuleLayer3Count = new List<S1F6_ModuleLayer3Count>();
        }
    }

    public class S1F6_ModuleLayer3Count
    {
        public string MODULEID_3 { get; set; }

        public Byte MODULE_STATE_3 { get; set; }

        public Byte PROC_STATE_3 { get; set; }

        public List<S1F6_ModuleLayer4Count> ModuleLayer4Count { get; set; }

        public S1F6_ModuleLayer3Count()
        {
            this.MODULEID_3 = string.Empty;
            this.MODULE_STATE_3 = 0;
            this.PROC_STATE_3 = 0;
            this.ModuleLayer4Count = new List<S1F6_ModuleLayer4Count>();
        }
    }

    public class S1F6_ModuleLayer4Count
    {
        public string MODULEID_4 { get; set; }

        public Byte MODULE_STATE_4 { get; set; }

        public Byte PROC_STATE_4 { get; set; }

        public S1F6_ModuleLayer4Count()
        {
            this.MODULEID_4 = string.Empty;
            this.MODULE_STATE_4 = 0;
            this.PROC_STATE_4 = 0;
        }
    }
}
