using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.CTC.Test.Models
{
    public class FlowRecipeModel
    {
        public string Name { get; set; }

        public List<IUintRecipeModel> UnitList { get; set; } = new List<IUintRecipeModel>();
    }
}
