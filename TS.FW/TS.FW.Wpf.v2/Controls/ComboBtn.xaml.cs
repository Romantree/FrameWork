using System.Windows;
using System.Windows.Controls;

namespace TS.FW.Wpf.v2.Controls
{
    /// <summary>
    /// ComboBtn.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ComboBtn : ComboBox
    {
        private static readonly ControlProperty<ComboBtn> DP = new ControlProperty<ComboBtn>();

        public static readonly DependencyProperty ShadowProperty = DP.ToProperty(true);
        public static readonly DependencyProperty CornerRadiusProperty = DP.ToProperty(new CornerRadius(3));

        public CornerRadius CornerRadius { get => (CornerRadius)this.GetValue(CornerRadiusProperty); set => this.SetValue(CornerRadiusProperty, value); }

        public bool Shadow { get => (bool)this.GetValue(ShadowProperty); set => this.SetValue(ShadowProperty, value); }

        public ComboBtn()
        {
            InitializeComponent();
        }
    }
}
