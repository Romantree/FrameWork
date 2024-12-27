using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TS.FW.Plc.Test.Views.Page.MitsubishiPlc;

namespace TS.FW.Plc.Test.ViewModels.Page.MitsubishiPlc
{
    public class MEtherneSVMonitoringViewModel : IMEtherneMenuViewModel
    {
        private readonly MEtherneSVMonitoringView _view = new MEtherneSVMonitoringView();

        public override int No => 3;

        public override string Name => "SV 모니터링";

        public override ContentControl View => _view;
    }
}
