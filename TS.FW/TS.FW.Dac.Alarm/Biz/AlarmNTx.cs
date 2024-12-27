using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Dac.Core;

namespace TS.FW.Dac.Alarm
{
    public class AlarmNTx : IBizNTxBase
    {
        public ResponseList<AlarmEntity> GetAlarmList()
        {
            try
            {
                using (var dac = new AlarmDac())
                {
                    return new ResponseList<AlarmEntity>(dac.SelectedAll());
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public ResponseList<AlarmEntity> GetAlarmListForEnable()
        {
            try
            {
                using (var dac = new AlarmDac())
                {
                    return new ResponseList<AlarmEntity>(dac.SelectedForEnable());
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response<AlarmEntity> GetAlarm(Enum alarm)
        {
            try
            {
                using (var dac = new AlarmDac())
                {
                    return new Response<AlarmEntity>(dac.Selected(alarm.ToString()));
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
