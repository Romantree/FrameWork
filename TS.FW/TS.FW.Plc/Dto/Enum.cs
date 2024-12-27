namespace TS.FW.Plc.Dto.Packet
{
    public enum PlcCommandType
    {
        ReadBit = 0x04010001,
        ReadWord = 0x04010000,

        WriteBit = 0x14010001,
        WriteWord = 0x14010000,

        RandomRead = 0x04030000,

        RandomWriteBit = 0x14020001,
        RandomWriteWord = 0x14020000,

        // TODO : 사용되지 않음..
        //MonitorAdd = 0x08010000,
        //Monitor = 0x08020000,

        LS_READ_REQUEST = 0x0054,

        LS_READ_RESPONSE = 0X0055,

        LS_WRITE_REQUEST = 0X0058,

        LS_WRITE_RESPONSE = 0X0059,
    }


    public enum PlcDeviceCode
    {
        /// <summary>
        /// B
        ///  - MX Component 지정 유형 : 16진수
        /// </summary>
        B_LINK_RELAY = 0xA0,

        /// <summary>
        /// W
        ///  - MX Component 지정 유형 : 16진수
        /// </summary>
        W_LINK_REGISTER = 0xB4,

        /// <summary>
        /// R
        ///  - MX Component 지정 유형 : 10진수
        /// </summary>
        R_FILE_ACCESS_REGISTER = 0xAF,

        /// <summary>
        /// M
        ///  - MX Component 지정 유형 : 10진수
        /// </summary>
        M_INSIDE_RELAY = 0x90,

        /* TODO : 기존코드에서는 사용되지 않음. 

        /// <summary>
        /// X
        /// </summary>
        X_INPUT_RELAY = 0x9C,

        /// <summary>
        /// Y
        /// </summary>
        Y_OUTPUT_RELAY = 0x9D,

        /// <summary>
        /// L
        /// </summary>
        L_LATCH_RELAY = 0x92,

        /// <summary>
        /// F
        /// </summary>
        F_ANNUNCIATOR = 0x93,

        /// <summary>
        /// V
        /// </summary>
        V_EDGE_RELAY = 0x94,

        /// <summary>
        /// D
        /// </summary>
        D_DATA_REGISTER = 0xA8,

        /// <summary>
        /// S 
        /// </summary>
        S_SETP_RELAY = 0x98,

        /// <summary>
        /// Z
        /// </summary>
        Z_INDEX_REGISTER = 0xCC,

        */

        LS_BIT_DATA_CODE = 0x0000,

        LS_BYTE_DATA_CODE = 0x0001,

        LS_WORD_DATA_CODE = 0x0002,

        LS_DWORD_DATA_CODE = 0x0003,

        LS_LWORD_DATA_CODE = 0x0004,

        LS_CONTINUE_DATA_CODE = 0x0014,
    }

    public static class PlcDeviceCodeEx
    {
        public static string ToCodeString(this PlcDeviceCode code)
        {
            return code.ToString().Substring(0, 1);
        }
    }
}
