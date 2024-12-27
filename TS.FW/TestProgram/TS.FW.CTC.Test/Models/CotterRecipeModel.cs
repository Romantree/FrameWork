using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.CTC.Test.Models
{
    public class CotterRecipeModel : IUintRecipeModel
    {
        public int C { get; set; }

        public CotterRecipeModel() : base(UnitRecipeType.Cotter)
        {

        }
    }
}
