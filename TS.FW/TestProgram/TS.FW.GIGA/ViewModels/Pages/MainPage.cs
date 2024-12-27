using System;
using System.Windows;
using System.Windows.Media;
using TS.FW.GIGA.ViewModels.Pages.Alarm;
using TS.FW.GIGA.ViewModels.Pages.Utility;
using TS.FW.GIGA.Views.Pages;
using TS.FW.Wpf.v2.Controls.InPut;
using TS.FW.Wpf.v2.Helpers;
using TS.FW.Wpf.v2.Subscribe;

namespace TS.FW.GIGA.ViewModels.Pages
{
    public class PgDashViewModel : IMainPageViewModel
    {
        private readonly FrameworkElement view = new MainPageView();

        public override int No => 0;

        public override string Name => "Dash Board";

        public MainPageSelecter<IDashViewModel> Menu { get; set; } = new MainPageSelecter<IDashViewModel>();

        public override Visual Icon => ResourceHelper.Ins["appbar_monitor", "Icons"] as Visual;

        public override FrameworkElement View => view;
    }

    public class PgRcpViewModel : IMainPageViewModel
    {
        private readonly FrameworkElement view = new MainPageView();

        public override int No => 1;

        public override string Name => "Recipe";

        public MainPageSelecter<IRcpViewModel> Menu { get; set; } = new MainPageSelecter<IRcpViewModel>();

        public override Visual Icon => ResourceHelper.Ins["appbar_page_edit", "Icons"] as Visual;

        public override FrameworkElement View => view;

        public PgRcpViewModel()
        {
            AP.System.OnLoginModeChangedEvent += System_OnLoginModeChangedEvent;
        }

        private void System_OnLoginModeChangedEvent(object sender, LoginMode e)
        {
            try
            {
                var data = DB.User.ToUserData(e);

                this.IsEnabled = data.Recipe;
                this.Visibility = data.Recipe ? Visibility.Visible : Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }

    public class PgSvcViewModel : IMainPageViewModel
    {
        private readonly FrameworkElement view = new MainPageView();

        public override int No => 2;

        public override string Name => "Service";

        public MainPageSelecter<ISvcViewModel> Menu { get; set; } = new MainPageSelecter<ISvcViewModel>();

        public override Visual Icon => ResourceHelper.Ins["appbar_stock", "Icons"] as Visual;

        public override FrameworkElement View => view;

        public PgSvcViewModel()
        {
            AP.System.OnLoginModeChangedEvent += System_OnLoginModeChangedEvent;
        }

        private void System_OnLoginModeChangedEvent(object sender, LoginMode e)
        {
            try
            {
                var data = DB.User.ToUserData(e);

                this.IsEnabled = data.Service;
                this.Visibility = data.Service ? Visibility.Visible : Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }

    public class PgConfigViewModel : IMainPageViewModel
    {
        private readonly FrameworkElement view = new MainPageView();

        public override int No => 3;

        public override string Name => "Config";

        public MainPageSelecter<IConfigViewModel> Menu { get; set; } = new MainPageSelecter<IConfigViewModel>();

        public override Visual Icon => ResourceHelper.Ins["appbar_cogs", "Icons"] as Visual;

        public override FrameworkElement View => view;

        public PgConfigViewModel()
        {
            AP.System.OnLoginModeChangedEvent += System_OnLoginModeChangedEvent;
        }

        private void System_OnLoginModeChangedEvent(object sender, LoginMode e)
        {
            try
            {
                var data = DB.User.ToUserData(e);

                this.IsEnabled = data.Config;
                this.Visibility = data.Config ? Visibility.Visible : Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }

    public class PgUtilViewModel : IMainPageViewModel
    {
        private readonly FrameworkElement view = new MainPageView();

        public override int No => 4;

        public override string Name => "Utilty";

        public MainPageSelecter<IUtilViewModel> Menu { get; set; } = new MainPageSelecter<IUtilViewModel>();

        public override Visual Icon => ResourceHelper.Ins["appbar_page_powerpoint", "Icons"] as Visual;

        public override FrameworkElement View => view;

        public PgUtilViewModel()
        {
            AP.System.OnLoginModeChangedEvent += System_OnLoginModeChangedEvent;
        }

        private void System_OnLoginModeChangedEvent(object sender, LoginMode e)
        {
            try
            {
                var data = DB.User.ToUserData(e);

                this.IsEnabled = data.Utilty;
                this.Visibility = data.Utilty ? Visibility.Visible : Visibility.Collapsed;

                foreach (var item in Menu.SubMenuList)
                {
                    if(item is UtUserViewModel) item.IsEnabled = e == LoginMode.Programmer || e == LoginMode.Engineer;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }

    public class PgSetupViewModel : IMainPageViewModel
    {
        private readonly FrameworkElement view = new MainPageView();

        public override int No => 5;

        public override string Name => "Setup";

        public MainPageSelecter<ISetupViewModel> Menu { get; set; } = new MainPageSelecter<ISetupViewModel>();

        public override Visual Icon => ResourceHelper.Ins["appbar_cog", "Icons"] as Visual;

        public override FrameworkElement View => view;

        public PgSetupViewModel()
        {
            AP.System.OnLoginModeChangedEvent += System_OnLoginModeChangedEvent;
        }

        private void System_OnLoginModeChangedEvent(object sender, LoginMode e)
        {
            try
            {
                var data = DB.User.ToUserData(e);

                this.IsEnabled = data.Setup;
                this.Visibility = data.Setup ? Visibility.Visible : Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }

    public class PgAlarmViewModel : IMainPageViewModel
    {
        private readonly FrameworkElement view = new MainPageView();

        public override int No => 99;

        public override string Name => "Alarm";

        public MainPageSelecter<IAlarmViewModel> Menu { get; set; } = new MainPageSelecter<IAlarmViewModel>();

        public override Visual Icon => ResourceHelper.Ins["appbar_alert", "Icons"] as Visual;

        public override FrameworkElement View => view;

        public PgAlarmViewModel()
        {
            this.Visibility = Visibility.Hidden;
            AP.System.OnLoginModeChangedEvent += System_OnLoginModeChangedEvent;
        }

        private void System_OnLoginModeChangedEvent(object sender, LoginMode e)
        {
            try
            {
                var data = DB.User.ToUserData(e);

                foreach (var item in Menu.SubMenuList)
                {
                    if (item is AlarmMainViewModel) continue;

                    item.IsEnabled = data.Alarm;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
