namespace TS.FW.Robot.RC100.Data
{
    public class RobotActionData
    {
        /// <summary>
        /// 1~200
        /// </summary>
        public int StageNo { get; set; }

        /// <summary>
        /// 1~25
        /// </summary>
        public int SlotNo { get; set; }

        public RobotArm Arm { get; set; }

        public RobotPoint Point { get; set; }

        public WaferSize WaferSize { get; set; }

        internal object[] ToData()
        {
            return new object[] { this.StageNo, this.SlotNo, (int)this.Arm, (int)this.WaferSize, (int)this.Point };
        }
    }
}
