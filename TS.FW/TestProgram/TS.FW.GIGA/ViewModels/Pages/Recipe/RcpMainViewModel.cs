using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TS.FW.Wpf.v2.Helpers;

namespace TS.FW.GIGA.ViewModels.Pages.Recipe
{
    public class RcpMainViewModel : IRcpViewModel
    {
        private readonly FrameworkElement view = new FrameworkElement();

        public override int No => 0;

        public override string Name => "Main";

        public override FrameworkElement View => view;

        public override Visual Icon => ResourceHelper.Ins["appbar_clipboard", "Icons"] as Visual;

        public override void Show()
        {
            base.Show();
        }
    }
}
