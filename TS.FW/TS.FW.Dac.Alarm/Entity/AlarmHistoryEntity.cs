using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Dac.Alarm
{
    [Table(Name = "TbAlarmHistory")]
    public class AlarmHistoryEntity
    {
        [Column(IsPrimaryKey = true)]
        public string AlarmCode { get; set; }

        [Column(IsPrimaryKey = true)]
        public DateTime PostTime { get; set; }

        [Column(CanBeNull = true)]
        public DateTime? ClearTime { get; set; }

        [Column]
        public int AlarmLevel { get; set; }

        [Column]
        public string Contents { get; set; }

        [Column]
        public string Action { get; set; }

        public AlarmHistoryEntity()
        {

        }

        public AlarmHistoryEntity(string alarmCode)
        {
            this.PostTime = DateTime.Now;
            this.ClearTime = null;
            this.AlarmCode = alarmCode;
        }

        public T ToType<T>()
        {
            return (T)Enum.Parse(typeof(T), this.AlarmCode);
        }
    }
}
