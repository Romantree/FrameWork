using System.Windows;
using System.Windows.Media;
using TS.FW.GIGA.Views.Pages.DashBoard;
using TS.FW.Wpf.v2.Core;
using TS.FW.Wpf.v2.Helpers;

namespace TS.FW.GIGA.ViewModels.Pages.DashBoard
{
    public class DashMainViewModel : IDashViewModel
    {
        private readonly FrameworkElement view = new DashMainView();

        public override int No => 0;

        public override string Name => "Main";

        public override FrameworkElement View => view;

        public override Visual Icon => ResourceHelper.Ins["appbar_clipboard", "Icons"] as Visual;

        public TestModel TEST { get; set; } = new TestModel();
    }

    public class TestModel : IModel
    {
        public bool IsTest { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool IsTest1 { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public TestModel()
        {
            this.IsTest = true;
            this.IsTest1 = false;
        }
    }
}
