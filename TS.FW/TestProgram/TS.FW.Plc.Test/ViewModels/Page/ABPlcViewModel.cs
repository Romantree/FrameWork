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
    public class ABPlcViewModel : IMainMenuViewModel
    {
        private readonly ABPlcView _view = new ABPlcView();

        public override int No => 2;

        public override string Name => "AB - PLC";

        public override string Title => "Allen Bradley";

        public override string SubTitle => "PLC";

        public override ContentControl View => _view;

        public override Visual Visual => ResourceHelper.ToVisual("appbar_scrabble_a", "Resources/Icons.xaml");
    }
}
