using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.CTC.Test.Process.Work.Item;

namespace TS.FW.CTC.Test.Process.Work
{
    public abstract class IWorkProcess : IProcessTimer
    {
        public static int UnitTestDelay = 5000;

        public WorkItem WorkItem { get; private set; }

        public bool IsWork => this.WorkItem != null;

        public WorkRecipe Recipe => this.IsWork ? this.WorkItem.NextRecipe : null;

        public override void DoWork_Porcess()
        {
            try
            {
                lock (this)
                {
                    var item = this.WorkItem;
                    if (item == null) return;

                    var res = this.ExcuteProcess(item);

                    switch (res)
                    {
                        case StepResult.Next:
                            {
                                this.Next(item);
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
                                this.Finish(item);
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
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public virtual void SetItem(WorkItem item)
        {
            try
            {
                lock (this)
                {
                    this.WorkItem = item;
                    if (this.Recipe == null) return;

                    this.Recipe.StartTime = DateTime.Now;
                    this.Recipe.Complete = false;

                    switch (this.Recipe.Type)
                    {
                        case UnitRecipeType.Align:
                            {
                                item.Step.Align.Init();
                            }
                            break;
                        case UnitRecipeType.Centering:
                            {
                                item.Step.Centering.Init();
                            }
                            break;
                        case UnitRecipeType.Cotter:
                            {
                                item.Step.Cotter.Init();
                            }
                            break;
                        case UnitRecipeType.Imprint:
                            {

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

        protected virtual void Next(WorkItem item)
        {
            try
            {
                if (this.Recipe == null) return;

                switch (this.Recipe.Type)
                {
                    case UnitRecipeType.Align:
                        {
                            item.Step.Align.Next();
                        }
                        break;
                    case UnitRecipeType.Centering:
                        {
                            item.Step.Centering.Next();
                        }
                        break;
                    case UnitRecipeType.Cotter:
                        {
                            item.Step.Cotter.Next();
                        }
                        break;
                    case UnitRecipeType.Imprint:
                        {

                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        protected virtual void Finish(WorkItem item)
        {
            try
            {
                if (this.Recipe == null) return;

                this.Recipe.EndTime = DateTime.Now;
                this.Recipe.Complete = true;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                //this.WorkItem = null;
            }
        }

        protected virtual StepResult ExcuteProcess(WorkItem item)
        {
            if (this.Recipe == null) return StepResult.Pending;

            switch (this.Recipe.Type)
            {
                case UnitRecipeType.Align: return this.ExcuteProcess(item, item.Step.Align);
                case UnitRecipeType.Centering: return this.ExcuteProcess(item, item.Step.Centering);
                case UnitRecipeType.Cotter: return this.ExcuteProcess(item, item.Step.Cotter);
                case UnitRecipeType.Imprint:
                    break;
            }

            return StepResult.Not_Found_Step;
        }

        private StepResult ExcuteProcess<T>(WorkItem item, ProcessStep<T> step) where T : struct, IConvertible
        {
            this.AlarmCheck();

            if (this.IsPause) return StepResult.Pending;

            return step.ExcuteProcess(this, item);
        }

        private void AlarmCheck()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
