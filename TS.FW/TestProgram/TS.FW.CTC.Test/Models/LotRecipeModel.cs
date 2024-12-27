using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.CTC.Test.Models
{
    public class LotRecipeModel
    {
        public string Name { get; set; }

        public List<FlowRecipeModel> SlotList { get; set; } = new List<FlowRecipeModel>();
    }
}
