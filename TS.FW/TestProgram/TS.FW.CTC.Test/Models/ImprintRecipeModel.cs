using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.CTC.Test.Models
{
    public class ImprintRecipeModel : IUintRecipeModel
    {
        public int D { get; set; }

        public ImprintRecipeModel() : base(UnitRecipeType.Imprint)
        {

        }
    }
}
