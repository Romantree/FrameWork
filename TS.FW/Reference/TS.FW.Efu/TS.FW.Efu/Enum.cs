using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Efu
{
    public enum EFU_TYPE
    {
        MIN_LFU_COUNT = 1,
        MAX_LFU_COUNT = 9,

        STX = 0x02,
        ETX = 0x03,
        REQ_MODE1 = 0x8A,
        REQ_MODE2 = 0x87,
        CHG_MODE1 = 0x89,
        CHG_MODE2 = 0x84,
        LV32 = 0x81,
        DPU = 0x9F,

        ALARM_NORMAL = 0x80,
        ALARM_OVER_CURRENT = 0xA0,
        ALARM_MOTOR_ALARM = 0xC0,
        ALARM_NO_CONNECT = 0x00,

        CHG_RESULT_SUCCESS = 189,
    }
}
