using System.Windows;
using System.Windows.Media;
using TS.FW.Wpf.v2.Helpers;
using TS.FW.Wpf.v2.Subscribe;
using WPF.UI.TEST.Views.Pages;

namespace WPF.UI.TEST.ViewModels.Pages
{
    public class PgDashViewModel : IMainPageViewModel
    {
        private readonly FrameworkElement view = new MainPageView();

        public override int No => 0;

        public override string Name => "Dash Board";

        public MainPageSelecter<IDashViewModel> Menu { get; set; } = new MainPageSelecter<IDashViewModel>();

        public override Visual Icon => ResourceHelper.Ins["ts_DashBoard", "TsIcon"] as Visual;

        public override FrameworkElement View => view;
    }

    public class PgRecipeViewModel : IMainPageViewModel
    {
        private readonly FrameworkElement view = new MainPageView();

        public override int No => 1;

        public override string Name => "Recipe";

        public MainPageSelecter<IRecipeViewModel> Menu { get; set; } = new MainPageSelecter<IRecipeViewModel>();

        public override Visual Icon => ResourceHelper.Ins["ts_Recipe", "TsIcon"] as Visual;

        public override FrameworkElement View => view;
    }

    public class PgMaintViewModel : IMainPageViewModel
    {
        private readonly FrameworkElement view = new MainPageView();

        public override int No => 2;

        public override string Name => "Maint";

        public MainPageSelecter<IMaintViewModel> Menu { get; set; } = new MainPageSelecter<IMaintViewModel>();

        public override Visual Icon => ResourceHelper.Ins["ts_Maint", "TsIcon"] as Visual;

        public override FrameworkElement View => view;
    }

    public class PgConfigViewModel : IMainPageViewModel
    {
        private readonly FrameworkElement view = new MainPageView();

        public override int No => 3;

        public override string Name => "Config";

        public MainPageSelecter<IConfigViewModel> Menu { get; set; } = new MainPageSelecter<IConfigViewModel>();

        public override Visual Icon => ResourceHelper.Ins["ts_Config", "TsIcon"] as Visual;

        public override FrameworkElement View => view;
    }

    public class PgHistoryViewModel : IMainPageViewModel
    {
        private readonly FrameworkElement view = new MainPageView();

        public override int No => 4;

        public override string Name => "History";

        public MainPageSelecter<IHistoryViewModel> Menu { get; set; } = new MainPageSelecter<IHistoryViewModel>();

        public override Visual Icon => ResourceHelper.Ins["ts_History", "TsIcon"] as Visual;

        public override FrameworkElement View => view;
    }

    public class PgUtilityViewModel : IMainPageViewModel
    {
        private readonly FrameworkElement view = new MainPageView();

        public override int No => 5;

        public override string Name => "Utility";

        public MainPageSelecter<IUtilityViewModel> Menu { get; set; } = new MainPageSelecter<IUtilityViewModel>();

        public override Visual Icon => ResourceHelper.Ins["ts_Utility", "TsIcon"] as Visual;

        public override FrameworkElement View => view;
    }
}
