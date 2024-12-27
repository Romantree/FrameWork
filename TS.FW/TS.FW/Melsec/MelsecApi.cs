using System;
using System.Runtime.InteropServices;

namespace TS.FW.Melsec
{
    public class MelsecApi
    {
        [DllImport("MDFUNC32.dll")]
        public static extern short mdOpen(short Chan, short Mode, ref Int32 Path);
        [DllImport("MDFUNC32.dll")]
        public static extern short mdClose(Int32 Path);
        [DllImport("MDFUNC32.dll")]
        public static extern short mdDevSetEx(Int32 path, Int32 Netno, Int32 Stno, Int32 Devtyp, Int32 devno);
        [DllImport("MDFUNC32.dll")]
        public static extern Int32 mdDevRstEx(Int32 Path, Int32 Netno, Int32 Stno, Int32 Devtyp, Int32 devno);
        [DllImport("MDFUNC32.dll")]
        public static extern short mdSendEx(Int32 Path, Int32 Netno, Int32 Stno, Int32 Devtyp, Int32 devno, ref Int32 size, short[] buf);
        [DllImport("MDFUNC32.dll")]
        public static extern Int32 mdReceiveEx(Int32 path, Int32 Netno, Int32 Stno, Int32 Devtyp, Int32 devno, ref Int32 size, short[] buf);
    }

    public enum MDevType
    {
        //LX = 1,
        //LY = 2,
        //SM = 5,
        //SD = 14,
        LB = 23,
        LW = 24,
        //RWw = 36,
        //RWr = 37,
        //BufferMemory = 50,
        //RECV = 101,
    }

    public enum MErrorCode
    {
        PATH_ERROR = -1,
        START_DEVICE_NUMBER_ERROR = -2,
        DEVICE_TYPE_ERROR = -3,
        SIZE_ERROR = -5,
        NUMBER_OF_BLOCKS_ERROR = -6,
        CHANNEL_NUMBER_ERROR = -8,


        BLOCK_NUMBER_ERROR = -12,
        WRITE_PROTECT_ERROR = -13,
        NETWORK_OR_STATION_NUMBER_ERROR = -16,
        ALL_STATION_OR_GROUP_NUMBER_ERROR = -17,
        REMOTE_COMMAND_CODE_ERROR = -18,
        FUNCTION_OUT_OF_RANGE = -19,

        DLL_LOAD_ERROR = -31,
        RESOURCE_TIME_OUT_ERROR = -32,
        INCORRECT_ACCESS_TARGET_ERROR = -33,
        REGISTRY_ACCESS_ERROR_1 = -34,
        REGISTRY_ACCESS_ERROR_2 = -35,
        REGISTRY_ACCESS_ERROR_3 = -36,
        INIT_SETTING_ERROR = -37,
        CLOSE_ERROR = -42,
        ROM_ERROR = -43,
        NUMBER_OF_EVENTS_ERROR = -61,
        EVENT_NUMBER_ERROR = -62,
        EVENT_NUMBER_OVERLAPPED_REGISTRATION_ERROR = -63,
        TIMEOUT_TIME_ERROR = -64,
        EVENT_WAIT_TIME_OUT_ERROR = -65,
        EVENT_INITIALIZATION_ERROR = -66,
        NO_EVENT_SETTING_ERROR = -67,
        EVENT_OVERLAPPED_OCCURRENCE_ERROR = -70,

        TRANSIENT_DATA_TARGET_STATION_NUMBER_ERROR = -2174,

        // -257 to -4096 Errors detected in the MELSECNET/H and MELSECNET/10 network system

        SUCCESS = 0,

        DRIVER_NOT_STARTED = 1,
        TIME_OUT_ERROR = 2,

        CHANNEL_OPENED_ERROR = 66,
        UNSUPPORTED_FUNCTION_EXECUTION_ERROR = 69,

        STATION_NUMBER_ERROR = 70,
        NO_RECEPTION_DATA_ERROR = 71,
        MEMORY_RESERVATION_ERROR = 77,

        SEND_RECV_CHANNEL_NUMBER_ERROR = 85,

        BOARD_RESOURCE_ERROR = 100,
        ROUTING_PARAMETER_ERROR = 101,
        BOARD_DRIVER_REQUEST_ERROR = 102,
        BOARD_DRIVER_RESPONSE_ERROR = 103,

        PARAMETER_ERROR = 133,

        // 4069 to 16383 Melsec data link library internal error
        // 16384 to 20479 Error detected by the access target CPU

        DEVICE_TYPE_DOES_NOT_EXIST = 16432,
        DEVICE_NUMBER_IS_OUT_OF_RANGE = 16433,
        REQUEST_DATA_ERROR = 16512,
        LILNK_RELATED_ERROR_1 = 18944,
        LILNK_RELATED_ERROR_2 = 18945,

    }
}
