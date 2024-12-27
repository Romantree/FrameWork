using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.XCom.SDC.OLED.V227
{
    public enum eOnlAck
    {
        REQUEST_ACCEPTED = 0,
        EQUIPMENT_STATE_DONE,
        IS_NOT_MODULE_ID,
        IS_NOT_MCMC,
    }

    public enum eEAC
    {
        ACK,
        BUSY,
        OUT_OF_RANGE,
    }

    public enum eTEAC
    {
        ACK,
        NOT_EXIST,
        BUSY,
        OUT_OF_RANGE,
    }

    public enum eMIACK
    {
        NO_ERROR,
        IS_NOT_MODULE_ID,
    }

    public enum eTIACK
    {
        OK,
        ERROR,
        IS_NOT_MODULE_ID,
        TRID_IS_NOT_MATCH,
    }

    public enum eACKC2
    {
        OK,
        ERROR,
    }

    public enum eHCACK
    {
        ACK = 0,
        IS_NOT_COMMAND_ERROR = 1,
        PARAMETER_INVALID_EEOR = 3,
        CONTROL_STATE_LOCAL = 7,
        IS_NOT_EXIST_PPID = 10,
        IS_NOT_AVAILLABLE_PPID = 15,
    }
}
