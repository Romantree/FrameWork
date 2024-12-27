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
    public class S6F13_NameListVariableDataSend_GlassLotData_Model : IXComModel
    {
        public Byte DATA_ID { get; set; }

        public Int32 CEID { get; set; }

        public Byte RPTID_1 { get; set; }

        public List<S6F13_GlassLotData_LIST_1> GlassLotData_LIST_1 { get; set; }

        public Byte RPTID_2 { get; set; }

        public List<S6F13_GlassLotData_LIST_2> GlassLotData_LIST_2 { get; set; }

        public S6F13_NameListVariableDataSend_GlassLotData_Model()
        {
            this.Stream = 6;
            this.Function = 13;
            this.FullName = "NameListVariableDataSend - GlassLotData";
            this.Name = "NameListVariableDataSend";
            this.SubName = "GlassLotData";
            this.IsUnDefined = false;

            this.DATA_ID = 0;
            this.CEID = 0;
            this.RPTID_1 = 0;
            this.GlassLotData_LIST_1 = new List<S6F13_GlassLotData_LIST_1>();
            this.RPTID_2 = 0;
            this.GlassLotData_LIST_2 = new List<S6F13_GlassLotData_LIST_2>();
        }
    }

    public class S6F13_GlassLotData_LIST_1
    {
        public string H_GLASSID { get; set; }

        public string E_GLASSID { get; set; }

        public string LOTID { get; set; }

        public string BATCHID { get; set; }

        public string JOBID { get; set; }

        public string PORTID { get; set; }

        public string SLOTNO { get; set; }

        public string PRODUCT_TYPE { get; set; }

        public string PORDUCT_KIND { get; set; }

        public string PORDUCTID { get; set; }

        public string RUNSPECID { get; set; }

        public string LAYERID { get; set; }

        public string STEPID { get; set; }

        public string PPID { get; set; }

        public string FLOWID { get; set; }

        public Int32[] GLASS_SIZE { get; set; }

        public Int32 GLASS_THICKNESS { get; set; }

        public Int32 GLASS_STATE { get; set; }

        public string GLASS_ORDER { get; set; }

        public string COMMENT { get; set; }

        public S6F13_GlassLotData_LIST_1()
        {
            this.H_GLASSID = string.Empty;
            this.E_GLASSID = string.Empty;
            this.LOTID = string.Empty;
            this.BATCHID = string.Empty;
            this.JOBID = string.Empty;
            this.PORTID = string.Empty;
            this.SLOTNO = string.Empty;
            this.PRODUCT_TYPE = string.Empty;
            this.PORDUCT_KIND = string.Empty;
            this.PORDUCTID = string.Empty;
            this.RUNSPECID = string.Empty;
            this.LAYERID = string.Empty;
            this.STEPID = string.Empty;
            this.PPID = string.Empty;
            this.FLOWID = string.Empty;
            this.GLASS_SIZE = new Int32[2];
            this.GLASS_THICKNESS = 0;
            this.GLASS_STATE = 0;
            this.GLASS_ORDER = string.Empty;
            this.COMMENT = string.Empty;
        }
    }

    public class S6F13_GlassLotData_LIST_2
    {
        public string MODULE_ID { get; set; }

        public string RAWPATH { get; set; }

        public string GLASS_DATA_FILE_PATH { get; set; }

        public string SUMPATH { get; set; }

        public string LOT_DATA_FILE_PATH { get; set; }

        public string IMGPATH { get; set; }

        public string IMAGE_FILE_PATH { get; set; }

        public string DISK { get; set; }

        public string FILE_DISK { get; set; }

        public S6F13_GlassLotData_LIST_2()
        {
            this.MODULE_ID = string.Empty;
            this.RAWPATH = string.Empty;
            this.GLASS_DATA_FILE_PATH = string.Empty;
            this.SUMPATH = string.Empty;
            this.LOT_DATA_FILE_PATH = string.Empty;
            this.IMGPATH = string.Empty;
            this.IMAGE_FILE_PATH = string.Empty;
            this.DISK = string.Empty;
            this.FILE_DISK = string.Empty;
        }
    }
}
