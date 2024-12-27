using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Dac.Alarm;

namespace TS.FW.GIGA.Managers
{
    public class AlarmManager : IAlarmBase<eAlarm>
    {
        public override void AlarmPostAciton(AlarmData<eAlarm> alarm)
        {
			try
			{
				switch (alarm.Level)
				{
					case AlarmLevel.CycleStop:
						{

						}
						break;
					case AlarmLevel.Heavy:
						{
							AP.ProcessStop();
						}
						break;
					case AlarmLevel.Critical:
						{
                            AP.ProcessStop();
                        }
						break;
				}
			}
			catch (Exception ex)
			{
				Logger.Write(this, ex);
			}
        }

        public override void AlarmClearAction()
        {
			try
			{
                if (this.IsAlarmNotLight) return;
            }
			catch (Exception ex)
			{
				Logger.Write(this, ex);
			}
        }
    }
}
