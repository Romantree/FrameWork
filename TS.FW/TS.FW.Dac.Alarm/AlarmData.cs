using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Dac.Alarm
{
    public class AlarmData<T> where T : Enum
    {
        public DateTime Time { get; set; } = DateTime.Now;

        public T Alarm { get; set; }

        public AlarmLevel Level { get; set; }

        public string Contetns { get; set; }

        public string Action { get; set; }

        public bool Enable { get; set; }

        public object State { get; set; }

        public AlarmRecoveryHandler<T> Recovery { get; set; }

        public bool IsRecovery => this.Level == AlarmLevel.Heavy;

        public static implicit operator AlarmData<T>(AlarmEntity item)
        {
            if (item == null) return null;

            return new AlarmData<T>()
            {
                Alarm = item.ToType<T>(),
                Level = (AlarmLevel)item.AlarmLevel,
                Contetns = item.Contents,
                Action = item.Action,
                Enable = item.Enable,
            };
        }

        public static implicit operator AlarmEntity(AlarmData<T> item)
        {
            if (item == null) return null;

            return new AlarmEntity(item.Alarm)
            {
                AlarmLevel = (int)item.Level,
                Contents = item.Contetns,
                Action = item.Action,
                Enable = item.Enable,
            };
        }
    }
}
