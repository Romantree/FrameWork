using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using TS.FW.GIGA.Models.Setup;
using TS.FW.GIGA.Views.Pages.Setup;
using TS.FW.Wpf.v2.Helpers;

namespace TS.FW.GIGA.ViewModels.Pages.Setup
{
    public class SetupMotionUvwViewMdoel : ISetupViewModel
    {
        private readonly FrameworkElement view = new SetupMotionView();

        public override int No => 2;

        public override string Name => "UVW (M)";

        public override FrameworkElement View => view;

        public override Visual Icon => ResourceHelper.Ins["appbar_server", "Icons"] as Visual;

        public MotionModel Motion { get; set; } = new MotionModel();

        public override void Init()
        {
            try
            {
                base.Init();

                Motion.Axis.Add(new AxisModel(eAxis.UVW_01));
                Motion.Axis.Add(new AxisModel(eAxis.UVW_02));
                Motion.Axis.Add(new AxisModel(eAxis.UVW_03));
                Motion.Axis.Add(new AxisModel(eAxis.UVW_04));

                Motion.OnSelectAxisCmd.Execute(Motion.Axis.FirstOrDefault());
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public override void Update()
        {
            try
            {
                Motion.Update();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
