using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Dac.Core;
using TS.FW.Dac.Transaction;

namespace TS.FW.Dac.Alarm
{
    public class AlarmRTx : IBizRTxBase
    {
        public Response InitAlarm(params Enum[] alarm)
        {
            try
            {
                if (alarm == null || alarm.Length <= 0)
                {
                    using (var dac = new AlarmDac())
                    {
                        dac.DeleteAll();
                    }

                    using (var dac = new AlarmHistoryDac())
                    {
                        dac.DeleteAll();
                    }

                    return new Response();
                }

                using (var dac = new AlarmDac())
                {
                    dac.DeleteNoMatch(alarm);

                    var list = dac.SelectedAll();

                    var insertList = alarm.Where(t => list.Any(x => x.AlarmCode == t.ToString()) == false);
                    if (insertList.Count() > 0)
                    {
                        dac.Insert(insertList);
                    }
                }

                using (var dac = new AlarmHistoryDac())
                {
                    dac.DeleteNoMatch(alarm.Select(t => t.ToString()).ToArray());

                    dac.InitAlarmHistory();
                }

                return new Response();
            }
            catch (Exception ex)
            {
                ContextUtil.SetAbort();
                return ex;
            }
        }

        public Response Insert(AlarmEntity entity)
        {
            try
            {
                using (var dac = new AlarmDac())
                {
                    dac.Insert(entity);
                }

                return new Response();
            }
            catch (Exception ex)
            {
                ContextUtil.SetAbort();
                return ex;
            }
        }

        public Response Update(AlarmEntity entity)
        {
            try
            {
                using (var dac = new AlarmDac())
                {
                    dac.Update(entity);
                }

                return new Response();
            }
            catch (Exception ex)
            {
                ContextUtil.SetAbort();
                return ex;
            }
        }

        public Response Update(IEnumerable<AlarmEntity> entitys)
        {
            try
            {
                using (var dac = new AlarmDac())
                {
                    dac.Update(entitys);
                }

                return new Response();
            }
            catch (Exception ex)
            {
                ContextUtil.SetAbort();
                return ex;
            }
        }

        public Response Delete(Enum alarm)
        {
            try
            {
                using (var dac = new AlarmDac())
                {
                    dac.Delete(alarm.ToString());
                }

                using (var dac = new AlarmHistoryDac())
                {
                    dac.Delete(alarm.ToString());
                }

                return new Response();
            }
            catch (Exception ex)
            {
                ContextUtil.SetAbort();
                return ex;
            }
        }

        public Response DeleteAll()
        {
            try
            {
                using (var dac = new AlarmDac())
                {
                    dac.DeleteAll();
                }

                using (var dac = new AlarmHistoryDac())
                {
                    dac.DeleteAll();
                }

                return new Response();
            }
            catch (Exception ex)
            {
                ContextUtil.SetAbort();
                return ex;
            }
        }
    }
}
