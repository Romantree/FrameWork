using System.Windows;
using System.Windows.Controls;
using TS.FW.Wpf.v2.Core;

namespace TS.FW.Wpf.v2.Controls
{
    /// <summary>
    /// CaptionValue.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CaptionValue : ContentControl
    {
        private static readonly ControlProperty<CaptionValue> DP = new ControlProperty<CaptionValue>();

        public static readonly DependencyProperty CaptionProperty = DP.ToProperty(string.Empty);
        public static readonly DependencyProperty SizeCaptionProperty = DP.ToProperty(100.0D);
        public static readonly DependencyProperty MarginCaptionProperty = DP.ToProperty(new Thickness(2));
        public static readonly DependencyProperty CornerRadiusProperty = DP.ToProperty(new CornerRadius(3));
        public static readonly DependencyProperty TextAlignmentProperty = DP.ToProperty(TextAlignment.Center);

        public static readonly DependencyProperty ShadowProperty = DP.ToProperty(true);

        public string Caption { get => (string)this.GetValue(CaptionProperty); set => this.SetValue(CaptionProperty, value); }

        public double SizeCaption { get => (double)this.GetValue(SizeCaptionProperty); set => this.SetValue(SizeCaptionProperty, value); }

        public Thickness MarginCaption { get => (Thickness)this.GetValue(MarginCaptionProperty); set => this.SetValue(MarginCaptionProperty, value); }

        public CornerRadius CornerRadius { get => (CornerRadius)this.GetValue(CornerRadiusProperty); set => this.SetValue(CornerRadiusProperty, value); }

        public TextAlignment TextAlignment { get => (TextAlignment)this.GetValue(TextAlignmentProperty); set => this.SetValue(TextAlignmentProperty, value); }

        public bool Shadow { get => (bool)this.GetValue(ShadowProperty); set => this.SetValue(ShadowProperty, value); }

        public CaptionValue()
        {
            InitializeComponent();
        }
    }
}
