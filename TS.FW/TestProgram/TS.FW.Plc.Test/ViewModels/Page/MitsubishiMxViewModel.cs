using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using TS.FW.Plc.Test.Views.Page;
using TS.FW.Wpf.Controls;
using TS.FW.Wpf.Helper;

namespace TS.FW.Plc.Test.ViewModels.Page
{
    public class MitsubishiMxViewModel : IMainMenuViewModel
    {
        private readonly MitsubishiMxView _view = new MitsubishiMxView();

        public override int No => 1;

        public override string Name => "미쯔비시 - PLC (MX)";

        public override string Title => "Mitsubishi";

        public override string SubTitle => "PLC MX Component";

        public override ContentControl View => _view;

        public override Visual Visual => ResourceHelper.ToVisual("appbar_scrabble_m", "Resources/Icons.xaml");
    }
}
