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
    public class CenteringRecipe : IUintRecipe
    {
        [DataMember]
        public int B { get; set; }

        public CenteringRecipe() : base(UnitRecipeType.Centering)
        {

        }

        public static implicit operator CenteringRecipeModel(CenteringRecipe item)
        {
            return new CenteringRecipeModel()
            {
                Name = item.Name,
                B = item.B
            };
        }

        public static implicit operator CenteringRecipe(CenteringRecipeModel item)
        {
            return new CenteringRecipe()
            {
                Name = item.Name,
                B = item.B
            };
        }
    }
}
