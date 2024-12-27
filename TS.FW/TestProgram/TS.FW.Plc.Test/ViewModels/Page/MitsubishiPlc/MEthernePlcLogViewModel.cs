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
    public class MEthernePlcLogViewModel : IMEtherneMenuViewModel
    {
        private readonly MEthernePlcLogView _view = new MEthernePlcLogView();

        public override int No => 4;

        public override string Name => "PLC 로그";

        public override ContentControl View => _view;
    }
}
