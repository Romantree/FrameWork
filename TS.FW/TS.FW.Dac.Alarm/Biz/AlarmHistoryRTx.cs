using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Dac.Core;
using TS.FW.Dac.Transaction;

namespace TS.FW.Dac.Alarm
{
    public class AlarmHistoryRTx : IBizRTxBase
    {
        public Response AlarmPost(Enum alarm)
        {
            try
            {
                using (var dac = new AlarmHistoryDac())
                {
                    dac.AlarmPost(alarm.ToString(), DateTime.Now);
                }

                return new Response();
            }
            catch (Exception ex)
            {
                ContextUtil.SetAbort();
                return ex;
            }
        }

        public Response AlarmClear(Enum alarm)
        {
            try
            {
                using (var dac = new AlarmHistoryDac())
                {
                    dac.AlarmClear(alarm.ToString(), DateTime.Now);
                }

                return new Response();
            }
            catch (Exception ex)
            {
                ContextUtil.SetAbort();
                return ex;
            }
        }

        public Response Delete(DateTime deleteTime)
        {
            try
            {
                using (var dac = new AlarmHistoryDac())
                {
                    dac.Delete(deleteTime);
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
