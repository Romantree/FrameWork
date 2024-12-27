using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Dac.Core;

namespace TS.FW.Dac.Alarm
{
    internal class AlarmDac : IDacBase<AlarmEntity>
    {
        public AlarmDac() : base("ALARM_DB")
        {

        }

        public List<AlarmEntity> SelectedAll()
        {
            var tmp = this.Table.ToList();
            return this.Table.ToList();
        }

        public List<AlarmEntity> SelectedForEnable()
        {
            return this.Table.Where(t => t.Enable == true).ToList();
        }

        public AlarmEntity Selected(string alarmCode)
        {
            return this.Table.SingleOrDefault(t => t.AlarmCode == alarmCode);
        }

        public void Insert(AlarmEntity entity)
        {
            if (entity == null) throw new NullReferenceException("Entity 정보가 Null 입니다.");
            if (this.Table.Any(t => t.AlarmCode == entity.AlarmCode)) throw new Exception("동일한 정보가 존재합니다.");

            this.InsertOnSubmit(entity);
        }

        public void Insert(IEnumerable<Enum> list)
        {
            var query = @"
                        INSERT INTO TbAlarm
                            (AlarmCode, AlarmLevel, Contents, Action, Enable)
                        VALUES
                            {0}
                        ";

            var command = string.Format(query, string.Join(",", list.Select(t => string.Format("('{0}', 2, 'EMPTY', 'EMPTY', 1)", t.ToString()))));

            this.Context.ExecuteCommand(command);
        }

        public void Update(AlarmEntity entity)
        {
            if (entity == null) throw new NullReferenceException("Entity 정보가 Null 입니다.");
            if (this.Table.Any(t => t.AlarmCode == entity.AlarmCode) == false) throw new Exception("Update 가능한 정보가 존재하지 않습니다.");

            var updateEntity = this.Selected(entity.AlarmCode);

            updateEntity.Update(entity);
            this.SubmitChanges();
        }

        public void Update(IEnumerable<AlarmEntity> entitys)
        {
            if (entitys == null) throw new NullReferenceException("Entity 정보가 Null 입니다.");

            foreach (var entity in entitys)
            {
                if (this.Table.Any(t => t.AlarmCode == entity.AlarmCode) == false) continue;

                var updateEntity = this.Selected(entity.AlarmCode);
                updateEntity.Update(entity);
            }

            this.SubmitChanges();
        }

        public void Delete(string alarmCode)
        {
            var deleteEntity = this.Selected(alarmCode);
            if (deleteEntity == null) return;

            this.DeleteOnSubmit(deleteEntity);
        }

        public void DeleteAll()
        {
            var query = @"
                        DELETE FROM TbAlarm
                        ";

            this.Context.ExecuteCommand(query);
        }

        public void DeleteNoMatch(params Enum[] alarm)
        {
            var query = @"
                        DELETE FROM TbAlarm
                        WHERE AlarmCode NOT IN ({0})
                        ";

            var command = string.Format(query, string.Join(",", alarm.Select(t => string.Format("'{0}'", t.ToString()))));
            this.Context.ExecuteCommand(command);
        }
    }
}
