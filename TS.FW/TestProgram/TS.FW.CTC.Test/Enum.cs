using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.CTC.Test
{
    public enum UnitRecipeType
    {
        Align,
        Centering,
        Cotter,
        Imprint
    }

    public enum StepResult
    {
        Next,
        Pending,
        Change,
        Alarm,
        Error,
        Finish,
        Not_Found_Step,
        Pause,
        Stop,
    }

    public enum RobotAction
    {
        GET,
        PUT,
    }

    public enum RobotMove
    {
        None,
        Port,
        Align,
        Centering,
        Cotter,
        Imprint
    }

    public enum RobotArmType
    {
        TOP,
        BOTTOM
    }

    public enum RobotHeadMode
    {
        TwoHeand,
        OneHeand,
        FixHeand
    }

    public enum ScheduleState
    {
        None,

        MappingStart,
        MappingEnd,

        Run,
        End,
    }
}
