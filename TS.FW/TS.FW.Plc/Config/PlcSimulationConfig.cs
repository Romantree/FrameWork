using System.Collections.Generic;
using TS.FW.Plc.Dto;
using TS.FW.Plc.Dto.Packet;

namespace TS.FW.Plc.Config
{
    public class PlcSimulationConfig : IPlcConfigBase
    {
        public PlcSimulationConfig(int plcNo, string iniFilePath) : base(plcNo, PlcTypes.Simulation, iniFilePath)
        {
        }

        public PlcSimulationConfig(int plcNo) : this(plcNo, "")
        {

        }

        public override string ToString()
        {
            return base.ToString();
        }

        public static PlcSimulationConfig ToTestConfig(int plcNo, int count = 3000, int start = 0x1000)
        {
            var config = new PlcSimulationConfig(plcNo);

            var inBit = ToPlcBit(count, 0, "IN_SIM_BIT");
            var outBit = ToPlcBit(count, start, "OUT_SIM_BIT");
            var inWord = ToPlcWord(count, 0, "IN_SIM_WORD");
            var outWord = ToPlcWord(count, start, "OUT_SIM_WORD");

            config.Init(inBit, outBit, inWord, outWord);

            return config;
        }

        private static IEnumerable< PlcBit> ToPlcBit(int count, int start, string name)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new PlcBit(start + i, string.Format("{0}_{1}", name, (i + 1).ToString().PadLeft(3, '0')));
            }
        }

        private static IEnumerable<PlcWord> ToPlcWord(int count, int start, string name, PlcDeviceCode code = PlcDeviceCode.W_LINK_REGISTER)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new PlcWord(start + i, string.Format("{0}_{1}", name, (i + 1).ToString().PadLeft(3, '0')), 1, PlcWordDataType.Number, code);
            }
        }
    }
}
