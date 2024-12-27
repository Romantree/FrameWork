using System.Runtime.Serialization;

namespace TS.FW.Plc.Config
{
    /// <summary>
    /// PLC 이더넷 통신설정 클래스
    /// </summary>
    [DataContract]
    public class PlcLSConfig : IPlcConfigBase
    {
        [DataMember]
        public string IpAddress { get; private set; }

        [DataMember]
        public int Port { get; private set; }

        public PlcLSConfig(int plcNo, string iniFilePath) : base(plcNo, PlcTypes.LS, iniFilePath)
        {
            this.IpAddress = "127.0.0.1";
            this.Port = 2004;
        }

        public PlcLSConfig(int plcNo) : this(plcNo, "")
        {

        }

        public void SetNetworkInfo(string ipAddress, int port)
        {
            this.IpAddress = ipAddress;
            this.Port = port;
        }

        public override string ToString()
        {
            return string.Format("{0} IP : {1}  Port : {2}", base.ToString(), this.IpAddress, this.Port);
        }
    }
}
