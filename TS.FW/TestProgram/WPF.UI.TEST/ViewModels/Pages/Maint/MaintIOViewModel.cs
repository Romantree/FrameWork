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
    public class MaintIOViewModel : IMaintViewModel
    {
        private readonly FrameworkElement view = new MaintIOView();

        public override int No => 2;

        public override string Name => "I/O";

        public override FrameworkElement View => view;

        public override Visual Icon => ResourceHelper.Ins["ts_IO", "TsIcon"] as Visual;
    }
}
