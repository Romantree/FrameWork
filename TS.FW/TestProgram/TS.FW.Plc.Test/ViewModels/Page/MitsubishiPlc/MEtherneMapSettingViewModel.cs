using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TS.FW.Plc.Test.Views.Page.MitsubishiPlc;

namespace TS.FW.Plc.Test.ViewModels.Page.MitsubishiPlc
{
    public class MEtherneMapSettingViewModel : IMEtherneMenuViewModel
    {
        private readonly MEtherneMapSettingView _view = new MEtherneMapSettingView();

        public override int No => 99;

        public override string Name => "PLC Map 설정";

        public override ContentControl View => _view;

        public override Visibility Visibility => Visibility.Collapsed;
    }
}
