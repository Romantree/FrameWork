using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TS.FW.Wpf.v2.Helpers;
using WPF.UI.TEST.Views.Pages.Config;
using WPF.UI.TEST.Views.Pages.DashBoard;

namespace WPF.UI.TEST.ViewModels.Pages.Config
{
    public class CfgUserViewModel : IConfigViewModel
    {
        private readonly FrameworkElement view = new CfgUserView();

        public override int No => 1;

        public override string Name => "User";

        public override FrameworkElement View => view;

        public override Visual Icon => ResourceHelper.Ins["ts_User", "TsIcon"] as Visual;
    }
}
