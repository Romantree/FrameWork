using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using TS.FW.GIGA.Models.Setup;
using TS.FW.GIGA.Views.Pages.Setup;
using TS.FW.Wpf.v2.Helpers;

namespace TS.FW.GIGA.ViewModels.Pages.Setup
{
    public class SetupMotionCameraViewMdoel : ISetupViewModel
    {
        private readonly FrameworkElement view = new SetupMotionView();

        public override int No => 1;

        public override string Name => "Camera (M)";

        public override FrameworkElement View => view;

        public override Visual Icon => ResourceHelper.Ins["appbar_server", "Icons"] as Visual;

        public MotionModel Motion { get; set; } = new MotionModel();

        public override void Init()
        {
            try
            {
                base.Init();

                Motion.Axis.Add(new AxisModel(eAxis.CAMERA_T_X_01));
                Motion.Axis.Add(new AxisModel(eAxis.CAMERA_T_Y_01));
                Motion.Axis.Add(new AxisModel(eAxis.CAMERA_T_Z_01));
                Motion.Axis.Add(new AxisModel(eAxis.CAMERA_T_X_02));
                Motion.Axis.Add(new AxisModel(eAxis.CAMERA_T_Y_02));
                Motion.Axis.Add(new AxisModel(eAxis.CAMERA_T_Z_02));
                Motion.Axis.Add(new AxisModel(eAxis.CAMERA_B_X_01));
                Motion.Axis.Add(new AxisModel(eAxis.CAMERA_B_Y_01));
                Motion.Axis.Add(new AxisModel(eAxis.CAMERA_B_Z_01));
                Motion.Axis.Add(new AxisModel(eAxis.CAMERA_B_X_02));
                Motion.Axis.Add(new AxisModel(eAxis.CAMERA_B_Y_02));
                Motion.Axis.Add(new AxisModel(eAxis.CAMERA_B_Z_02));

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
