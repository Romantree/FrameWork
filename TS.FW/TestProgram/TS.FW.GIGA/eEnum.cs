using System;
using System.Collections.Generic;
using TS.FW.Dac.Alarm;

namespace TS.FW.GIGA
{
    public static class EnumHelper
    {
        public static List<string> SerialPort { get; set; } = new List<string>();

        public static List<AlarmLevel> AlarmLevel { get; set; } = new List<AlarmLevel>();

        public static List<eAxis> Axis { get; set; } = new List<eAxis>();

        static EnumHelper()
        {
            foreach (var item in System.IO.Ports.SerialPort.GetPortNames()) SerialPort.Add(item);

            InitEnum(AlarmLevel);
            InitEnum(Axis);
        }

        private static void InitEnum<T>(List<T> list)
        {
            try
            {
                foreach (T item in Enum.GetValues(typeof(T)))
                {
                    list.Add(item);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(EnumHelper), ex);
            }
        }

        public static CyState ToRev(this CyState state) => state == CyState.UP ? CyState.DOWN : CyState.UP;
    }

    public enum eAxis
    {
        UVW_01,
        UVW_02,
        UVW_03,
        UVW_04,

        CAMERA_T_X_01,
        CAMERA_T_X_02,
        CAMERA_T_Y_01,
        CAMERA_T_Y_02,
        CAMERA_T_Z_01,
        CAMERA_T_Z_02,

        CAMERA_B_X_01,
        CAMERA_B_X_02,
        CAMERA_B_Y_01,
        CAMERA_B_Y_02,
        CAMERA_B_Z_01,
        CAMERA_B_Z_02,

        GAP_PRESS_LEFT,
        GAP_PRESS_RIGHT,

        FILM_BALANCE_01,
        FILM_BALANCE_02,

        FILM_GRIP_01,
        FILM_GRIP_02,

        STAGE_X,
        STAGE_X_S,

        UNWINDER,
        REWINDER,
        COVER_FILM,
        NIP_ROLL,
        DE_NIP_ROLL,
    }

    public enum CyUnit
    {

    }

    public enum CyState
    {
        UP,
        DOWN,
    }

    public enum VacuumSetting
    {
        Vacuum,
        Vent,
        Off,
    }

    public enum VacuumStatus
    {
        ATM,

        Vacuum,
        Vent,

        VacuumProcess,
        VentProcess,

        Error,
    }

    public enum EpcUnit
    {
        All,
        Unwinder,
        Rewinder,
    }

    public enum EpcMode
    {
        None,
        Auto,
        Manual,
        Center,
    }

    public enum EpcState
    {
        CenterMove,
        Deviation,
        Overload,
        ActLimit,
        AutoMode,
        ManualMode,
    }

    public enum TensionUnit
    {
        All,
        Unwinder,
        Rewinder,
        CoverFilm,
    }

    public enum eIOType
    {
        IN,
        OUT,
    }

    public enum eSignalType
    {
        A,
        B,
    }
}
