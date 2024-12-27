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
    public class S1F6_FromatStateData_ProcessStepInquiry_Model : IXComModel
    {
        public Byte SFCD { get; set; }

        public string MODULE_ID_1 { get; set; }

        public Byte MODULE_STATE_1 { get; set; }

        public Byte PROC_STATE_1 { get; set; }

        public Byte CUR_STEPNO_1 { get; set; }

        public List<S1F6_ProcessStepInquiry_LIST_1> ProcessStepInquiry_LIST_1 { get; set; }

        public List<S1F6_ProcessStepInquiry_LIST_2> ProcessStepInquiry_LIST_2 { get; set; }

        public List<S1F6_ProcessStepInquiry_LIST_3> ProcessStepInquiry_LIST_3 { get; set; }

        public List<S1F6_ProcessStepInquiry_LIST_4> ProcessStepInquiry_LIST_4 { get; set; }

        public S1F6_FromatStateData_ProcessStepInquiry_Model()
        {
            this.Stream = 1;
            this.Function = 6;
            this.FullName = "FromatStateData - ProcessStepInquiry";
            this.Name = "FromatStateData";
            this.SubName = "ProcessStepInquiry";
            this.IsUnDefined = false;

            this.SFCD = 0;
            this.MODULE_ID_1 = string.Empty;
            this.MODULE_STATE_1 = 0;
            this.PROC_STATE_1 = 0;
            this.CUR_STEPNO_1 = 0;
            this.ProcessStepInquiry_LIST_1 = new List<S1F6_ProcessStepInquiry_LIST_1>();
            this.ProcessStepInquiry_LIST_2 = new List<S1F6_ProcessStepInquiry_LIST_2>();
            this.ProcessStepInquiry_LIST_3 = new List<S1F6_ProcessStepInquiry_LIST_3>();
            this.ProcessStepInquiry_LIST_4 = new List<S1F6_ProcessStepInquiry_LIST_4>();
        }
    }

    public class S1F6_ProcessStepInquiry_LIST_1
    {
        public Byte STEPNO_1 { get; set; }

        public string STEP_DESC_1 { get; set; }

        public S1F6_ProcessStepInquiry_LIST_1()
        {
            this.STEPNO_1 = 0;
            this.STEP_DESC_1 = string.Empty;
        }
    }

    public class S1F6_ProcessStepInquiry_LIST_2
    {
        public string H_GLASSID_1 { get; set; }

        public S1F6_ProcessStepInquiry_LIST_2()
        {
            this.H_GLASSID_1 = string.Empty;
        }
    }

    public class S1F6_ProcessStepInquiry_LIST_3
    {
        public string MATERIAL_ID_1 { get; set; }

        public S1F6_ProcessStepInquiry_LIST_3()
        {
            this.MATERIAL_ID_1 = string.Empty;
        }
    }

    public class S1F6_ProcessStepInquiry_LIST_4
    {
        public string MODULE_ID_2 { get; set; }

        public Byte MODULE_STATE_2 { get; set; }

        public Byte PROC_STATE_2 { get; set; }

        public Byte CUR_STEPNO_2 { get; set; }

        public List<S1F6_ProcessStepInquiry_LIST_5> ProcessStepInquiry_LIST_5 { get; set; }

        public List<S1F6_ProcessStepInquiry_LIST_6> ProcessStepInquiry_LIST_6 { get; set; }

        public List<S1F6_ProcessStepInquiry_LIST_7> ProcessStepInquiry_LIST_7 { get; set; }

        public List<S1F6_ProcessStepInquiry_LIST_8> ProcessStepInquiry_LIST_8 { get; set; }

        public S1F6_ProcessStepInquiry_LIST_4()
        {
            this.MODULE_ID_2 = string.Empty;
            this.MODULE_STATE_2 = 0;
            this.PROC_STATE_2 = 0;
            this.CUR_STEPNO_2 = 0;
            this.ProcessStepInquiry_LIST_5 = new List<S1F6_ProcessStepInquiry_LIST_5>();
            this.ProcessStepInquiry_LIST_6 = new List<S1F6_ProcessStepInquiry_LIST_6>();
            this.ProcessStepInquiry_LIST_7 = new List<S1F6_ProcessStepInquiry_LIST_7>();
            this.ProcessStepInquiry_LIST_8 = new List<S1F6_ProcessStepInquiry_LIST_8>();
        }
    }

    public class S1F6_ProcessStepInquiry_LIST_5
    {
        public Byte STEPNO_2 { get; set; }

        public string STEP_DESC_2 { get; set; }

        public S1F6_ProcessStepInquiry_LIST_5()
        {
            this.STEPNO_2 = 0;
            this.STEP_DESC_2 = string.Empty;
        }
    }

    public class S1F6_ProcessStepInquiry_LIST_6
    {
        public string H_GLASSID_2 { get; set; }

        public S1F6_ProcessStepInquiry_LIST_6()
        {
            this.H_GLASSID_2 = string.Empty;
        }
    }

    public class S1F6_ProcessStepInquiry_LIST_7
    {
        public string MATERIAL_ID_2 { get; set; }

        public S1F6_ProcessStepInquiry_LIST_7()
        {
            this.MATERIAL_ID_2 = string.Empty;
        }
    }

    public class S1F6_ProcessStepInquiry_LIST_8
    {
        public string MODULE_ID_3 { get; set; }

        public Byte MODULE_STATE_3 { get; set; }

        public Byte PROC_STATE_3 { get; set; }

        public Byte CUR_STEPNO_3 { get; set; }

        public List<S1F6_ProcessStepInquiry_LIST_9> ProcessStepInquiry_LIST_9 { get; set; }

        public List<S1F6_ProcessStepInquiry_LIST_10> ProcessStepInquiry_LIST_10 { get; set; }

        public List<S1F6_ProcessStepInquiry_LIST_11> ProcessStepInquiry_LIST_11 { get; set; }

        public List<S1F6_ProcessStepInquiry_LIST_12> ProcessStepInquiry_LIST_12 { get; set; }

        public S1F6_ProcessStepInquiry_LIST_8()
        {
            this.MODULE_ID_3 = string.Empty;
            this.MODULE_STATE_3 = 0;
            this.PROC_STATE_3 = 0;
            this.CUR_STEPNO_3 = 0;
            this.ProcessStepInquiry_LIST_9 = new List<S1F6_ProcessStepInquiry_LIST_9>();
            this.ProcessStepInquiry_LIST_10 = new List<S1F6_ProcessStepInquiry_LIST_10>();
            this.ProcessStepInquiry_LIST_11 = new List<S1F6_ProcessStepInquiry_LIST_11>();
            this.ProcessStepInquiry_LIST_12 = new List<S1F6_ProcessStepInquiry_LIST_12>();
        }
    }

    public class S1F6_ProcessStepInquiry_LIST_9
    {
        public Byte STEPNO_3 { get; set; }

        public string STEP_DESC_3 { get; set; }

        public S1F6_ProcessStepInquiry_LIST_9()
        {
            this.STEPNO_3 = 0;
            this.STEP_DESC_3 = string.Empty;
        }
    }

    public class S1F6_ProcessStepInquiry_LIST_10
    {
        public string H_GLASSID_3 { get; set; }

        public S1F6_ProcessStepInquiry_LIST_10()
        {
            this.H_GLASSID_3 = string.Empty;
        }
    }

    public class S1F6_ProcessStepInquiry_LIST_11
    {
        public string MATERIAL_ID_3 { get; set; }

        public S1F6_ProcessStepInquiry_LIST_11()
        {
            this.MATERIAL_ID_3 = string.Empty;
        }
    }

    public class S1F6_ProcessStepInquiry_LIST_12
    {
        public string MODULE_ID_4 { get; set; }

        public Byte MODULE_STATE_4 { get; set; }

        public Byte PROC_STATE_4 { get; set; }

        public Byte CUR_STEPNO_4 { get; set; }

        public List<S1F6_ProcessStepInquiry_LIST_13> ProcessStepInquiry_LIST_13 { get; set; }

        public List<S1F6_ProcessStepInquiry_LIST_14> ProcessStepInquiry_LIST_14 { get; set; }

        public List<S1F6_ProcessStepInquiry_LIST_15> ProcessStepInquiry_LIST_15 { get; set; }

        public List<S1F6_ProcessStepInquiry_LIST_16> ProcessStepInquiry_LIST_16 { get; set; }

        public S1F6_ProcessStepInquiry_LIST_12()
        {
            this.MODULE_ID_4 = string.Empty;
            this.MODULE_STATE_4 = 0;
            this.PROC_STATE_4 = 0;
            this.CUR_STEPNO_4 = 0;
            this.ProcessStepInquiry_LIST_13 = new List<S1F6_ProcessStepInquiry_LIST_13>();
            this.ProcessStepInquiry_LIST_14 = new List<S1F6_ProcessStepInquiry_LIST_14>();
            this.ProcessStepInquiry_LIST_15 = new List<S1F6_ProcessStepInquiry_LIST_15>();
            this.ProcessStepInquiry_LIST_16 = new List<S1F6_ProcessStepInquiry_LIST_16>();
        }
    }

    public class S1F6_ProcessStepInquiry_LIST_13
    {
        public Byte STEPNO_4 { get; set; }

        public string STEP_DESC_4 { get; set; }

        public S1F6_ProcessStepInquiry_LIST_13()
        {
            this.STEPNO_4 = 0;
            this.STEP_DESC_4 = string.Empty;
        }
    }

    public class S1F6_ProcessStepInquiry_LIST_14
    {
        public string H_GLASSID_4 { get; set; }

        public S1F6_ProcessStepInquiry_LIST_14()
        {
            this.H_GLASSID_4 = string.Empty;
        }
    }

    public class S1F6_ProcessStepInquiry_LIST_15
    {
        public string MATERIAL_ID_4 { get; set; }

        public S1F6_ProcessStepInquiry_LIST_15()
        {
            this.MATERIAL_ID_4 = string.Empty;
        }
    }

    public class S1F6_ProcessStepInquiry_LIST_16
    {
        public string MODULE_ID_5 { get; set; }

        public Byte MODULE_STATE_5 { get; set; }

        public Byte PROC_STATE_5 { get; set; }

        public Byte CUR_STEPNO_5 { get; set; }

        public List<S1F6_ProcessStepInquiry_LIST_17> ProcessStepInquiry_LIST_17 { get; set; }

        public List<S1F6_ProcessStepInquiry_LIST_18> ProcessStepInquiry_LIST_18 { get; set; }

        public List<S1F6_ProcessStepInquiry_LIST_19> ProcessStepInquiry_LIST_19 { get; set; }

        public S1F6_ProcessStepInquiry_LIST_16()
        {
            this.MODULE_ID_5 = string.Empty;
            this.MODULE_STATE_5 = 0;
            this.PROC_STATE_5 = 0;
            this.CUR_STEPNO_5 = 0;
            this.ProcessStepInquiry_LIST_17 = new List<S1F6_ProcessStepInquiry_LIST_17>();
            this.ProcessStepInquiry_LIST_18 = new List<S1F6_ProcessStepInquiry_LIST_18>();
            this.ProcessStepInquiry_LIST_19 = new List<S1F6_ProcessStepInquiry_LIST_19>();
        }
    }

    public class S1F6_ProcessStepInquiry_LIST_17
    {
        public Byte STEPNO_5 { get; set; }

        public string STEP_DESC_5 { get; set; }

        public S1F6_ProcessStepInquiry_LIST_17()
        {
            this.STEPNO_5 = 0;
            this.STEP_DESC_5 = string.Empty;
        }
    }

    public class S1F6_ProcessStepInquiry_LIST_18
    {
        public string H_GLASSID_5 { get; set; }

        public S1F6_ProcessStepInquiry_LIST_18()
        {
            this.H_GLASSID_5 = string.Empty;
        }
    }

    public class S1F6_ProcessStepInquiry_LIST_19
    {
        public string MATERIAL_ID_5 { get; set; }

        public S1F6_ProcessStepInquiry_LIST_19()
        {
            this.MATERIAL_ID_5 = string.Empty;
        }
    }
}
