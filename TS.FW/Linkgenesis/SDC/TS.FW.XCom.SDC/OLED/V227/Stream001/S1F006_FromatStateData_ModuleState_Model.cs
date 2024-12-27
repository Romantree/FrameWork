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
    public class S1F6_FromatStateData_ModuleState_Model : IXComModel
    {
        public Byte SFCD { get; set; }

        public string MODULE_ID_1 { get; set; }

        public Byte MODULE_STATE_1 { get; set; }

        public Byte PROC_STATE_1 { get; set; }

        public Byte MCMD { get; set; }

        public List<S1F6_ModuleState_LIST_1> ModuleState_LIST_1 { get; set; }

        public S1F6_FromatStateData_ModuleState_Model()
        {
            this.Stream = 1;
            this.Function = 6;
            this.FullName = "FromatStateData - ModuleState";
            this.Name = "FromatStateData";
            this.SubName = "ModuleState";
            this.IsUnDefined = false;

            this.SFCD = 0;
            this.MODULE_ID_1 = string.Empty;
            this.MODULE_STATE_1 = 0;
            this.PROC_STATE_1 = 0;
            this.MCMD = 0;
            this.ModuleState_LIST_1 = new List<S1F6_ModuleState_LIST_1>();
        }
    }

    public class S1F6_ModuleState_LIST_1
    {
        public string MODULE_ID_2 { get; set; }

        public Byte MODULE_STATE_2 { get; set; }

        public Byte PROC_STATE_2 { get; set; }

        public List<S1F6_ModuleState_LIST_2> ModuleState_LIST_2 { get; set; }

        public S1F6_ModuleState_LIST_1()
        {
            this.MODULE_ID_2 = string.Empty;
            this.MODULE_STATE_2 = 0;
            this.PROC_STATE_2 = 0;
            this.ModuleState_LIST_2 = new List<S1F6_ModuleState_LIST_2>();
        }
    }

    public class S1F6_ModuleState_LIST_2
    {
        public string MODULE_ID_3 { get; set; }

        public Byte MODULE_STATE_3 { get; set; }

        public Byte PROC_STATE_3 { get; set; }

        public List<S1F6_ModuleState_LIST_3> ModuleState_LIST_3 { get; set; }

        public S1F6_ModuleState_LIST_2()
        {
            this.MODULE_ID_3 = string.Empty;
            this.MODULE_STATE_3 = 0;
            this.PROC_STATE_3 = 0;
            this.ModuleState_LIST_3 = new List<S1F6_ModuleState_LIST_3>();
        }
    }

    public class S1F6_ModuleState_LIST_3
    {
        public string MODULE_ID_4 { get; set; }

        public Byte MODULE_STATE_4 { get; set; }

        public Byte PROC_STATE_4 { get; set; }

        public List<S1F6_ModuleState_LIST_4> ModuleState_LIST_4 { get; set; }

        public S1F6_ModuleState_LIST_3()
        {
            this.MODULE_ID_4 = string.Empty;
            this.MODULE_STATE_4 = 0;
            this.PROC_STATE_4 = 0;
            this.ModuleState_LIST_4 = new List<S1F6_ModuleState_LIST_4>();
        }
    }

    public class S1F6_ModuleState_LIST_4
    {
        public string MODULE_ID_5 { get; set; }

        public Byte MODULE_STATE_5 { get; set; }

        public Byte PROC_STATE_5 { get; set; }

        public S1F6_ModuleState_LIST_4()
        {
            this.MODULE_ID_5 = string.Empty;
            this.MODULE_STATE_5 = 0;
            this.PROC_STATE_5 = 0;
        }
    }
}
