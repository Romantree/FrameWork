using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using TS.FW.Wpf.Controls;
using TS.FW.Wpf.Core;
using TS.FW.Wpf.Test.Views.Page;

namespace TS.FW.Wpf.Test.ViewModels.Page
{
    public class SetingViewModel : IMainMenuViewModel
    {
        private readonly SetingView _view = new SetingView();

        public override int No => 3;

        public override string Name => "Setting";

        public override ContentControl View => _view;

        public override eIconType Icon => eIconType.Settings;

        public List<ISetingMenuModel> MenuList { get; set; } = new List<ISetingMenuModel>();

        public ISetingMenuModel SelectedMenu { get => this.GetValue<ISetingMenuModel>(); set => this.SetValue(value); }

        public override void Init()
        {
            try
            {
                foreach (var item in IMenuViewModel.ToMenuViewModelList<ISetingMenuModel>())
                {
                    item.Init();

                    this.MenuList.Add(item);
                }

                this.SelectedMenu = this.MenuList.First();

                base.Init();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }

    public abstract class ISetingMenuModel : IMenuViewModel
    {
        protected Label3D _view;
        protected readonly string _name = "Setting Menu {0}";

        public ISetingMenuModel(Color color)
        {
            _view = new Label3D() { Background = new SolidColorBrush(color), Content = this.Name, FontSize = 45 };
        }
    }

    public class SetingMenuModel_1 : ISetingMenuModel
    {
        public override int No => 1;

        public override string Name => string.Format(this._name, this.No);

        public override ContentControl View => _view;

        public SetingMenuModel_1() : base(Colors.Red) { }
    }

    public class SetingMenuModel_2 : ISetingMenuModel
    {
        public override int No => 2;

        public override string Name => string.Format(this._name, this.No);

        public override ContentControl View => _view;

        public SetingMenuModel_2() : base(Colors.Blue) { }
    }

    public class SetingMenuModel_3 : ISetingMenuModel
    {
        public override int No => 3;

        public override string Name => string.Format(this._name, this.No);

        public override ContentControl View => _view;

        public SetingMenuModel_3() : base(Colors.BlueViolet) { }
    }

    public class SetingMenuModel_4 : ISetingMenuModel
    {
        public override int No => 4;

        public override string Name => string.Format(this._name, this.No);

        public override ContentControl View => _view;

        public SetingMenuModel_4() : base(Colors.Brown) { }
    }

    public class SetingMenuModel_5 : ISetingMenuModel
    {
        public override int No => 5;

        public override string Name => string.Format(this._name, this.No);

        public override ContentControl View => _view;

        public SetingMenuModel_5() : base(Colors.DarkGoldenrod) { }
    }
}
