using TS.FW.CTC.Test.Dto;

namespace TS.FW.CTC.Test.Managers.Schedule
{
    public class ScheduleItem
    {
        public int No { get; set; }

        public int RobotNo { get; set; }

        public int PortNo { get; set; }

        public LotRecipe Recipe { get; set; }

        public ScheduleState State { get; set; } = ScheduleState.None;
    }
}
