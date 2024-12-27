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
    public class S1F6_FromatStateData_InquireMaterialInfo_Model : IXComModel
    {
        public string MODULE_ID_1 { get; set; }

        public Byte SFCD { get; set; }

        public Byte RPTID_1 { get; set; }

        public string MODULE_ID_2 { get; set; }

        public Byte MCMD { get; set; }

        public Byte MODULE_STATE { get; set; }

        public Byte PROC_STATE { get; set; }

        public Byte BYWHO { get; set; }

        public string OPERID { get; set; }

        public Byte RPTID_2 { get; set; }

        public List<S1F6_InquireMaterialInfo_LIST_1> InquireMaterialInfo_LIST_1 { get; set; }

        public S1F6_FromatStateData_InquireMaterialInfo_Model()
        {
            this.Stream = 1;
            this.Function = 6;
            this.FullName = "FromatStateData - InquireMaterialInfo";
            this.Name = "FromatStateData";
            this.SubName = "InquireMaterialInfo";
            this.IsUnDefined = false;

            this.MODULE_ID_1 = string.Empty;
            this.SFCD = 0;
            this.RPTID_1 = 0;
            this.MODULE_ID_2 = string.Empty;
            this.MCMD = 0;
            this.MODULE_STATE = 0;
            this.PROC_STATE = 0;
            this.BYWHO = 0;
            this.OPERID = string.Empty;
            this.RPTID_2 = 0;
            this.InquireMaterialInfo_LIST_1 = new List<S1F6_InquireMaterialInfo_LIST_1>();
        }
    }

    public class S1F6_InquireMaterialInfo_LIST_1
    {
        public string M_TRAYID { get; set; }

        public string M_BATCHID { get; set; }

        public Byte M_KIND { get; set; }

        public string M_TYPE { get; set; }

        public string M_MAKER { get; set; }

        public string M_CODE { get; set; }

        public string M_REVNO { get; set; }

        public string TRAY_STATE { get; set; }

        public Int64 TOTAL_QTY { get; set; }

        public Int64 USED_QTY { get; set; }

        public Int64 INEQP_QTY { get; set; }

        public Int64 REMAIN_QTY { get; set; }

        public Int64 NG_QTY { get; set; }

        public Int64 ASSEMBLE_QTY { get; set; }

        public string PORTID { get; set; }

        public string PRODUCT_TYPE { get; set; }

        public string PRODUCT_KIND { get; set; }

        public string PRODUCTID { get; set; }

        public string M_STEP { get; set; }

        public string COMMENT { get; set; }

        public List<S1F6_InquireMaterialInfo_LIST_2> InquireMaterialInfo_LIST_2 { get; set; }

        public S1F6_InquireMaterialInfo_LIST_1()
        {
            this.M_TRAYID = string.Empty;
            this.M_BATCHID = string.Empty;
            this.M_KIND = 0;
            this.M_TYPE = string.Empty;
            this.M_MAKER = string.Empty;
            this.M_CODE = string.Empty;
            this.M_REVNO = string.Empty;
            this.TRAY_STATE = string.Empty;
            this.TOTAL_QTY = 0;
            this.USED_QTY = 0;
            this.INEQP_QTY = 0;
            this.REMAIN_QTY = 0;
            this.NG_QTY = 0;
            this.ASSEMBLE_QTY = 0;
            this.PORTID = string.Empty;
            this.PRODUCT_TYPE = string.Empty;
            this.PRODUCT_KIND = string.Empty;
            this.PRODUCTID = string.Empty;
            this.M_STEP = string.Empty;
            this.COMMENT = string.Empty;
            this.InquireMaterialInfo_LIST_2 = new List<S1F6_InquireMaterialInfo_LIST_2>();
        }
    }

    public class S1F6_InquireMaterialInfo_LIST_2
    {
        public string M_ID { get; set; }

        public string M_LOC { get; set; }

        public string M_SUBLOC { get; set; }

        public string M_SLOT { get; set; }

        public string D_CODE { get; set; }

        public string TAG { get; set; }

        public S1F6_InquireMaterialInfo_LIST_2()
        {
            this.M_ID = string.Empty;
            this.M_LOC = string.Empty;
            this.M_SUBLOC = string.Empty;
            this.M_SLOT = string.Empty;
            this.D_CODE = string.Empty;
            this.TAG = string.Empty;
        }
    }
}
