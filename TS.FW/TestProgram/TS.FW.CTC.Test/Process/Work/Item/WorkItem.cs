using System;
using System.Collections.Generic;
using System.Linq;
using TS.FW.CTC.Test.Dto;

namespace TS.FW.CTC.Test.Process.Work.Item
{
    public class WorkItem
    {
        private readonly List<WorkRecipe> _recipeList = new List<WorkRecipe>();

        private string GorupKey => string.Join("_", this._recipeList.Select(t => t.Recipe.Type));

        public int SlotNo { get; set; } = -1;

        public bool IsEnd { get; set; } = false;

        public WorkStep Step { get; } = new WorkStep();

        public WorkRecipe PrevRecipe => this.GetPrevRecipe();

        public WorkRecipe NextRecipe => this.GetNextRecipe();

        public bool Complete => this.GetComplete();

        public void SetRecipe(FlowRecipe recipe)
        {
            try
            {
                lock (this._recipeList)
                {
                    this._recipeList.Clear();

                    if (recipe == null || recipe.UnitList == null || recipe.UnitList.Count <= 0) return;

                    this._recipeList.AddRange(recipe.UnitList.Select(t => new WorkRecipe(t)));
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private WorkRecipe GetPrevRecipe()
        {
            try
            {
                lock (this._recipeList)
                {
                    if (this._recipeList == null || this._recipeList.Count <= 0) return null;

                    return this._recipeList.LastOrDefault(t => t.Complete == true);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return null;
            }
        }

        private WorkRecipe GetNextRecipe()
        {
            try
            {
                lock (this._recipeList)
                {
                    if (this._recipeList == null || this._recipeList.Count <= 0) return null;

                    return this._recipeList.FirstOrDefault(t => t.Complete == false);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return null;
            }
        }

        private bool GetComplete()
        {
            try
            {
                lock (this._recipeList)
                {
                    if (this._recipeList == null || this._recipeList.Count <= 0) return false;

                    return this._recipeList.All(t => t.Complete);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return false;
            }
        }

        private IEnumerable<WorkSeq> ToWorkSeq(int groupNo, int no)
        {
            var seqNo = groupNo + no;

            yield return new WorkSeq(seqNo, this.SlotNo, RobotAction.GET, RobotMove.Port) { No = no };

            foreach (var item in this._recipeList)
            {
                yield return new WorkSeq(seqNo, this.SlotNo, RobotAction.PUT, item.Recipe.Type) { No = no };

                yield return new WorkSeq(++seqNo, this.SlotNo, RobotAction.GET, item.Recipe.Type) { No = no };
            }

            yield return new WorkSeq(seqNo, this.SlotNo, RobotAction.PUT, RobotMove.Port) { No = no };
        }

        private IEnumerable<WorkSeq> ToWorkSeqOneHand(int groupNo, int no)
        {
            var seqNo = groupNo + no;

            yield return new WorkSeq(seqNo, this.SlotNo, RobotAction.GET, RobotMove.Port) { No = no };

            foreach (var item in this._recipeList)
            {
                yield return new WorkSeq(seqNo++, this.SlotNo, RobotAction.PUT, item.Recipe.Type) { No = no };

                yield return new WorkSeq(seqNo, this.SlotNo, RobotAction.GET, item.Recipe.Type) { No = no };
            }

            yield return new WorkSeq(seqNo, this.SlotNo, RobotAction.PUT, RobotMove.Port) { No = no };
        }

        private IEnumerable<WorkSeq> ToWorkSeqFix(int groupNo, int no, int count)
        {
            var seqNo = groupNo + no;

            foreach (var item in this._recipeList)
            {
                yield return new WorkSeq(seqNo, this.SlotNo, RobotAction.GET, RobotMove.Port) { No = no };

                yield return new WorkSeq(seqNo++, this.SlotNo, RobotAction.PUT, item.Recipe.Type) { No = no };

                yield return new WorkSeq(seqNo, this.SlotNo, RobotAction.GET, item.Recipe.Type) { No = no };

                yield return new WorkSeq(seqNo, this.SlotNo, RobotAction.PUT, RobotMove.Port) { No = no };

                seqNo += count;
            }
        }

        public static IEnumerable<WorkSeq> ToWorkSeq(IEnumerable<WorkItem> workList, int inc = 100)
        {
            var gIndex = 1;

            foreach (var gItem in workList.GroupBy(t => t.GorupKey))
            {
                foreach (var sItem in gItem.SelectMany((t, i) => t.ToWorkSeq(gIndex * inc, i)).GroupBy(t => t.GroupNo).Select(t => t.ToArray()))
                {
                    var min = sItem.Min(t => t.No);
                    var max = sItem.Max(t => t.No);

                    yield return sItem.First(t => t.No == max && t.Action == RobotAction.GET);

                    if (sItem.Length - 2 != 0)
                    {
                        for (int i = max; i > min; i--)
                        {
                            yield return sItem.First(t => t.No == i - 1 && t.Action == RobotAction.GET);
                            yield return sItem.First(t => t.No == i && t.Action == RobotAction.PUT);
                        }
                    }

                    yield return sItem.First(t => t.No == min && t.Action == RobotAction.PUT);
                }

                gIndex++;
            }
        }

        public static IEnumerable<WorkSeq> ToWorkSeqOneHand(IEnumerable<WorkItem> workList, int inc = 100)
        {
            var gIndex = 1;

            foreach (var gItem in workList.GroupBy(t => t.GorupKey))
            {
                foreach (var sItem in gItem.SelectMany((t, i) => t.ToWorkSeqOneHand(gIndex * inc, i)).GroupBy(t => t.GroupNo).Select(t => t.ToArray()))
                {
                    foreach (var item in sItem)
                    {
                        yield return item;
                    }
                }

                gIndex++;
            }
        }

        public static IEnumerable<WorkSeq> ToWorkSeqFix(IEnumerable<WorkItem> workList, int inc = 100)
        {
            var gIndex = 1;

            foreach (var gItem in workList.GroupBy(t => t.GorupKey))
            {
                var count = gItem.Count();

                foreach (var sItem in gItem.SelectMany((t, i) => t.ToWorkSeqFix(gIndex * inc, i, count)).GroupBy(t => t.GroupNo).Select(t => t.ToArray()))
                {
                    foreach (var item in sItem)
                    {
                        yield return item;
                    }
                }

                gIndex++;
            }
        }
    }
}
