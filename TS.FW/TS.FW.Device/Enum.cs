using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Device
{
    public enum eDirection
    {
        Plus,
        Minus
    }

    public enum eSignal
    {
        ERROR = -1,

        OFF = 0,
        ON = 1,
    }

    public enum AXT_ENCODE_TYPE
    {
        TYPE_INCREMENTAL = 0,
        TYPE_ABSOLUTE = 1,
    }

    public enum DeviceType
    {
        N804,
        PM,
        A5N,
    }
}
