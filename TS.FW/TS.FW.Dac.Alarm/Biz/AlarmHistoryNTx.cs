using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Dac.Core;

namespace TS.FW.Dac.Alarm
{
    public class AlarmHistoryNTx : IBizNTxBase
    {
        public ResponseList<AlarmHistoryEntity> GetAlarmHistoryByBetween(DateTime start, DateTime end)
        {
            try
            {
                using (var dac = new AlarmHistoryDac())
                {
                    return new ResponseList<AlarmHistoryEntity>(dac.SelectedByBetween(start, end));
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response<bool> IsClearAlarm(string alarmCode)
        {
            try
            {
                using (var dac = new AlarmHistoryDac())
                {
                    return new Response<bool>(dac.IsClearAlarm(alarmCode));
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
