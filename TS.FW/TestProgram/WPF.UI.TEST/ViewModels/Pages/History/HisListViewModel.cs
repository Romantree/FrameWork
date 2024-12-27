using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TS.FW.Wpf.v2.Helpers;
using WPF.UI.TEST.Views.Pages.DashBoard;
using WPF.UI.TEST.Views.Pages.History;

namespace WPF.UI.TEST.ViewModels.Pages.History
{
    public class HisListViewModel : IHistoryViewModel
    {
        private readonly FrameworkElement view = new HisListView();

        public override int No => 0;

        public override string Name => "List";

        public override FrameworkElement View => view;

        public override Visual Icon => ResourceHelper.Ins["ts_List", "TsIcon"] as Visual;
    }
}
