using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Dac.Alarm
{
    public delegate void AlarmRecoveryHandler<T>(AlarmData<T> model) where T : Enum;

    public abstract class IAlarmBase<T> where T : Enum
    {
        protected readonly Dictionary<T, AlarmData<T>> _alarmList = new Dictionary<T, AlarmData<T>>();

        public bool IsAlarm => this._alarmList.Count > 0;

        /// <summary>
        /// Alarm 발생된 값 중에 Light 레벨외 알람 발생 확인 조건 값.
        /// </summary>
        public bool IsAlarmNotLight => this.IsAlarm ? this._alarmList.Values.Any(t => t.Level != AlarmLevel.Light) : false;

        public bool IsCycleStop => this.IsAlarm ? this._alarmList.Values.Any(t => t.Level == AlarmLevel.CycleStop) : false;

        public event EventHandler<AlarmData<T>> OnAlarmPostEvent;
        public event EventHandler<T> OnAlarmClearEvent;

        public virtual AlarmLevel AlarmPost(T alarm, AlarmRecoveryHandler<T> fnRecovery = null, object stage = null)
        {
            lock (this._obj)
            {
                if (this._alarmList.ContainsKey(alarm) == true)
                {
                    return this._alarmList[alarm].Level;
                }
            }

            var model = this.GetAlarm(alarm);
            if (model.Enable == false) return AlarmLevel.Light;

            if (this._alarmList.ContainsKey(model.Alarm)) return model.Level;


            this.AlarmPostAciton(model);

            try
            {
                model.State = stage;
                model.Recovery = fnRecovery;

                this._alarmList.Add(model.Alarm, model);
                this.AlarmPostHistory(model.Alarm);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                this.OnAlarmPostEvent?.Invoke(this, model);
            }

            return model.Level;
        }

        public void AlarmRecovery(T alarm)
        {
            try
            {
                if (this._alarmList.ContainsKey(alarm) == false) return;

                var model = this._alarmList[alarm];

                if (model.IsRecovery)
                {
                    model.Recovery?.Invoke(model);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                this.AlarmClear(alarm);
            }
        }

        private object _obj = new object();

        public virtual void AlarmClear(T alarm)
        {
            if (this._alarmList.ContainsKey(alarm) == false) return;

            try
            {
                lock (this._obj)
                {
                    this._alarmList.Remove(alarm);
                }
                this.AlarmClearHistory(alarm);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                this.AlarmClearAction();
            }

            this.OnAlarmClearEvent?.Invoke(this, alarm);
        }

        public void AlarmClearAll()
        {
            try
            {
                foreach (var alarm in this._alarmList)
                {
                    this.AlarmClearHistory(alarm.Key);

                    this.OnAlarmClearEvent?.Invoke(this, alarm.Key);
                }

                lock (this._obj)
                {
                    this._alarmList.Clear();
                }

            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                this.AlarmClearAction();
            }
        }

        public abstract void AlarmClearAction();
        //{
        //    try
        //    {
        //        //this.IO.BuzzerAlarm = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Write(this, ex);
        //    }
        //}
        public abstract void AlarmPostAciton(AlarmData<T> alarm);
        //{
        //    try
        //    {
        //        switch (alarm.Level)
        //        {
        //            case AlarmLevel.Light:
        //                {
        //                }
        //                break;
        //            case AlarmLevel.Heavy:
        //                {
        //                }
        //                break;
        //            case AlarmLevel.Critical:
        //                {
        //                }
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Write(this, ex);
        //    }
        //}

        public void LoadDataBase()
        {
            using (var biz = new AlarmRTx())
            {
                var res = biz.InitAlarm(Enum.GetValues(typeof(T)).Cast<Enum>().ToArray());
                if (res == false) throw new Exception(res.Comment);
            }
        }

        public List<AlarmData<T>> GetPostAlarm()
        {
            return this._alarmList.Values.ToList();
        }

        public List<AlarmData<T>> GetAlarmList()
        {
            try
            {
                using (var biz = new AlarmNTx())
                {
                    var res = biz.GetAlarmList();
                    if (res == false) throw new Exception(res.Comment);

                    return res.Result.Select(t => (AlarmData<T>)t).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);

                return new List<AlarmData<T>>();
            }
        }

        public Response UpdateAlarm(AlarmData<T> model)
        {
            try
            {
                using (var biz = new AlarmRTx())
                {
                    return biz.Update(model);
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response UpdateAlarm(IEnumerable<AlarmData<T>> list)
        {
            try
            {
                using (var biz = new AlarmRTx())
                {
                    return biz.Update(list.Select(t => (AlarmEntity)t));
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public List<AlarmHistoryEntity> GetAlarmHistoryList(DateTime start, DateTime end)
        {
            try
            {
                using (var biz = new AlarmHistoryNTx())
                {
                    var res = biz.GetAlarmHistoryByBetween(start, end);
                    if (res == false) throw new Exception(res.Comment);

                    return res.Result.Select(t => (AlarmHistoryEntity)t).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);

                return new List<AlarmHistoryEntity>();
            }
        }

        private AlarmData<T> GetAlarm(T alarm)
        {
            try
            {
                var entity = this.GetAlarmEntity(alarm);
                if (entity == null) return null;// AlarmData.UNDEFINED_ALARM;

                return entity;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return null;// AlarmData.UNDEFINED_ALARM;
            }
        }

        private AlarmEntity GetAlarmEntity(T alarm)
        {
            try
            {
                using (var biz = new AlarmNTx())
                {
                    var res = biz.GetAlarm(alarm);
                    if (res == false) throw new Exception(res.Comment);

                    return res.Result;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return null;
            }
        }

        private void AlarmPostHistory(T alarm)
        {
            try
            {
                using (var biz = new AlarmHistoryRTx())
                {
                    var res = biz.AlarmPost(alarm);
                    if (res == false) Logger.Write(this, res.Comment, Logger.LogEventLevel.Error);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void AlarmClearHistory(T alarm)
        {
            try
            {
                using (var biz = new AlarmHistoryRTx())
                {
                    var res = biz.AlarmClear(alarm);
                    if (res == false) Logger.Write(this, res.Comment, Logger.LogEventLevel.Error);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
