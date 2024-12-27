using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.CTC.Test.Dto;

namespace TS.FW.CTC.Test.Process.Work.Item
{
    public class WorkRecipe
    {
        public IUintRecipe Recipe { get; private set; }

        public UnitRecipeType Type => this.Recipe.Type;

        public AlignRecipe Align => this.Recipe as AlignRecipe;

        public CenteringRecipe Centering => this.Recipe as CenteringRecipe;

        public CotterRecipe Cotter => this.Recipe as CotterRecipe;

        public ImprintRecipe Imprint => this.Recipe as ImprintRecipe;

        public bool Complete { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public WorkRecipe(IUintRecipe recipe)
        {
            this.Recipe = recipe;
            this.Complete = false;
        }

        public override string ToString()
        {
            return string.Format("{0}[{1}]", this.Type, this.Complete);
        }
    }
}
