using System;
using System.Collections.Generic;

namespace TS.FW.Robot.RC100.State
{
    public class RobotPosition
    {
        public double X { get; set; }

        public double T { get; set; }

        public double Z { get; set; }

        public double R1 { get; set; }

        public double R2 { get; set; }

        internal void SetData(string msg)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(msg)) return;

                var token = msg.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (token.Length < 5) return;

                if (double.TryParse(token[0], out double value)) this.X = value;
                if (double.TryParse(token[1], out value)) this.T = value;
                if (double.TryParse(token[2], out value)) this.Z = value;
                if (double.TryParse(token[3], out value)) this.R1 = value;
                if (double.TryParse(token[4], out value)) this.R2 = value;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        internal object[] ToData()
        {
            return new object[] { this.X, this.T, this.Z, this.R1, this.R2 };
        }

        internal string GetData()
        {
            var list = new List<string>();

            foreach (var info in this.GetType().GetProperties())
            {
                var value = (double)info.GetValue(this);

                list.Add(value.ToString());
            }

            return string.Join(",", list);
        }
    }
}
