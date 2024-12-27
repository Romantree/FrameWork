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
    public class LSPlcViewModel : IMainMenuViewModel
    {
        private readonly LSPlcView _view = new LSPlcView();

        public override int No => 3;

        public override string Name => "LS - PLC";

        public override string Title => "LS 산전";

        public override string SubTitle => "PLC";

        public override ContentControl View => _view;

        public override Visual Visual => ResourceHelper.ToVisual("appbar_scrabble_l", "Resources/Icons.xaml");
    }
}
