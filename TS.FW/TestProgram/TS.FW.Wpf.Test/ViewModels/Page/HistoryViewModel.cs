using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using TS.FW.Wpf.Controls;
using TS.FW.Wpf.Controls.Pu;
using TS.FW.Wpf.Core;
using TS.FW.Wpf.Helper;
using TS.FW.Wpf.Test.Views.Page;

namespace TS.FW.Wpf.Test.ViewModels.Page
{
    public class HistoryViewModel : IMainMenuViewModel
    {
        private readonly HistoryView _view = new HistoryView();

        public override int No => 3;

        public override string Name => "History";

        public override ContentControl View => _view;

        public override Visual Visual => ResourceHelper.ToVisual("appbar_folder_star", "Resources/Icons.xaml");

        public List<IHistoryMenuModel> MenuList { get; set; } = new List<IHistoryMenuModel>();

        public IHistoryMenuModel SelectedMenu { get => this.GetValue<IHistoryMenuModel>(); set => this.SetValue(value); }

        public List<int> List { get; set; } = new List<int>();

        public int Selected { get => this.GetValue<int>(); set => this.SetValue(value); }

        public HistoryViewModel()
        {
            for (int i = 0; i < 200; i++)
            {
                this.List.Add(i);
            }
        }

        public override void Init()
        {
            try
            {
                foreach (var item in IMenuViewModel.ToMenuViewModelList<IHistoryMenuModel>())
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

        protected override void OnNotifyCommand(object commandParameter)
        {
            this.Selected = NumberPad.Show(0);

            base.OnNotifyCommand(commandParameter);
        }
    }

    public abstract class IHistoryMenuModel : IMenuViewModel
    {
        protected Label3D _view;
        protected readonly string _name = "History Menu {0}";

        public IHistoryMenuModel(Color color)
        {
            _view = new Label3D() { Background = new SolidColorBrush(color), Content = this.Name, FontSize = 45 };
        }
    }

    public class HistoryMenuModel_1 : IHistoryMenuModel
    {
        public override int No => 1;

        public override string Name => string.Format(this._name, this.No);

        public override ContentControl View => _view;

        public HistoryMenuModel_1() : base(Colors.Red) { }
    }

    public class HistoryMenuModel_2 : IHistoryMenuModel
    {
        public override int No => 2;

        public override string Name => string.Format(this._name, this.No);

        public override ContentControl View => _view;

        public HistoryMenuModel_2() : base(Colors.Blue) { }
    }

    public class HistoryMenuModel_3 : IHistoryMenuModel
    {
        public override int No => 3;

        public override string Name => string.Format(this._name, this.No);

        public override ContentControl View => _view;

        public HistoryMenuModel_3() : base(Colors.BlueViolet) { }
    }

    public class HistoryMenuModel_4 : IHistoryMenuModel
    {
        public override int No => 4;

        public override string Name => string.Format(this._name, this.No);

        public override ContentControl View => _view;

        public HistoryMenuModel_4() : base(Colors.Brown) { }
    }

    public class HistoryMenuModel_5 : IHistoryMenuModel
    {
        public override int No => 5;

        public override string Name => string.Format(this._name, this.No);

        public override ContentControl View => _view;

        public HistoryMenuModel_5() : base(Colors.DarkGoldenrod) { }
    }
}
