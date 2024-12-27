using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.CTC.Test.Models
{
    public abstract class IUintRecipeModel
    {
        public UnitRecipeType Type { get; private set; }

        public string Name { get; set; }

        public IUintRecipeModel(UnitRecipeType type)
        {
            this.Type = type;
        }
    }
}
