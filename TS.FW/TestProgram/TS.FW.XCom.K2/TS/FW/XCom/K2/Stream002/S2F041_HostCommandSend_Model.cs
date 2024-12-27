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
    public class S2F41_HostCommandSend_Model : IXComModel
    {
        private const int CONST_JOB_PROCESS_START = 1;
        private const int CONST_JOB_PROCESS_CANCEL= 2;
        private const int CONST_JOB_PROCESS_ABORT = 3;
        private const int CONST_JOB_PROCESS_START_PERMISSION= 30;

        private const int CONST_RELOAD_CASSETTE  = 36;


        public int RCMD { get; private set; }

        #region JOB PROCESS COMMAND
        public string JOBID { get; set; } = string.Empty;
        public string IPID { get; set; } = string.Empty;
        public string ICID { get; set; } = string.Empty;
        public string OPID { get; set; } = string.Empty;
        public string OCID { get; set; } = string.Empty;
        public string SLOTINFO { get; set; } = string.Empty;
        public string ORDER { get; set; } = string.Empty;
        public List<short> SLOTs { get; set; }
        #endregion

        public List<string> PORTs { get; set; }
        public List<string> MODULEs { get; set; }

        public string MODULEID { get; set; } = string.Empty;
        public string H_GLASSID { get; set; } = string.Empty;


        public short HCACK { get; set; } = 0;
        public Dictionary<string, short> CPACK { get; set; }


        public S2F41_HostCommandSend_Model()
        {
            this.Stream = 2;
            this.Function = 41;
            this.FullName = "HostCommandSend";
            this.Name = "HostCommandSend";
            this.SubName = "";
            this.IsUnDefined = true;

            SLOTs = new List<short>();
            PORTs = new List<string>();
            MODULEs = new List<string>();
            CPACK = new Dictionary<string, short>();
        }

        public override void RecvData(IXComData data)
        {
            var depth1 = data.List;
            RCMD = depth1.UINT2;

            switch(RCMD)
            {
                //JOB PROCESS COMMAND
                case CONST_JOB_PROCESS_START:
                case CONST_JOB_PROCESS_CANCEL:
                case CONST_JOB_PROCESS_ABORT:
                case CONST_JOB_PROCESS_START_PERMISSION:
                    {
                        var depth2 = depth1.List;
                        {
                            var list1 = depth2.List;
                            {
                                JOBID = list1.ASCII;
                                JOBID = list1.ASCII;
                            }
                            var list2 = depth2.List;
                            {
                                IPID = list2.ASCII;
                                IPID = list2.ASCII;
                            }
                            var list3 = depth2.List;
                            {
                                ICID = list3.ASCII;
                                ICID = list3.ASCII;
                            }
                            var list4 = depth2.List;
                            {
                                OPID = list4.ASCII;
                                OPID = list4.ASCII;
                            }
                            var list5 = depth2.List;
                            {
                                OCID = list5.ASCII;
                                OCID = list5.ASCII;
                            }
                            var list6 = depth2.List;
                            {
                                SLOTINFO = list6.ASCII;
                                SLOTINFO = list6.ASCII;
                            }
                            var list7 = depth2.List;
                            {
                                ORDER = list7.ASCII;
                                foreach(var item in list7.List.Values)
                                {
                                    SLOTs.Add(item.UINT1);
                                }
                            }
                        }
                    }
                    break;

                //PORT COMMAND
                case CONST_RELOAD_CASSETTE:
                    {
                        PORTs.Clear();
                        var portcnt = depth1.List;
                        foreach (var p in portcnt.Values)
                        {
                            var list = p.List;
                            var key = list.ASCII;
                            var value = list.ASCII;
                            PORTs.Add(value);
                        }
                    }
                    break;
                //EQUIPMENT COMMAND
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                    {
                        MODULEs.Clear();
                        var modulecnt = depth1.List;
                        foreach (var m in modulecnt.Values)
                        {
                            var list = m.List;
                            var key = list.ASCII;
                            var value = list.ASCII;
                            MODULEs.Add(value);
                        }
                    }
                    break;

                //GLASS PROCESS COMMAND
                case 11:
                case 12:
                case 13:
                case 14:
                    {
                        SLOTs.Clear();

                        var depth2 = depth1.List;
                        var modulelist = depth2.List;
                        var tmpstr1 = modulelist.ASCII;
                        MODULEID = modulelist.ASCII;
                        var glslist = depth2.List;
                        var tmpstr2 = glslist.ASCII;
                        H_GLASSID = glslist.ASCII;

                        var slotlist = depth2.List;
                        var tmpstr3 = slotlist.ASCII;
                        foreach(var item in slotlist.List.Values)
                        {
                            SLOTs.Add(item.UINT1);
                        }
                    }
                    break;

            }

        }

        private void ProcessCmdReply(XCOM xcom, int msgId)
        {
            xcom.SetListItem(msgId, 3);

            xcom.SetUINT1(msgId, RCMD);
            xcom.SetUINT1(msgId, HCACK);
            xcom.SetListItem(msgId, 7);
            {
                xcom.SetListItem(msgId, 3);
                {
                    xcom.SetString(msgId, "JOBID", 10);
                    xcom.SetString(msgId, JOBID, 16);
                    var cpack = CPACK.ContainsKey("JOBID") ? CPACK["JOBID"] : 0;
                    xcom.SetUINT1(msgId, cpack);
                }
                xcom.SetListItem(msgId, 3);
                {
                    xcom.SetString(msgId, "IPID", 10);
                    xcom.SetString(msgId, IPID, 4);
                    var cpack = CPACK.ContainsKey("IPID") ? CPACK["IPID"] : 0;
                    xcom.SetUINT1(msgId, cpack);
                }

                xcom.SetListItem(msgId, 3);
                {
                    xcom.SetString(msgId, "ICID", 10);
                    xcom.SetString(msgId, ICID, 4);
                    var cpack = CPACK.ContainsKey("ICID") ? CPACK["ICID"] : 0;
                    xcom.SetUINT1(msgId, cpack);
                }
                xcom.SetListItem(msgId, 3);
                {
                    xcom.SetString(msgId, "OPID", 10);
                    xcom.SetString(msgId, OPID, 4);
                    var cpack = CPACK.ContainsKey("OPID") ? CPACK["OPID"] : 0;
                    xcom.SetUINT1(msgId, cpack);
                }
                xcom.SetListItem(msgId, 3);
                {
                    xcom.SetString(msgId, "OCID", 10);
                    xcom.SetString(msgId, OCID, 12);
                    var cpack = CPACK.ContainsKey("OCID") ? CPACK["OCID"] : 0;
                    xcom.SetUINT1(msgId, cpack);
                }
                xcom.SetListItem(msgId, 3);
                {
                    xcom.SetString(msgId, "SLOTINFO", 10);
                    xcom.SetString(msgId, SLOTINFO, 56);
                    var cpack = CPACK.ContainsKey("SLOTINFO") ? CPACK["SLOTINFO"] : 0;
                    xcom.SetUINT1(msgId, cpack);
                }
                xcom.SetListItem(msgId, 3);
                {
                    xcom.SetString(msgId, "ORDER", 10);

                    xcom.SetListItem(msgId, SLOTs.Count);
                    foreach (var s in SLOTs)
                        xcom.SetUINT1(msgId, s);

                    var cpack = CPACK.ContainsKey("ORDER") ? CPACK["ORDER"] : 0;
                    xcom.SetUINT1(msgId, cpack);
                }
            }
        }

        private void PortCmdReply(XCOM xcom, int msgId)
        {
            xcom.SetListItem(msgId, 3);

            xcom.SetUINT1(msgId, RCMD);
            xcom.SetUINT1(msgId, HCACK);
            xcom.SetListItem(msgId, PORTs.Count);
            foreach(var p in PORTs)
            {
                xcom.SetListItem(msgId, 3);
                {
                    xcom.SetString(msgId, "PORTID", 10);
                    xcom.SetString(msgId, p.PadLeft(4, '0'), 4);
                    var cpack = CPACK.ContainsKey(p) ? CPACK[p] : 0; //todo : 확인 필요 구문
                    xcom.SetUINT1(msgId, cpack);
                }
            }
        }

        private void ModuleCmdReply(XCOM xcom, int msgId)
        {
            xcom.SetListItem(msgId, 3);

            xcom.SetUINT1(msgId, RCMD);
            xcom.SetUINT1(msgId, HCACK);
            xcom.SetListItem(msgId, PORTs.Count);
            foreach (var m in MODULEs)
            {
                xcom.SetListItem(msgId, 3);
                {
                    xcom.SetString(msgId, "MODULEID", 10);
                    xcom.SetString(msgId, m, 28);
                    var cpack = CPACK.ContainsKey(m) ? CPACK[m] : 0; //todo : 확인 필요 구문
                    xcom.SetUINT1(msgId, cpack);
                }
            }
        }

        private void GlassCmdReply(XCOM xcom, int msgId)
        {
            xcom.SetListItem(msgId, 3);

            xcom.SetUINT1(msgId, RCMD);
            xcom.SetUINT1(msgId, HCACK);
            xcom.SetListItem(msgId, 3);
            {
                xcom.SetListItem(msgId, 3);
                {
                    xcom.SetString(msgId, "MODULEID", 10);
                    xcom.SetString(msgId, MODULEID, 28);
                    var cpack = CPACK.ContainsKey("MODULEID") ? CPACK["MODULEID"] : 0; //todo : 확인 필요 구문
                    xcom.SetUINT1(msgId, cpack);
                }
                xcom.SetListItem(msgId, 3);
                {
                    xcom.SetString(msgId, "H_GLASSID", 10);
                    xcom.SetString(msgId, H_GLASSID, 16);
                    var cpack = CPACK.ContainsKey("H_GLASSID") ? CPACK["H_GLASSID"] : 0; //todo : 확인 필요 구문
                    xcom.SetUINT1(msgId, cpack);
                }
                xcom.SetListItem(msgId, 3);
                {
                    xcom.SetString(msgId, "SLOTNO", 10);
                    xcom.SetListItem(msgId, SLOTs.Count);
                    foreach (var s in SLOTs)
                    {
                        xcom.SetUINT1(msgId, s);
                    }
                    var cpack = CPACK.ContainsKey("SLOTNO") ? CPACK["SLOTNO"] : 0; //todo : 확인 필요 구문
                    xcom.SetUINT1(msgId, cpack);
                }
            }
           
        }

        public override void SendData(XCOM xcom, int msgId)
        {
            switch(RCMD)
            {
                //JOB PROCESS COMMAND
                case CONST_JOB_PROCESS_START:
                case CONST_JOB_PROCESS_CANCEL:
                case CONST_JOB_PROCESS_ABORT:
                case CONST_JOB_PROCESS_START_PERMISSION:
                    this.ProcessCmdReply(xcom, msgId);
                    //todo : Send 명령은 따로 안해줘도 되나?
                    break;
                //PORT COMMAND
                case CONST_RELOAD_CASSETTE:
                    PortCmdReply(xcom, msgId);
                    break;
                //EQUIPMENT COMMAND
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                    ModuleCmdReply(xcom, msgId);
                    break;
                //GLASS PROCESS COMMAND
                case 11:
                case 12:
                case 13:
                case 14:
                    GlassCmdReply(xcom, msgId);
                    break;
            }

        }
    }
}
