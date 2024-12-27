using System;

namespace TS.FW.Robot.RC100
{

    public enum RobotCmdType
    {
        /// <summary>
        /// 상태 모니터링
        /// </summary>
        STATE,

        /// <summary>
        /// 프로그램 초기화 및 실행
        /// </summary>
        INITIAL,

        /// <summary>
        /// 프로그램 정지
        /// </summary>
        STOP,

        /// <summary>
        /// 알람 리셋
        /// </summary>
        RESET,

        /// <summary>
        /// 모터전원 ON
        /// </summary>
        SERVOON,

        /// <summary>
        /// 모터전원 OFF
        /// </summary>
        SERVOOF,

        /// <summary>
        /// 프로그램 일시정지
        /// </summary>
        PAUSE,

        /// <summary>
        /// 프로그램 재시작
        /// </summary>
        RESUM,

        /// <summary>
        /// 로봇 Home 위치로 이동
        /// </summary>
        HOME,

        /// <summary>
        /// 로봇 Arm Fold 위치로 이동
        /// </summary>
        ARMFOLD,

        /// <summary>
        /// 워크 취출전 대기위치로 이동
        /// </summary>
        FGREADY,

        /// <summary>
        /// 워크 취출동작
        /// </summary>
        GETFROM,

        /// <summary>
        /// 워크 수납전 대기위치로 이동
        /// </summary>
        FPREADY,

        /// <summary>
        /// 워크 수납동작
        /// </summary>
        PUTINTO,

        /// <summary>
        /// 워크 취출동작으로 티칭위치까지 이동
        /// </summary>
        GETTPOS,

        /// <summary>
        /// Z 축 하강 및 핸드 복귀이동
        /// </summary>
        PUTAFLD,

        /// <summary>
        /// 진공 켜기, Clamp
        /// </summary>
        VACUMON,

        /// <summary>
        /// 진공 끄기, Unclamp
        /// </summary>
        VACUMOF,

        /// <summary>
        /// 절대위치 이동
        /// </summary>
        MOVEABS,

        /// <summary>
        /// 상대위치 이동
        /// </summary>
        MOVEREL,

        /// <summary>
        /// 로봇의 현재위치를 반환
        /// </summary>
        GETCPOS,

        /// <summary>
        /// 티칭 좌표값을 반환
        /// </summary>
        GETLPOS,

        /// <summary>
        /// 티칭 파라메터를 반환
        /// </summary>
        GETLPAR,

        /// <summary>
        /// 로봇 이동속도를 반환
        /// </summary>
        GETTSPD,

        /// <summary>
        /// 입력한 좌표값을 티칭
        /// </summary>
        SETCORD,

        /// <summary>
        /// 로봇의 현재위치를 티칭
        /// </summary>
        SETLPOS,

        /// <summary>
        /// 티칭 파라메터를 설정
        /// </summary>
        SETLPAR,

        /// <summary>
        /// 로봇 이동 속도를 설정
        /// </summary>
        SETTSPD,

        /// <summary>
        /// 워크 확인
        /// </summary>
        MAPSCAN,

        /// <summary>
        /// 워크두께 확인
        /// </summary>
        GETMDAT,

        /// <summary>
        /// 맵핑 티칭 좌표값 반환
        /// </summary>
        GETMPOS,

        /// <summary>
        /// 맵핑 티칭 파라메터 반환
        /// </summary>
        GETMPAR,
    }

    public enum RobotError
    {
        NONE = 0,

        NOT_REMOTE_MODE = 1,
        CMD_EXEC_ERROR = 7,

        INVALID_COMMAND_NUMBER = 301,
        INVALID_STAGE_NUMBER = 302,
        INVALID_SLOT_NUMBER = 303,
        INVALID_ARM_NUMBER = 304,
        INVALID_SIZE_NUMBER = 305,

        UNDETECT_WORK_DURING_GET_R1 = 405,
        UNDETECT_WORK_DURING_GET_R2 = 406,

        UNDETECT_WORK_BEFORE_PUT_R1 = 403,
        UNDETECT_WORK_BEFORE_PUT_R2 = 404,

        DETECT_WORK_DURING_PUT_R1 = 411,
        DETECT_WORK_DURING_PUT_R2 = 412,

        TIME_OUT_CHECK_VACUUM_ON_R1 = 413,
        TIME_OUT_CHECK_VACUUM_ON_R2 = 414,

        TIME_OUT_CHECK_VACUUM_OFF_R1 = 415,
        TIME_OUT_CHECK_VACUUM_OFF_R2 = 416,

        INVALID_AXIS_NUMBER = 435,
        SWLIMIT_RANGE_OVER = 429,
        INVALID_MAX_SLOT_VALUE = 422,

        INVALID_SPEED_VALUE = 426,
        INVALID_UP_DOWN_PITCH_VALUE = 427,
        INVALID_SLOT_PITCH_VALUE = 428,

        INVALID_SPEED_MODE = 424,

        //INVALID_START_POSITION = 317,
        //DETECT_WORK_BEFORE_GET_R1 = 401,
        //DETECT_WORK_BEFORE_GET_R2 = 402,
        //INVAILD_CONDITION_FOR_EXCHANE = 449,
    }

    public enum WaferSize
    {
        Inch_8 = 1,
        Inch_12,
    }
    public enum WaferState
    {
        None,
        Normal,
        Double,
        Cross,
    }

    public enum RobotArm
    {
        None = 0,
        R1,
        R2,
    }

    public enum RobotPoint
    {
        None = 0,
        P1,
        P2,
        P3,
        P4,
        P5,
    }

    public enum RobotSpeedMode
    {
        None = 0,
        Home,
        Stage,
        UpDown,
        Arm,
    }

    [Flags]
    public enum RobotStateCheck
    {
        None = 0x00,

        Alarm = 0x1,
        ProgramRun = 0x2,
        RobotReady = 0x4,
        CmdReady = 0x8,

        ArmDetectOnR1 = 0x10,
        ArmDetectOffR1 = 0x20,
        ArmDetectOnR2 = 0x40,
        ArmDetectOffR2 = 0x80,

        UserRobotMoveCmd = RobotStateCheck.Alarm | RobotStateCheck.ProgramRun | RobotStateCheck.RobotReady | RobotStateCheck.CmdReady,
    }

}
