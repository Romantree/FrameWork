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
    public class ImprintRecipe : IUintRecipe
    {
        [DataMember]
        public int D { get; set; }

        public ImprintRecipe() : base(UnitRecipeType.Imprint)
        {

        }

        public static implicit operator ImprintRecipeModel(ImprintRecipe item)
        {
            return new ImprintRecipeModel()
            {
                Name = item.Name,
                D = item.D
            };
        }

        public static implicit operator ImprintRecipe(ImprintRecipeModel item)
        {
            return new ImprintRecipe()
            {
                Name = item.Name,
                D = item.D
            };
        }
    }
}
