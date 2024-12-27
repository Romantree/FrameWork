using System.Runtime.Serialization;

namespace TS.FW.Plc.Config
{
    [DataContract]
    public class PlcABConfig : IPlcConfigBase
    {
        [DataMember]
        public string OpcName { get; private set; }

        public PlcABConfig(int plcNo, string iniFilePath) : base(plcNo, PlcTypes.AB, iniFilePath)
        {
            OpcName = "aa";
        }

        public void SetNetworkInfo(string opcName)
        {
            OpcName = opcName;
        }

        public override string ToString()
        {
            return string.Format("{0} OpcName : {1}", base.ToString(), this.OpcName);
        }
    }
}
