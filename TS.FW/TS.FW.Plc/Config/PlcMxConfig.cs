using System.Runtime.Serialization;

namespace TS.FW.Plc.Config
{
    /// <summary>
    /// PLC MX 컴포넌트 통신설정 클래스
    /// </summary>
    [DataContract]
    public class PlcMxConfig : IPlcConfigBase
    {
        [DataMember]
        public string IpAddress { get; private set; }

        [DataMember]
        public byte StationNo { get; set; }

        public PlcMxConfig(int plcNo, string iniFilePath) : base(plcNo, PlcTypes.Mx, iniFilePath)
        {
            this.IpAddress = "127.0.0.1";
            this.StationNo = 0x01;
        }

        public PlcMxConfig(int plcNo) : this(plcNo, "")
        {

        }

        public void SetNetworkInfo(string ipAddress)
        {
            this.IpAddress = ipAddress;
        }

        public override string ToString()
        {
            return string.Format("{0} IP : {1}", base.ToString(), this.IpAddress);
        }
    }
}
