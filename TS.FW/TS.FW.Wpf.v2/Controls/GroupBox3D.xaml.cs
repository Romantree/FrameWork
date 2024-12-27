using System.Windows;
using System.Windows.Controls;

namespace TS.FW.Wpf.v2.Controls
{
    /// <summary>
    /// GroupBox3D.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class GroupBox3D : GroupBox
    {
        private static ControlProperty<GroupBox3D> DP = new ControlProperty<GroupBox3D>();

        public static readonly DependencyProperty CornerRadiusProperty = DP.ToProperty(new CornerRadius(3));

        public CornerRadius CornerRadius { get => (CornerRadius)this.GetValue(CornerRadiusProperty); set => this.SetValue(CornerRadiusProperty, value); }

        public GroupBox3D()
        {
            InitializeComponent();
        }
    }
}
