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
    public class ManualRunViewModel : IMainMenuViewModel
    {
        private readonly ManualRunView _view = new ManualRunView();

        public override int No => 2;

        public override string Name => "Manual Run";

        public override ContentControl View => _view;

        public override eIconType Icon => eIconType.Cabinet;

        public List<IMenuViewModel> MenuList { get; set; } = new List<IMenuViewModel>();

        public IMenuViewModel SelectedMenu { get => this.GetValue<IMenuViewModel>(); set => this.SetValue(value); }

        public override void Init()
        {
            try
            {
                foreach (var item in IMenuViewModel.ToMenuViewModelList<IManualRunMenuModel>())
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

    public abstract class IManualRunMenuModel : IMenuViewModel
    {
        protected static Label3D ToView(int no, Color color)
        {
            return new Label3D() { Background = new SolidColorBrush(color), Content = "Manual Menu " + no.ToString(), FontSize = 45 };
        }
    }

    public class ManualMenuModel_1 : IManualRunMenuModel
    {
        private Label3D _view = ToView(1, Colors.Red);

        public override int No => 1;

        public override string Name => "Manual Menu 1";

        public override ContentControl View => _view;
    }

    public class ManualMenuModel_2 : IManualRunMenuModel
    {
        private Label3D _view = ToView(2, Colors.Blue);

        public override int No => 2;

        public override string Name => "Manual Menu 2";

        public override ContentControl View => _view;
    }

    public class ManualMenuModel_3 : IManualRunMenuModel
    {
        private Label3D _view = ToView(3, Colors.BlueViolet);

        public override int No => 3;

        public override string Name => "Manual Menu 3";

        public override ContentControl View => _view;
    }

    public class ManualMenuModel_4 : IManualRunMenuModel
    {
        private Label3D _view = ToView(4, Colors.Brown);

        public override int No => 4;

        public override string Name => "Manual Menu 4";

        public override ContentControl View => _view;
    }

    public class ManualMenuModel_5 : IManualRunMenuModel
    {
        private Label3D _view = ToView(5, Colors.DarkGoldenrod);

        public override int No => 5;

        public override string Name => "Manual Menu 5";

        public override ContentControl View => _view;
    }
}
