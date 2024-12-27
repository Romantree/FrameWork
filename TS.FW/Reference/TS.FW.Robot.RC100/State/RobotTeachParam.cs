using System;

namespace TS.FW.Robot.RC100.State
{
    public class RobotTeachParam
    {
        public double CassettePitch { get; set; }

        public double UpPitch { get; set; }

        public double DownPitch { get; set; }

        public int MaxSlot { get; set; }

        internal void SetData(string msg)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(msg)) return;

                var token = msg.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (token.Length < 4) return;

                if (double.TryParse(token[0], out double value)) this.CassettePitch = value;
                if (double.TryParse(token[1], out value)) this.UpPitch = value;
                if (double.TryParse(token[2], out value)) this.DownPitch = value;
                if (int.TryParse(token[3], out int value1)) this.MaxSlot = value1;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        internal object[] ToData()
        {
            return new object[] { this.CassettePitch, this.UpPitch, this.DownPitch, this.MaxSlot };
        }
    }
}
