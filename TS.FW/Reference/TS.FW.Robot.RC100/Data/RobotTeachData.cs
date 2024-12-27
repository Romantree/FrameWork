namespace TS.FW.Robot.RC100.Data
{
    public class RobotTeachData
    {
        public int StageNo { get; set; }

        public RobotArm Arm { get; set; }

        public RobotPoint Point { get; set; }

        internal object[] ToData()
        {
            return new object[] { this.StageNo, (int)this.Arm, (int)this.Point };
        }
    }
}
