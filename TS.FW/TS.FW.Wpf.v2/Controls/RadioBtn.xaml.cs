using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TS.FW.Wpf.v2.Controls
{
    /// <summary>
    /// RadioBtn.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RadioBtn : RadioButton
    {
        private static readonly ControlProperty<RadioBtn> DP = new ControlProperty<RadioBtn>();

        public static readonly DependencyProperty IconSizeProperty = DP.ToProperty(new GridLength(30));
        public static readonly DependencyProperty IconMarginProperty = DP.ToProperty(new Thickness(5));

        public static readonly DependencyProperty MarginContentProperty = DP.ToProperty(new Thickness(0));
        public static readonly DependencyProperty CornerRadiusProperty = DP.ToProperty(new CornerRadius(3));

        public new static readonly DependencyProperty HorizontalContentAlignmentProperty = DP.ToProperty(HorizontalAlignment.Center);
        public new static readonly DependencyProperty VerticalContentAlignmentProperty = DP.ToProperty(VerticalAlignment.Center);

        public static readonly DependencyProperty OnProperty = DP.ToProperty<Brush>(new SolidColorBrush(Converter.ColorConverter.ToColor("FF3E3A39")));
        public static readonly DependencyProperty OffProperty = DP.ToProperty<Brush>(new SolidColorBrush(Converter.ColorConverter.ToColor("FF3E3A39")));

        public static readonly DependencyProperty ShadowProperty = DP.ToProperty(true);

        public GridLength IconSize { get => (GridLength)this.GetValue(IconSizeProperty); set => this.SetValue(IconSizeProperty, value); }

        public Thickness IconMargin { get => (Thickness)this.GetValue(IconMarginProperty); set => this.SetValue(IconMarginProperty, value); }

        public Thickness MarginContent { get => (Thickness)this.GetValue(MarginContentProperty); set => this.SetValue(MarginContentProperty, value); }

        public CornerRadius CornerRadius { get => (CornerRadius)this.GetValue(CornerRadiusProperty); set => this.SetValue(CornerRadiusProperty, value); }

        public new HorizontalAlignment HorizontalContentAlignment { get => (HorizontalAlignment)this.GetValue(HorizontalContentAlignmentProperty); set => this.SetValue(HorizontalContentAlignmentProperty, value); }

        public new VerticalAlignment VerticalContentAlignment { get => (VerticalAlignment)this.GetValue(VerticalContentAlignmentProperty); set => this.SetValue(VerticalContentAlignmentProperty, value); }

        public Brush On { get => (Brush)this.GetValue(OnProperty); set => this.SetValue(OnProperty, value); }

        public Brush Off { get => (Brush)this.GetValue(OffProperty); set => this.SetValue(OffProperty, value); }

        public bool Shadow { get => (bool)this.GetValue(ShadowProperty); set => this.SetValue(ShadowProperty, value); }

        public RadioBtn()
        {
            InitializeComponent();
        }
    }
}
