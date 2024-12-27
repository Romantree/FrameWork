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
    public class FlowRecipe
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<IUintRecipe> UnitList { get; set; } = new List<IUintRecipe>();

        public static implicit operator FlowRecipeModel(FlowRecipe item)
        {
            var data = new FlowRecipeModel()
            {
                Name = item.Name,
            };

            foreach (var unit in item.UnitList)
            {
                data.UnitList.Add(unit);
            }

            return data;
        }

        public static implicit operator FlowRecipe(FlowRecipeModel item)
        {
            var data = new FlowRecipe()
            {
                Name = item.Name
            };

            foreach (var unit in item.UnitList)
            {
                data.UnitList.Add(unit);
            }

            return data;
        }
    }
}
