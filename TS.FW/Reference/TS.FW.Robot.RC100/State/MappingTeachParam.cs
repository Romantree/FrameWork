using System;

namespace TS.FW.Robot.RC100.State
{
    public class MappingTeachParam
    {
        public int Speed { get; private set; }

        public int MaxThickness { get; private set; }

        public int Offset { get; private set; }

        public int MappingReference { get; private set; }

        public int Tolerance { get; private set; }

        public int MaxSlot { get; private set; }

        public int SlotPitch { get; private set; }

        public int MinThickness { get; private set; }

        internal void SetData(string msg)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(msg)) return;

                var token = msg.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (token.Length < 8) return;

                // TODO : hspark 데이터 유형 확인 필요
                if (int.TryParse(token[0], out int value)) this.Speed = value;
                if (int.TryParse(token[1], out value)) this.MaxThickness = value;
                if (int.TryParse(token[2], out value)) this.Offset = value;
                if (int.TryParse(token[3], out value)) this.MappingReference = value;
                if (int.TryParse(token[4], out value)) this.Tolerance = value;
                if (int.TryParse(token[5], out value)) this.MaxSlot = value;
                if (int.TryParse(token[6], out value)) this.SlotPitch = value;
                if (int.TryParse(token[7], out value)) this.MinThickness = value;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
