using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TS.FW.Wpf.Controls
{
    /// <summary>
    /// ZoomControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ZoomControl : ContentControl
    {
        private static ControlProperty<ZoomControl> Property = new ControlProperty<ZoomControl>();

        public static readonly DependencyProperty IsMousePositionProperty = Property.ToProperty("IsMousePosition", typeof(bool), false);
        public static readonly DependencyProperty ScaleProperty = Property.ToProperty("Scale", typeof(double), 0D);
        public static readonly DependencyProperty CenterXProperty = Property.ToProperty("CenterX", typeof(double), 0D);
        public static readonly DependencyProperty CenterYProperty = Property.ToProperty("CenterY", typeof(double), 0D);

        public bool IsMousePosition { get => (bool)this.GetValue(IsMousePositionProperty); set => this.SetValue(IsMousePositionProperty, value); }

        public double Scale { get => (double)this.GetValue(ScaleProperty); set => this.SetValue(ScaleProperty, value); }

        public double CenterX { get => (double)this.GetValue(CenterXProperty); set => this.SetValue(CenterXProperty, value); }

        public double CenterY { get => (double)this.GetValue(CenterYProperty); set => this.SetValue(CenterYProperty, value); }

        public ZoomControl()
        {
            InitializeComponent();
        }

        private void ContentPresenter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsMousePosition == false) return;

                var cnt = sender as UIElement;
                var pos = e.GetPosition(cnt);

                this.CenterX = pos.X - (this.ActualWidth * 0.5D);
                this.CenterY = pos.Y - (this.ActualHeight * 0.5D);

                this.IsMousePosition = false;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private static void OnDependencyProeprtyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FrameworkElement;
            if (control == null) return;

            control.SetValue(e.Property, e.NewValue);
        }
    }
}
