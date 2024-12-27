using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TS.FW.Test.Config;
using TS.FW.Test.Models;
using TS.FW.Wpf.Controls;
using TS.FW.Wpf.Converters;
using TS.FW.Wpf.Core;

namespace TS.FW.Test.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        ConfigSetting Setting = new ConfigSetting();

        public LoginModel Login { get => this.GetValue<LoginModel>(); set => this.SetValue(value); }
        //public ObservableCollection<string> Lists { get; set; } = new ObservableCollection<string>();
        
        public ObservableCollection<RecipeModel> RecipeList { get => this.GetValue<ObservableCollection<RecipeModel>>(); set => this.SetValue(value); }
        public RecipeModel Recipe { get => this.GetValue<RecipeModel>(); set => this.SetValue(value); }

        public eIconType ToggleIcon { get => this.GetValue<eIconType>(); set => this.SetValue(value); }
        public bool LoginLock { get => this.GetValue<bool>();
            set
            {
                this.SetValue(value);

                Setting.Login.IsLock = value;
            }
        }

        public MainViewModel()
        {
            OnOffColorConverter.InitColorList(this.ToOnOffColor().ToArray());

            //Login = new LoginModel();

            //Setting.Login.Name = "Thinksoft";
            //Setting.Login.Age = 5;
            //Setting.Login.Data = 8.77059;

            Login = Setting.Login;
            LoginLock = Login.IsLock;

            RecipeList = new ObservableCollection<RecipeModel>();
            var tmp = Setting.GetRecipeList();
            if (tmp != null)
            {
                foreach (var item in tmp)
                    RecipeList.Add(item);
            }

            Recipe = new RecipeModel();
        }

        protected override void OnNotifyCommand(object commandParameter)
        {
            try
            {
                switch (commandParameter as string)
                {
                    case "SAVE":
                        {
                            //RecipeList.Clear();
                            var rcp = RecipeList.FirstOrDefault(t => t.Name == Recipe.Name);

                            if (rcp != null)
                                rcp = Recipe;
                            else
                            {
                                var item = Recipe.Clone() as RecipeModel;
                                RecipeList.Add(item);
                            }

                            var lists = RecipeList.ToList();
                            Setting.SetRecipeList(lists);
                            var tmp = Setting.GetRecipeList();
                        }
                        break;

                    case "DELETE":
                        {
                            var rcp = RecipeList.FirstOrDefault(t => t.Name == Recipe.Name);
                            if (rcp != null)
                            {
                                RecipeList.Remove(rcp);
                                Recipe = new RecipeModel();
                            }

                            Setting.SetRecipeList(RecipeList.ToList());
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        protected override void SetValue(object value, [CallerMemberName] string propertyName = null)
        {
            try
            {
                base.SetValue(value, propertyName);
            }
            finally
            {
                if (string.Equals(propertyName, nameof(this.LoginLock)))
                {
                    this.ToggleIcon = (bool)value ? eIconType.Lock : eIconType.Unlock;
                }
            }
        }

        private IEnumerable<OnOffColor> ToOnOffColor()
        {
            yield return OnOffColor.ToSolidColor("ALARM", Colors.Red, Colors.Lime);
            yield return OnOffColor.ToSolidColor("LAMP", Colors.Lime, Colors.DimGray);

            yield return OnOffColor.ToLinearColor("LOCK", Colors.Lime, Colors.WhiteSmoke, Colors.DimGray, Colors.WhiteSmoke);
        }
    }
}
