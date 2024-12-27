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
    public class AlignRecipe : IUintRecipe
    {
        [DataMember]
        public int A { get; set; }

        public AlignRecipe() : base(UnitRecipeType.Align)
        {

        }

        public static implicit operator AlignRecipeModel(AlignRecipe item)
        {
            return new AlignRecipeModel()
            {
                Name = item.Name,
                A = item.A
            };
        }

        public static implicit operator AlignRecipe(AlignRecipeModel item)
        {
            return new AlignRecipe()
            {
                Name = item.Name,
                A = item.A
            };
        }
    }
}
