using System.Collections.Generic;
using System.Linq;
using TS.FW.CTC.Test.Process.Robot.Item;
using TS.FW.CTC.Test.Process.Work.Item;

namespace TS.FW.CTC.Test.Process.Robot
{
    public partial class RobotProcess
    {
        private Queue<IRobotSeq> _robotSeq = new Queue<IRobotSeq>();
        private IRobotSeq _curSeq = null;

        private RobotExchangeSeq _exSeq => this._curSeq as RobotExchangeSeq;

        private WorkItem _curItem = null;
        private WorkItem _curItem2 = null;

        public StepResult CHECK_PROC_ENTER()
        {
            if (this._isMapping)
            {
                this.Step.Change(RobotStep.MAPPING_START);
                return StepResult.Change;
            }

            if(this._isClean)
            {
                this.Step.Change(RobotStep.CLEAN_WAFER_START);
                return StepResult.Change;
            }

            return StepResult.Next;
        }

        public StepResult START()
        {
            this.SetProcessMessage("PORT[{0}] 작업 시작", this._workPort + 1);

            this._robotSeq.Clear();

            this.SetProcessMessage("로봇 모드 {0}", this.RobotHeadMode);

            switch (this.RobotHeadMode)
            {
                case RobotHeadMode.TwoHeand:
                    {
                        var seqList = WorkItem.ToWorkSeq(this.GetWorkList());

                        foreach (var item in IRobotSeq.ToRobotSeq(seqList))
                        {
                            this._robotSeq.Enqueue(item);
                        }
                    }
                    break;
                case RobotHeadMode.OneHeand:
                    {
                        var seqList = WorkItem.ToWorkSeqOneHand(this.GetWorkList());

                        foreach (var item in IRobotSeq.ToRobotSeqOneHand(seqList))
                        {
                            this._robotSeq.Enqueue(item);
                        }
                    }
                    break;
                case RobotHeadMode.FixHeand:
                    {
                        var seqList = WorkItem.ToWorkSeqFix(this.GetWorkList());

                        foreach (var item in IRobotSeq.ToRobotSeqFix(seqList))
                        {
                            this._robotSeq.Enqueue(item);
                        }
                    }
                    break;
                default:
                    {
                        this.SetProcessMessage("알 수 없는 모드 입니다. {0}", this.RobotHeadMode);
                        return StepResult.Stop;
                    }
            }

            this._curSeq = null;
            this._curItem = null;
            this._curItem2 = null;

            return StepResult.Next;
        }

        public StepResult CHECK_SEQ_ENTER()
        {
            if (this._robotSeq.Count <= 0)
            {
                this.Step.Change(RobotStep.END);
                return StepResult.Change;
            }

            this._curSeq = this._robotSeq.Dequeue();

            this.SetProcessMessage(this._curSeq.ToString());

            return StepResult.Next;
        }

        public StepResult CHECK_PROCESS_POLLING()
        {
            if (this._curSeq.Move == RobotMove.Port) return StepResult.Next;

            switch (this._curSeq.Move)
            {
                case RobotMove.Align:
                    {
                        if (this.Align.IsBusy == false) return StepResult.Next;
                    }
                    break;
                case RobotMove.Centering:
                    {
                        if (this.Centering.IsBusy == false) return StepResult.Next;
                    }
                    break;
                case RobotMove.Cotter:
                    {
                        if (this.Cotter.IsBusy == false) return StepResult.Next;
                    }
                    break;
                case RobotMove.Imprint:
                    break;
            }

            this.SetProcessMessage("프로세스 대기 [{0}]", this._curSeq.Move);

            return StepResult.Pending;
        }

        public StepResult START_SEQ()
        {
            if (this._exSeq != null)
            {
                this._curItem = this.WorkList.FirstOrDefault(t => t.SlotNo == this._exSeq.ExchangeSlotNo);
                this._curItem2 = this.WorkList.FirstOrDefault(t => t.SlotNo == this._exSeq.SlotNo);
            }
            else
            {
                this._curItem = this.WorkList.FirstOrDefault(t => t.SlotNo == this._curSeq.SlotNo);
            }

            return StepResult.Next;
        }

        public StepResult MOVE_ROBOT_ENTER()
        {
            this.SetProcessMessage("로봇 이동 [{0}]", this._curSeq.Move);

            System.Threading.Thread.Sleep(10);

            return StepResult.Next;
        }

        public StepResult MOVE_ROBOT_POLLING()
        {
            this.SetProcessMessage("로봇 이동 완료 [{0}]", this._curSeq.Move);

            return StepResult.Next;
        }

        public StepResult DOOR_OPEN_ENTER()
        {
            this.SetProcessMessage("도어 오픈 [{0}]", this._curSeq.Move);

            System.Threading.Thread.Sleep(10);

            return StepResult.Next;
        }

        public StepResult DOOR_OPEN_POLLING()
        {
            this.SetProcessMessage("도어 오픈 완료 [{0}]", this._curSeq.Move);

            return StepResult.Next;
        }

        public StepResult MOVE_ROBOT_ACTION_ENTER()
        {
            var data = this.GetRobotActionData(this._curSeq);

            if (data.Arm == null)
            {
                this.SetProcessMessage("[{0}]{1} 할 수 없는 상태 입니다.", data.Action, data.Move);
                return StepResult.Stop;
            }

            this.SetProcessMessage("로봇 {0} 시작.", data.ToString(this._workPort));

            this.TimerStart();

            return StepResult.Next;
        }

        public StepResult MOVE_ROBOT_ACTION_POLLING()
        {
            var data = this.GetRobotActionData(this._curSeq);

            if (this.TimeOutCheck(1 * 1000)) // 동작 완료 체크
            {
                if (data.Action == RobotAction.GET)
                {
                    data.Arm.IsWafer = true;
                    data.Arm.WaferSlot = data.SlotNo;

                    this.SetWorkItem(data.Move, null);
                }
                else
                {
                    data.Arm.IsWafer = false;
                    data.Arm.WaferSlot = -1;

                    this.SetWorkItem(data.Move, this._curItem);
                }

                this.SetRobotHistoy(data.SlotNo, data.ArmType, data.Action, data.Move);

                this.SetProcessMessage("로봇 {0} 완료.", data.ToString(this._workPort));

                return StepResult.Next;
            }
            else if (this.TimeOutCheck(5 * 60 * 1000))
            {
                this.SetProcessMessage("로봇 {0} TimeOut.", data.ToString(this._workPort));

                return StepResult.Alarm;
            }

            return StepResult.Pending;
        }

        public StepResult MOVE_ROBOT_EX_ACTION_ENTER()
        {
            if (this._exSeq == null) return StepResult.Next;

            var data = this.GetRobotActionDataEx(this._exSeq);

            if (data.Arm == null)
            {
                this.SetProcessMessage("[{0}]{1} 할 수 없는 상태 입니다.", data.Action, data.Move);
                return StepResult.Stop;
            }

            this.SetProcessMessage("로봇 {0} 시작.", data.ToString(this._workPort));

            this.TimerStart();

            return StepResult.Next;
        }

        public StepResult MOVE_ROBOT_EX_ACTION_POLLING()
        {
            if (this._exSeq == null) return StepResult.Next;

            var data = this.GetRobotActionDataEx(this._exSeq);

            if (this.TimeOutCheck(1 * 1000)) // 동작 완료 체크
            {
                data.Arm.IsWafer = false;
                data.Arm.WaferSlot = -1;

                this.SetWorkItem(data.Move, this._curItem);

                this.SetRobotHistoy(data.SlotNo, data.ArmType, data.Action, data.Move);

                this.SetProcessMessage("로봇 {0} 완료.", data.ToString(this._workPort));

                return StepResult.Next;
            }
            else if (this.TimeOutCheck(5 * 60 * 1000))
            {
                this.SetProcessMessage("로봇 {0} TimeOut.", data.ToString(this._workPort));

                return StepResult.Alarm;
            }

            return StepResult.Pending;
        }

        public StepResult DOOR_CLOSE_ENTER()
        {
            this.SetProcessMessage("도어 닫기 [{0}]", this._curSeq.Move);

            System.Threading.Thread.Sleep(10);

            return StepResult.Next;
        }

        public StepResult DOOR_CLOSE_POLLING()
        {
            this.SetProcessMessage("도어 닫기 완료 [{0}]", this._curSeq.Move);

            return StepResult.Next;
        }

        public StepResult END_SEQ()
        {
            if (this._curSeq is RobotGetSeq) return StepResult.Next;

            this.SetWorkStart(this._curSeq.Move);

            return StepResult.Next;
        }

        public StepResult MOVE_CHECK_SEQ()
        {
            this.Step.Change(RobotStep.CHECK_SEQ_ENTER);
            return StepResult.Change;
        }

        public StepResult END()
        {
            this.SetProcessMessage("PORT[{0}] 작업 종료", this._workPort + 1);

            return StepResult.Finish;
        }

        public StepResult MAPPING_START()
        {
            this.SetProcessMessage("PORT[{0}] Mapping 시작", this._workPort + 1);

            this.OnPortMappingStartEvent?.Invoke(this, this.CurrentPort);

            for (int i = 0; i < TestWaferCount; i++)
            {
                this.CurrentPort.SlotList.Add(i, true);
            }

            System.Threading.Thread.Sleep(3000);

            return StepResult.Next;
        }

        public StepResult MAPPING_END()
        {
            this.SetProcessMessage("PORT[{0}] Mapping 종료", this._workPort + 1);

            this.OnPortMappingCompleteEvent?.Invoke(this, this.CurrentPort);

            return StepResult.Finish;
        }

        public StepResult CLEAN_WAFER_START()
        {
            this._robotSeq.Clear();

            if(this.TopArm.IsWafer)
            {
                this._robotSeq.Enqueue(new RobotPutSeq()
                {
                    Move = RobotMove.Port,
                    SlotNo = this.TopArm.WaferSlot,
                });
            }

            if (this.BottomArm.IsWafer)
            {
                this._robotSeq.Enqueue(new RobotPutSeq()
                {
                    Move = RobotMove.Port,
                    SlotNo = this.BottomArm.WaferSlot,
                });
            }

            if(this.Align.IsWork)
            {
                this._robotSeq.Enqueue(new RobotGetSeq()
                {
                    Move = RobotMove.Align,
                    SlotNo = this.Align.WorkItem.SlotNo,
                });

                this._robotSeq.Enqueue(new RobotPutSeq()
                {
                    Move = RobotMove.Port,
                    SlotNo = this.Align.WorkItem.SlotNo,
                });
            }

            if (this.Centering.IsWork)
            {
                this._robotSeq.Enqueue(new RobotGetSeq()
                {
                    Move = RobotMove.Centering,
                    SlotNo = this.Centering.WorkItem.SlotNo,
                });

                this._robotSeq.Enqueue(new RobotPutSeq()
                {
                    Move = RobotMove.Port,
                    SlotNo = this.Centering.WorkItem.SlotNo,
                });
            }

            if (this.Cotter.IsWork)
            {
                this._robotSeq.Enqueue(new RobotGetSeq()
                {
                    Move = RobotMove.Cotter,
                    SlotNo = this.Cotter.WorkItem.SlotNo,
                });

                this._robotSeq.Enqueue(new RobotPutSeq()
                {
                    Move = RobotMove.Port,
                    SlotNo = this.Cotter.WorkItem.SlotNo,
                });
            }

            this.Step.Change(RobotStep.CHECK_SEQ_ENTER);
            return StepResult.Change;
        }
    }
}
