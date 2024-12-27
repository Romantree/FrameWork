using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Dac.Cfg;

namespace TS.FW.Test.Config
{
    public class ConfigSetting : IConfigDb
    {
        public LoginDb Login { get; set; } = new LoginDb();
        public ParamDb Param { get; set; } = new ParamDb();

        public List<RecipeModel> GetRecipeList()
        {
            return this.GetValueClass<List<RecipeModel>>("RecipeList");
        }

        public void SetRecipeList(List<RecipeModel> item)
        {
            this.SetValue(item, "RecipeList");
        }
    }
}
