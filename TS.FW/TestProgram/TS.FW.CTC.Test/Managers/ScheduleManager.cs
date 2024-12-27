using System;
using System.Collections.Generic;
using System.Linq;
using TS.FW.CTC.Test.Dto;
using TS.FW.CTC.Test.Managers.Schedule;
using TS.FW.CTC.Test.Process;
using TS.FW.CTC.Test.Process.Robot;
using TS.FW.CTC.Test.Process.Robot.Item;
using TS.FW.Diagnostics;

namespace TS.FW.CTC.Test.Managers
{
    public class ScheduleManager
    {
        private readonly List<ScheduleItem> ScheduleIList = new List<ScheduleItem>();
        private readonly BackgroundTimer _trCheck = new BackgroundTimer();

        public readonly RobotProcess Robot1 = new RobotProcess() { No = 1 };
        public readonly RobotProcess Robot2 = new RobotProcess() { No = 2 };

        public event EventHandler<PortData> OnPortMappingStartEvent;

        public event EventHandler<PortData> OnPortMappingCompleteEvent;

        public event EventHandler<RobotHistory> OnRobotHistoryEvent;

        public ScheduleManager()
        {
            this.Robot1.OnPortMappingStartEvent += Robot_OnPortMappingStartEvent;
            this.Robot1.OnPortMappingCompleteEvent += Robot_OnPortMappingCompleteEvent;
            this.Robot1.OnRobotHistoryEvent += Robot_OnRobotHistoryEvent;

            this.Robot2.OnPortMappingStartEvent += Robot_OnPortMappingStartEvent;
            this.Robot2.OnPortMappingCompleteEvent += Robot_OnPortMappingCompleteEvent;
            this.Robot2.OnRobotHistoryEvent += Robot_OnRobotHistoryEvent;

            this._trCheck.SleepTimeMsc = 1000;
            this._trCheck.DoWork += _trCheck_DoWork;
            this._trCheck.Start();
        }

        public int Start(int robotNo, int protNo, LotRecipe recipe)
        {
            var item = new ScheduleItem()
            {
                RobotNo = robotNo,
                PortNo = protNo,
                Recipe = recipe,
            };

            lock (this.ScheduleIList)
            {
                this.ScheduleIList.Add(item);

                item.No = this.ScheduleIList.Count; 
            }

            return item.No;
        }

        public void Stop(int no)
        {
            var item = this.ScheduleIList.FirstOrDefault(t => t.No == no);
            if (item == null || item.State == ScheduleState.End) return;

            var robot = this.GetRobot(item.RobotNo);
            if(robot != null) robot.Stop();

            item.State = ScheduleState.End;
        }

        public void AllStop()
        {
            foreach (var item in this.ScheduleIList)
            {
                if (item.State == ScheduleState.End) continue;

                var robot = this.GetRobot(item.RobotNo);
                if (robot != null) robot.Stop();

                item.State = ScheduleState.End;
            }
        }

        private RobotProcess GetRobot(int no)
        {
            if (no == 1) return this.Robot1;
            if (no == 2) return this.Robot2;

            return null;
        }

        private void _trCheck_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                var list = this.ScheduleIList.Where(t => t.State != ScheduleState.End);

                foreach (var sc in list)
                {
                    var robot = this.GetRobot(sc.RobotNo);
                    if (robot == null || robot.IsBusy) continue;

                    switch (sc.State)
                    {
                        case ScheduleState.None:
                            {
                                sc.State = ScheduleState.MappingStart;
                                robot.MappingStart(sc.PortNo);
                            }
                            break;
                        case ScheduleState.MappingStart:
                            {

                            }
                            break;
                        case ScheduleState.MappingEnd:
                            {
                                sc.State = ScheduleState.Run;
                                robot.Start(sc.Recipe, sc.PortNo);
                            }
                            break;
                        case ScheduleState.Run:
                            {
                                if(robot.Complete)
                                {
                                    sc.State = ScheduleState.End;
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Robot_OnRobotHistoryEvent(object sender, RobotHistory e)
        {
            try
            {
                this.OnRobotHistoryEvent?.Invoke(sender, e);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Robot_OnPortMappingStartEvent(object sender, PortData e)
        {
            try
            {
                this.OnPortMappingStartEvent?.Invoke(sender, e);

                var robot = sender as RobotProcess;
                var item = this.ScheduleIList.FirstOrDefault(t => t.State != ScheduleState.End && t.RobotNo == robot.No);
                if (item == null) return;

                item.State = ScheduleState.MappingStart;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Robot_OnPortMappingCompleteEvent(object sender, PortData e)
        {
            try
            {
                this.OnPortMappingCompleteEvent?.Invoke(sender, e);

                var robot = sender as RobotProcess;
                var item = this.ScheduleIList.FirstOrDefault(t => t.State != ScheduleState.End && t.RobotNo == robot.No);
                if (item == null) return;

                item.State = ScheduleState.MappingEnd;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

    }
}
