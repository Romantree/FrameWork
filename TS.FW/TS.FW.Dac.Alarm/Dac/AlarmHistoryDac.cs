using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Dac.Core;

namespace TS.FW.Dac.Alarm
{
    internal class AlarmHistoryDac : IDacBase
    {
        public AlarmHistoryDac() : base("ALARM_DB")
        {

        }

        public List<AlarmHistoryEntity> SelectedByBetween(DateTime start, DateTime end)
        {
            var query = @"
                        SELECT
                            H.AlarmCode
                            , H.PostTime
                            , H.ClearTime
                            , A.AlarmLevel
                            , A.Contents
                            , A.Action
                        FROM
                            TbAlarmHistory AS H
                            INNER JOIN TbAlarm AS A ON H.AlarmCode = A.AlarmCode
                        WHERE
                            A.Enable = 1 AND
                            H.PostTime BETWEEN {0} AND {1}
                        ORDER BY
                            H.PostTime ASC";

            var excute = this.Context.ExecuteQuery<AlarmHistoryEntity>(query, start, end);

            return excute.ToList();
        }

        public bool IsClearAlarm(string alarmCode)
        {
            var query = @"
                        SELECT
                            H.AlarmCode
                            , H.PostTime
                            , H.ClearTime
                            , A.AlarmLevel
                            , A.Contents
                            , A.Action
                        FROM
                            TbAlarmHistory AS H
                            INNER JOIN TbAlarm AS A ON H.AlarmCode = A.AlarmCode
                        WHERE
                            A.Enable = 1 
                            AND H.ClearTime IS NULL
                            AND H.AlarmCode = {0}
                        ORDER BY
                            H.PostTIme DESC
                        LIMIT 1";

            var excute = this.Context.ExecuteQuery<AlarmHistoryEntity>(query, alarmCode);
            var entity = excute.FirstOrDefault();

            return entity == null;
        }

        public void InitAlarmHistory()
        {
            var query = @"
                        UPDATE TbAlarmHistory
                            SET ClearTime = {0}
                        WHERE
                            ClearTime IS NULL
                        ";

            this.Context.ExecuteCommand(query, DateTime.Now);
        }

        public void AlarmPost(string alarmCode, DateTime postTime)
        {
            if (this.IsClearAlarm(alarmCode) == false) return;

            var query = @"
                        INSERT INTO TbAlarmHistory 
                            (AlarmCode, PostTime)
                        VALUES 
                            ({0}, {1})
                        ";

            this.Context.ExecuteCommand(query, alarmCode, postTime);
        }

        public void AlarmClear(string alarmCode, DateTime clearTime)
        {
            if (this.IsClearAlarm(alarmCode)) return;

            var query = @"
                        UPDATE TbAlarmHistory
                            SET ClearTime = {1}
                        WHERE
                            AlarmCode = {0}
                            AND PostTime IN
                            (
                                SELECT
                                    PostTime
                                FROM
                                    TbAlarmHistory
                                WHERE
                                    AlarmCode = {0}
                                ORDER BY
                                    PostTime DESC
                                LIMIT 1
                            )
                        ";

            this.Context.ExecuteCommand(query, alarmCode, clearTime);
        }

        public void Delete(DateTime deleteTime)
        {
            var query = @"
                        DELETE FROM TbAlarmHistory
                        WHERE PostTime <= {0}
                        ";

            this.Context.ExecuteCommand(query, deleteTime);
        }

        public void Delete(string alarmCode)
        {
            var query = @"
                        DELETE FROM TbAlarmHistory
                        WHERE AlarmCode = {0}
                        ";

            this.Context.ExecuteCommand(query, alarmCode);
        }

        public void DeleteAll()
        {
            var query = @"
                        DELETE FROM TbAlarmHistory
                        ";

            this.Context.ExecuteCommand(query);
        }

        public void DeleteNoMatch(params string[] alarmCode)
        {
            var query = @"
                        DELETE FROM TbAlarmHistory
                        WHERE AlarmCode NOT IN ({0})
                        ";

            var command = string.Format(query, string.Join(",", alarmCode.Select(t => string.Format("'{0}'", t))));
            this.Context.ExecuteCommand(command);
        }
    }
}
