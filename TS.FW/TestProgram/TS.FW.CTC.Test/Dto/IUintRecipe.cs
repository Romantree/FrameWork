using Newtonsoft.Json;
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
    [KnownType(typeof(ImprintRecipe))]
    [KnownType(typeof(CotterRecipe))]
    [KnownType(typeof(CenteringRecipe))]
    [KnownType(typeof(AlignRecipe))]    
    public abstract class IUintRecipe
    {
        [DataMember]
        public UnitRecipeType Type { get; private set; }

        [DataMember]
        public string Name { get; set; }

        public IUintRecipe(UnitRecipeType type)
        {
            this.Type = type;
        }

        public static implicit operator IUintRecipeModel(IUintRecipe item)
        {
            switch (item.Type)
            {
                case UnitRecipeType.Align: return (AlignRecipeModel)(AlignRecipe)item;
                case UnitRecipeType.Centering: return (CenteringRecipeModel)(CenteringRecipe)item;
                case UnitRecipeType.Cotter: return (CotterRecipeModel)(CotterRecipe)item;
                case UnitRecipeType.Imprint: return (ImprintRecipeModel)(ImprintRecipe)item;
                default: return null;
            }
        }

        public static implicit operator IUintRecipe(IUintRecipeModel item)
        {
            switch (item.Type)
            {
                case UnitRecipeType.Align: return (AlignRecipe)(AlignRecipeModel)item;
                case UnitRecipeType.Centering: return (CenteringRecipe)(CenteringRecipeModel)item;
                case UnitRecipeType.Cotter: return (CotterRecipe)(CotterRecipeModel)item;
                case UnitRecipeType.Imprint: return (ImprintRecipe)(ImprintRecipeModel)item;
                default: return null;
            }
        }
    }
}
