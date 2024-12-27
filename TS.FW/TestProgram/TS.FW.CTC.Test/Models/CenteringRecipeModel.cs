using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.CTC.Test.Models
{
    public class CenteringRecipeModel : IUintRecipeModel
    {
        public int B { get; set; }

        public CenteringRecipeModel() : base(UnitRecipeType.Centering)
        {

        }
    }
}
