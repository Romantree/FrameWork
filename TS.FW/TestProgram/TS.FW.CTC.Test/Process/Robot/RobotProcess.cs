using System;
using System.Collections.Generic;
using System.Linq;
using TS.FW.CTC.Test.Dto;
using TS.FW.CTC.Test.Process.Robot.Item;
using TS.FW.CTC.Test.Process.Work.Align;
using TS.FW.CTC.Test.Process.Work.Centering;
using TS.FW.CTC.Test.Process.Work.Cotter;
using TS.FW.CTC.Test.Process.Work.Item;

namespace TS.FW.CTC.Test.Process.Robot
{
    public partial class RobotProcess : IProcessTimer
    {
        public static int TestWaferCount = 0;

        private int _workPort = 0;
        private bool _isMapping = false;
        private bool _isClean = false;

        public readonly AlignProcess Align = new AlignProcess();
        public readonly CenteringProcess Centering = new CenteringProcess();
        public readonly CotterProcess Cotter = new CotterProcess();

        public override string Name => "Robot";

        public event EventHandler<PortData> OnPortMappingStartEvent;

        public event EventHandler<PortData> OnPortMappingCompleteEvent;

        public event EventHandler<RobotHistory> OnRobotHistoryEvent;

        public int No { get; set; }

        /// <summary>
        /// 로봇 팔 우선순위 옵션
        /// </summary>
        public RobotArmType RobotArmPriority { get; set; } = RobotArmType.TOP;

        /// <summary>
        /// 로봇 팔 1개만 사용
        /// </summary>
        public RobotHeadMode RobotHeadMode { get; set; }

        /// <summary>
        /// Slot 작업 순서 반전
        /// </summary>
        public bool IsSlotNoDesc { get; set; } = false;

        public new bool IsBusy => base.IsBusy || this.Align.IsBusy || this.Centering.IsBusy || this.Cotter.IsBusy;

        public List<PortData> PortList { get; private set; } = new List<PortData>();

        public PortData CurrentPort => this.PortList.FirstOrDefault(t => t.PortNo == this._workPort);

        public List<WorkItem> WorkList { get; private set; } = new List<WorkItem>();

        public bool Complete => this.WorkList.Count > 0 && this.WorkList.All(t => t.Complete) && this._robotSeq.Count <= 0;

        public RobotArm TopArm { get; private set; } = new RobotArm() { RobotArmType = RobotArmType.TOP };

        public RobotArm BottomArm { get; private set; } = new RobotArm() { RobotArmType = RobotArmType.BOTTOM };

        public bool IsWafer => this.TopArm.IsWafer || this.BottomArm.IsWafer;

        private ProcessStep<RobotStep> Step { get; set; } = new ProcessStep<RobotStep>();

        public void Start(LotRecipe recipe, int portNo)
        {
            try
            {
                if (this.IsBusy || this.IsWafer) return;
                if (this.PortList.Any(t => t.PortNo == portNo) == false) return;

                var port = this.PortList.First(t => t.PortNo == portNo);
                if (port.SlotList.Count <= 0) return;

                this.WorkList.Clear();

                foreach (var slot in port.SlotList)
                {
                    if (slot.Value == false
                        || slot.Key > recipe.SlotList.Count
                        || recipe.SlotList[slot.Key].UnitList.Count <= 0) continue;

                    var item = new WorkItem() { SlotNo = slot.Key };
                    item.SetRecipe(recipe.SlotList[slot.Key]);

                    this.WorkList.Add(item);
                }

                this._isClean = false;
                this._isMapping = false;
                this._workPort = portNo;

                this.Step.Init();
                base.Start();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void MappingStart(int portNo)
        {
            try
            {
                if (this.IsBusy || this.IsWafer) return;

                if (this.PortList.Any(t => t.PortNo == portNo) == false)
                {
                    this.PortList.Add(new PortData() { PortNo = portNo });
                }

                this._isClean = false;
                this._isMapping = true;
                this._workPort = portNo;

                this.CurrentPort.SlotList.Clear();
                this.CurrentPort.History.Clear();

                this.Step.Init();
                base.Start();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void CleanStart(int? portNo = null)
        {
            try
            {
                if (this.IsBusy) return;

                this._isClean = true;
                this._isMapping = false;

                if (portNo.HasValue)
                    this._workPort = portNo.Value;

                this.Step.Init();
                base.Start();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public override void DoWork_Porcess()
        {
            try
            {
                var res = this.Step.ExcuteProcess(this);

                switch (res)
                {
                    case StepResult.Next:
                        {
                            this.Step.Next();
                        }
                        break;
                    case StepResult.Alarm:
                        {
                            this.SetProcessMessage("Alarm occurred.");
                            this.Pause();
                        }
                        break;
                    case StepResult.Error:
                        {
                            this.SetProcessMessage("Error occurred.");
                            this.Stop();
                        }
                        break;
                    case StepResult.Finish:
                        {
                            this.Stop();
                        }
                        break;
                    case StepResult.Not_Found_Step:
                        {
                            this.SetProcessMessage("Not Found Step.");
                            this.Stop();
                        }
                        break;
                    case StepResult.Stop:
                        {
                            this.Stop();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private IEnumerable<WorkItem> GetWorkList() => this.IsSlotNoDesc ? this.WorkList.OrderByDescending(t => t.SlotNo) : this.WorkList.OrderBy(t => t.SlotNo);

        private RobotArm GetArm(int slotNo)
        {
            if (this.RobotArmPriority == RobotArmType.TOP)
            {
                if (this.TopArm == true && this.TopArm == slotNo) return this.TopArm;
                if (this.BottomArm == true && this.BottomArm == slotNo) return this.BottomArm;
            }
            else
            {
                if (this.BottomArm == true && this.BottomArm == slotNo) return this.BottomArm;
                if (this.TopArm == true && this.TopArm == slotNo) return this.TopArm;
            }

            return null;
        }

        private RobotArm GetEmptyArm()
        {
            if (this.RobotArmPriority == RobotArmType.TOP)
            {
                if (this.TopArm == false) return this.TopArm;
                if (this.BottomArm == false) return this.BottomArm;
            }
            else
            {
                if (this.BottomArm == false) return this.BottomArm;
                if (this.TopArm == false) return this.TopArm;
            }

            return null;
        }

        private void SetWorkItem(RobotMove move, WorkItem item)
        {
            switch (this._curSeq.Move)
            {
                case RobotMove.Align:
                    {
                        this.Align.SetItem(item);
                    }
                    break;
                case RobotMove.Centering:
                    {
                        this.Centering.SetItem(item);
                    }
                    break;
                case RobotMove.Cotter:
                    {
                        this.Cotter.SetItem(item);
                    }
                    break;
                case RobotMove.Imprint:
                    break;
            }
        }

        private void SetWorkStart(RobotMove move)
        {
            switch (this._curSeq.Move)
            {
                case RobotMove.Align:
                    {
                        this.Align.Start();
                    }
                    break;
                case RobotMove.Centering:
                    {
                        this.Centering.Start();
                    }
                    break;
                case RobotMove.Cotter:
                    {
                        this.Cotter.Start();
                    }
                    break;
                case RobotMove.Imprint:
                    break;
            }
        }

        private void SetRobotHistoy(int slotNo, RobotArmType robotArm, RobotAction action, RobotMove move)
        {
            try
            {
                var item = new RobotHistory()
                {
                    PortNo = this._workPort + 1,
                    SlotNo = slotNo + 1,
                    RobotArm = robotArm,
                    Action = action,
                    Move = move,
                };

                this.CurrentPort.History.Add(item);
                this.OnRobotHistoryEvent?.Invoke(this, item);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private RobotActionData GetRobotActionData(IRobotSeq seq)
        {
            return new RobotActionData()
            {
                SlotNo = seq.SlotNo,
                Move = seq.Move,
                Action = this.GetRobotAction(seq),
                Arm = this.GetRobotArm(seq),
            };
        }

        private RobotActionData GetRobotActionDataEx(RobotExchangeSeq seq)
        {
            return new RobotActionData()
            {
                SlotNo = seq.ExchangeSlotNo,
                Move = seq.Move,
                Action = RobotAction.PUT,
                Arm = this.GetArm(seq.ExchangeSlotNo),
            };
        }

        private RobotAction GetRobotAction(IRobotSeq seq)
        {
            if (seq is RobotGetSeq || seq is RobotExchangeSeq) return RobotAction.GET;

            return RobotAction.PUT;
        }

        private RobotArm GetRobotArm(IRobotSeq seq)
        {
            if (seq is RobotGetSeq || seq is RobotExchangeSeq) return this.GetEmptyArm();

            return this.GetArm(seq.SlotNo);
        }
    }

    public class RobotActionData
    {
        public int SlotNo { get; set; }

        public RobotAction Action { get; set; }

        public RobotMove Move { get; set; }

        public RobotArm Arm { get; set; }

        public RobotArmType ArmType => this.Arm.RobotArmType;

        public string ToString(int portNo)
        {
            return string.Format("[{0}:{1}:{2}] = [P:{3} S:{4}]", this.Move, this.Action, this.ArmType, portNo, this.SlotNo);
        }
    }
}
