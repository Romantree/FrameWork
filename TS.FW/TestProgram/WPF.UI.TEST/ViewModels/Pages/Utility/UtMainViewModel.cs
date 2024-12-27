using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TS.FW.Wpf.v2.Helpers;
using WPF.UI.TEST.Views.Pages.DashBoard;
using WPF.UI.TEST.Views.Pages.Utility;

namespace WPF.UI.TEST.ViewModels.Pages.Utility
{
    public class UtMainViewModel : IUtilityViewModel
    {
        private readonly FrameworkElement view = new UtMainView();

        public override int No => 0;

        public override string Name => "Main";

        public override FrameworkElement View => view;

        public override Visual Icon => ResourceHelper.Ins["appbar_city_chicago", "Icons"] as Visual;
    }
}
