using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.CTC.Test.Models
{
    public class AlignRecipeModel : IUintRecipeModel
    {
        public int A { get; set; }

        public AlignRecipeModel() : base(UnitRecipeType.Align)
        {

        }
    }
}
