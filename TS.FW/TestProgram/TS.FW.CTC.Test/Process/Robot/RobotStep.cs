using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.CTC.Test.Process.Robot
{
    public enum RobotStep
    {
        CHECK_PROC_ENTER,

        START,

        CHECK_SEQ_ENTER,

        CHECK_PROCESS_POLLING,

        START_SEQ,

        MOVE_ROBOT_ENTER,
        MOVE_ROBOT_POLLING,

        DOOR_OPEN_ENTER,
        DOOR_OPEN_POLLING,

        MOVE_ROBOT_ACTION_ENTER,
        MOVE_ROBOT_ACTION_POLLING,

        MOVE_ROBOT_EX_ACTION_ENTER,
        MOVE_ROBOT_EX_ACTION_POLLING,

        DOOR_CLOSE_ENTER,
        DOOR_CLOSE_POLLING,

        END_SEQ,

        MOVE_CHECK_SEQ,

        END,

        MAPPING_START,

        MAPPING_END,

        CLEAN_WAFER_START,
    }
}
