using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.XCom.SDC.OLED.V227
{
    public enum eMCMD
    {
        OFFLINE = 1,
        LOCAL = 2,
        REMOTE = 3,
    }

    public enum eModuleState
    {
        NORMAL = 1,
        FAULT,
        PM,
    }

    public enum eProcState
    {
        IDLE = 1,
        SETUP,
        EXECUTING,
        PAUSE,
        DISABLED,
    }

    public enum eBywho
    {
        Host = 1,
        Operator,
        Equipment,
    }

    public enum ePortState
    {
        Empty = 0,
        Idel,
        Wait,
        Reserve,
        Busy,
        Complete,
        Abort,
        Cancel,
        Pause,
        Disabled,
    }

    public enum ePortType
    {
        BothPort = 0,
        InputPort,
        OutputPort,
        BufferPort,
        TeachingPort,
    }

    public enum eSortType
    {
        DumpFill = 0,
        Dump,
        LotID,
        BatchID,
        JobID,
        ProductKind,
        PrerunFlag,
        WorkState,
        WorkState_FlipState,
        ProductID_ProductType,
        ProductID,
        ProductType,
        ProductType_ProductKind,
        ProductID_ProductType_RunspecID
    }

    public enum eCassetteType
    {
        FullCassette = 1,
        QuarterCassette,
        CallCassette,
        MaskCassette,
        MaskCase,
        WireCassette,
        BoxCassette,
        TrayCassette,
        SmallBox,
        LargeBox,
        FilmRoll,
        FilmCassette,
    }

    public enum eCassetteBatchOrder
    {
        Start = 1,
        Normal,
        End,
    }

    public enum eGlassState
    {
        Idel = 1,
        SelectedToProcess,
        Processing,
        Done,
        Aborted,
        Canceled,
    }

    public enum eMaterialState
    {
        Idel = 1,
        SelectedToProcess,
        Processing,
        Done,
        Aborted,
        Canceled,
    }

    public enum eStageState
    {
        EMPTY,
        IDLE,
        WAIT,
        RESERVE,
        BUSY,
        COMPLETE,
        CANCEL,
        DISABLE,
    }

    public enum eJudgement
    {
        OK,
        NG,
        RJ,
        RW,
        SK,
    }

    public enum ePriority
    {
        High =1,
        Middle,
        Low,
    }

    public enum eSvDataType
    {
        BOOL,
        STRING,
        INTEGER,
        FLOAT,
        LONG,
    }
}
