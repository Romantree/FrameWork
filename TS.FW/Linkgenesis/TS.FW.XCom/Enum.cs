using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.XCom
{
    /// <summary>
    /// XCOM 통신 연결 유형을 정의
    /// </summary>
    public enum SECS_STATE
    {
        UNKNOWN = 0,
        NOT_CONNECTED = 101,
        NOT_SELECTED = 102,
        SELECTED = 103,
        ALMXC_T3 = 203,
        ALMXC_START = 215,
        ALMXC_PROTECTION = 216,
        ALMXC_INVALID_MSG = 217,
        ALMXC_UNKNOWN_DEV_ID = 221,
        ALMXC_UNKNOWN_STREAM = 222,
        ALMXC_UNKNOWN_FUNC = 223,
    }

    public enum eXComData
    {
        LIST = 0x00,

        BINARY = 0x08,
        BOOL = 0x09,

        ASCII = 0x10,
        JIS8 = 0x11,

        INT1 = 0x19,
        INT2 = 0x1A,
        INT4 = 0x1C,
        INT8 = 0x18,

        UINT1 = 0x29,
        UINT2 = 0x2A,
        UINT4 = 0x2C,
        UINT8 = 0x28,

        FLOAT4 = 0x24,
        FLOAT8 = 0x20,
    }
}
