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
    public class CfgSystemViewModel : IConfigViewModel
    {
        private readonly FrameworkElement view = new CfgSystemView();

        public override int No => 0;

        public override string Name => "System";

        public override FrameworkElement View => view;

        public override Visual Icon => ResourceHelper.Ins["ts_System", "TsIcon"] as Visual;
    }
}
