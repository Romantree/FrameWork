using S7.Net;
using System.Runtime.Serialization;

namespace TS.FW.Plc.Config
{
    /// <summary>
    /// PLC 이더넷 통신설정 클래스
    /// </summary>
    [DataContract]
    public class PlcSiemensConfig : IPlcConfigBase
    {
        [DataMember]
        public string IpAddress { get; private set; }

        [DataMember]
        public int Port { get; private set; }

        [DataMember]
        public CpuType Cpu { get; set; }
        
        [DataMember]
        public short Rack { get; set; }

        [DataMember]
        public short Slot { get; set; }

        [DataMember]
        public int DataBlock { get; set; }

        public PlcSiemensConfig(int plcNo, string iniFilePath) : base(plcNo, PlcTypes.Siemens, iniFilePath)
        {
            this.IpAddress = "127.0.0.1";
            this.Port = 102;
            this.Cpu = CpuType.S71200;
            this.Rack = 0;
            this.Slot = 0;
            this.DataBlock = 70;
        }

        public PlcSiemensConfig(int plcNo) : this(plcNo, "")
        {

        }

        public void SetNetworkInfo(string ipAddress, short rack, short slot, int dataBlock)
        {
            this.IpAddress = ipAddress;
            this.Rack = rack;
            this.Slot = slot;
            this.DataBlock = dataBlock;
        }

        public override string ToString()
        {
            return string.Format("{0} IP : {1}", base.ToString(), this.IpAddress);
        }
    }
}
