using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using TS.FW.GIGA.Models.Setup;
using TS.FW.GIGA.Views.Pages.Setup;
using TS.FW.Wpf.v2.Helpers;

namespace TS.FW.GIGA.ViewModels.Pages.Setup
{
    public class SetupMotionStageViewMdoel : ISetupViewModel
    {
        private readonly FrameworkElement view = new SetupMotionView();

        public override int No => 0;

        public override string Name => "Stage (M)";

        public override FrameworkElement View => view;

        public override Visual Icon => ResourceHelper.Ins["appbar_server", "Icons"] as Visual;

        public MotionModel Motion { get; set; } = new MotionModel();

        public override void Init()
        {
            try
            {
                base.Init();

                Motion.Axis.Add(new AxisModel(eAxis.UNWINDER));
                Motion.Axis.Add(new AxisModel(eAxis.REWINDER));
                Motion.Axis.Add(new AxisModel(eAxis.COVER_FILM));
                Motion.Axis.Add(new AxisModel(eAxis.NIP_ROLL));
                Motion.Axis.Add(new AxisModel(eAxis.DE_NIP_ROLL));
                Motion.Axis.Add(new AxisModel(eAxis.STAGE_X, eAxis.STAGE_X_S));
                Motion.Axis.Add(new AxisModel(eAxis.GAP_PRESS_LEFT));
                Motion.Axis.Add(new AxisModel(eAxis.GAP_PRESS_RIGHT));
                Motion.Axis.Add(new AxisModel(eAxis.FILM_BALANCE_01));
                Motion.Axis.Add(new AxisModel(eAxis.FILM_BALANCE_02));
                Motion.Axis.Add(new AxisModel(eAxis.FILM_GRIP_01));
                Motion.Axis.Add(new AxisModel(eAxis.FILM_GRIP_02));

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
