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
    public class SiemensPlcViewModel : IMainMenuViewModel
    {
        private readonly SiemensPlcView _view = new SiemensPlcView();

        public override int No => 5;

        public override string Name => "Siemens - PLC";

        public override string Title => "Siemens";

        public override string SubTitle => "PLC";

        public override ContentControl View => _view;

        public override Visual Visual => ResourceHelper.ToVisual("appbar_scrabble_s", "Resources/Icons.xaml");
    }
}
