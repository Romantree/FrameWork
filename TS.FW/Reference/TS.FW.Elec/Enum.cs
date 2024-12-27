using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Elec
{

    [Flags]
    public enum ElectMeterState
    {
        Normal = 0x0000,
        DI_Detect = 0x0001, // DI 감지
        DO_Detect = 0x0100, // DO 출력
    }

    [Flags]
    public enum ElectMeterAlarm
    {
        Normal = 0x0000,
        Temperature = 0x0001, // 온도
        Leakage_Charge = 0x0010, // 누설전류
    }
}
