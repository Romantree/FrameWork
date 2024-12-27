using System.Windows;
using System.Windows.Controls;

namespace TS.FW.Wpf.Controls
{
    /// <summary>
    /// Border3D.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Border3D : UserControl
    {
        private static ControlProperty<Border3D> Property = new ControlProperty<Border3D>();

        public static readonly DependencyProperty IsPressedProperty = Property.ToProperty("IsPressed", typeof(bool), false);
        public static readonly DependencyProperty CornerRadiusProperty = Property.ToProperty("CornerRadius", typeof(CornerRadius), new CornerRadius(0));

        public CornerRadius CornerRadius { get => (CornerRadius)this.GetValue(CornerRadiusProperty); set => this.SetValue(CornerRadiusProperty, value); }

        public bool IsPressed { get => (bool)this.GetValue(IsPressedProperty); set => this.SetValue(IsPressedProperty, value); }

        public Border3D()
        {
            InitializeComponent();
        }
    }
}
