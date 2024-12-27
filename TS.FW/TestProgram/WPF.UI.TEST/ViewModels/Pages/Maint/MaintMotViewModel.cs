using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TS.FW.Wpf.v2.Helpers;
using WPF.UI.TEST.Views.Pages.DashBoard;
using WPF.UI.TEST.Views.Pages.Maint;

namespace WPF.UI.TEST.ViewModels.Pages.Maint
{
    public class MaintMotViewModel : IMaintViewModel
    {
        private readonly FrameworkElement view = new MaintMotView();

        public override int No => 1;

        public override string Name => "Motion";

        public override FrameworkElement View => view;

        public override Visual Icon => ResourceHelper.Ins["ts_Motion", "TsIcon"] as Visual;
    }
}
