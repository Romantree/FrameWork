using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.JF
{
    public enum MMC_STAT
    {
        MMC_EXCEPT = (-1), // EXCEPT
        MMC_OK = (0),  // No Err
        MMC_NOT_FOUND_ERR = (1),  // MMC Not Founded Err
        MMC_DRIVER_OPEN_ERR = (2),  // Device Driver Open Err
        MMC_FILE_PARA_ERR = (3),  // File Open Read Write Err
        MMC_ABS_TIME_OUT_ERR = (4),  // ABS Serial Comm Time Out
        MMC_MUTEX_OPEN_ERR = (5),  // Mutex Open Errz
        MMC_MUTEX_TIME_ERR = (6),  // Mutex Time Out
        MMC_INVALID_AXIS = (7),  // Invalid Axis Number
        MMC_INVALID_PARAMETER = (8),  // Invalid Parameter
        MMC_NOT_INITIALIZED = (9),  // Not Initialized
        MMC_INVALID_SPEED = (10), // Invalid Speed
        MMC_INVALID_AXES = (11), // Invalid Axes for Multi Axis
        MMC_AMP_OFF = (12), // Amp Off Status
        MMC_INVALID_ACCDEC = (13), // Invalid Acc Dec
        MMC_NOT_READY = (14), // Continuous Move Not Ready
        MMC_INVALID_PORT = (15), // Invalid Port Number
        MMC_ABS_DATA_ERR = (16), // ABS Serial Data Err
        MMC_ABS_RX_SIZE = (40),
        MAX_ErrMap_TABLE_N = (100)
    }

    public enum enDir
    {
        /// <summary>
        /// CW = Plus
        /// </summary>
        CW = 0, //CW
        /// <summary>
        /// CCW =  Minus
        /// </summary>
        CCW = 1  //CCW
    }

    public enum enStopAct
    {
        NO_ACTION = 0,
        EMG_STOP = 2,
        DEC_STOP = 1,
    }

    public enum enLevel
    {
        LOW = 0,
        HIGH = 1,
    }

    //In Out 관련 Enum
    public enum eSignal
    {
        ERROR = -1,

        OFF = 0,
        ON = 1,
    }
}
