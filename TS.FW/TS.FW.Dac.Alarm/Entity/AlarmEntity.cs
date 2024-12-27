using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Dac.Alarm
{
    [Table(Name = "TbAlarm")]
    public class AlarmEntity
    {
        [Column(IsPrimaryKey = true)]
        public string AlarmCode { get; set; }

        [Column]
        public int AlarmLevel { get; set; }

        [Column]
        public string Contents { get; set; }

        [Column]
        public string Action { get; set; }

        [Column]
        public bool Enable { get; set; }

        public AlarmEntity()
        {

        }

        public AlarmEntity(Enum alarm)
        {
            this.AlarmCode = alarm.ToString();
            this.AlarmLevel = 0;
            this.Contents = string.Empty;
            this.Action = string.Empty;
            this.Enable = true;
        }

        public void Update(AlarmEntity entity)
        {
            this.AlarmLevel = entity.AlarmLevel;
            this.Contents = entity.Contents;
            this.Action = entity.Action;
            this.Enable = entity.Enable;
        }

        public T ToType<T>() where T : Enum
        {
            return (T)Enum.Parse(typeof(T), this.AlarmCode);
        }
    }
}
