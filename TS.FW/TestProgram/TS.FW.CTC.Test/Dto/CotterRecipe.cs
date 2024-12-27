using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TS.FW.CTC.Test.Models;

namespace TS.FW.CTC.Test.Dto
{
    [DataContract]
    public class CotterRecipe : IUintRecipe
    {
        [DataMember]
        public int C { get; set; }

        public CotterRecipe() : base(UnitRecipeType.Cotter)
        {

        }

        public static implicit operator CotterRecipeModel(CotterRecipe item)
        {
            return new CotterRecipeModel()
            {
                Name = item.Name,
                C = item.C
            };
        }

        public static implicit operator CotterRecipe(CotterRecipeModel item)
        {
            return new CotterRecipe()
            {
                Name = item.Name,
                C = item.C
            };
        }
    }
}
