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
    public class LotRecipe
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<FlowRecipe> SlotList { get; set; } = new List<FlowRecipe>();

        public static LotRecipe ToTest()
        {
            var item = new LotRecipe() { Name = string.Format("TEST_{0}", DateTime.Now.ToString("yyyyMMddHHmmss")) };

            for (int i = 0; i < 30; i++)
            {
                var flow = new FlowRecipe();
                flow.Name = "TEST";

                //if (i == 2)
                //{

                //}
                //else if (i % 2 == 1)
                //{
                //    flow.UnitList.Add(new CenteringRecipe() { Name = "TEST", B = 2 });
                //    flow.UnitList.Add(new AlignRecipe() { Name = "TEST", A = 1 });
                //    flow.UnitList.Add(new CotterRecipe() { Name = "TEST", C = 3 });
                //    //flow.UnitList.Add(new ImprintRecipe() { Name = "TEST" });
                //}
                //else if (i % 3 == 2)
                //{
                //    flow.UnitList.Add(new CenteringRecipe() { Name = "TEST", B = 2 });
                //    flow.UnitList.Add(new CotterRecipe() { Name = "TEST", C = 3 });
                //}
                //else
                {
                    flow.UnitList.Add(new AlignRecipe() { Name = "TEST", A = 1 });
                    flow.UnitList.Add(new CenteringRecipe() { Name = "TEST", B = 2 });
                    flow.UnitList.Add(new CotterRecipe() { Name = "TEST", C = 3 });
                    //flow.UnitList.Add(new ImprintRecipe() { Name = "TEST" });
                }


                item.SlotList.Add(flow);
            }

            return item;
        }

        public static implicit operator LotRecipeModel(LotRecipe item)
        {
            var data = new LotRecipeModel()
            {
                Name = item.Name,
            };

            foreach (var slot in item.SlotList)
            {
                data.SlotList.Add(slot);
            }

            return data;
        }

        public static implicit operator LotRecipe(LotRecipeModel item)
        {
            var data = new LotRecipe()
            {
                Name = item.Name,
            };

            foreach (var slot in item.SlotList)
            {
                data.SlotList.Add(slot);
            }

            return data;
        }
    }
}
