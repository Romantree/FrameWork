using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace TS.FW.Wpf.Controls
{
    /// <summary>
    /// ToggleButton3D.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ToggleButton3D : ToggleButton
    {
        private static ControlProperty<ToggleButton3D> Property = new ControlProperty<ToggleButton3D>();

        public static readonly DependencyProperty CornerRadiusProperty = Property.ToProperty("CornerRadius", typeof(CornerRadius), new CornerRadius(0));
        public static readonly DependencyProperty ContentMarginProperty = Property.ToProperty("ContentMargin", typeof(Thickness), new Thickness(0));

        public static readonly DependencyProperty IconProperty = Property.ToProperty("Icon", typeof(eIconType), eIconType.None);
        public static readonly DependencyProperty IconMarginProperty = Property.ToProperty("IconMargin", typeof(Thickness), new Thickness(2));
        public static readonly DependencyProperty IconWidthProperty = Property.ToProperty("IconWidth", typeof(double), 32.0D);
        public static readonly DependencyProperty IconHeightProperty = Property.ToProperty("IconHeight", typeof(double), 32.0D);
        public static readonly DependencyProperty HorizontalIconAlignmentProperty = Property.ToProperty("HorizontalIconAlignment", typeof(HorizontalAlignment), HorizontalAlignment.Left);
        public static readonly DependencyProperty VerticalIconAlignmentProperty = Property.ToProperty("VerticalIconAlignment", typeof(VerticalAlignment), VerticalAlignment.Center);
        public static readonly DependencyProperty VisualProperty = Property.ToProperty("Visual", typeof(Visual), null);

        public CornerRadius CornerRadius { get => (CornerRadius)this.GetValue(CornerRadiusProperty); set => this.SetValue(CornerRadiusProperty, value); }

        public Thickness ContentMargin { get => (Thickness)this.GetValue(ContentMarginProperty); set => this.SetValue(ContentMarginProperty, value); }

        public eIconType Icon { get => (eIconType)this.GetValue(IconProperty); set => this.SetValue(IconProperty, value); }

        public Thickness IconMargin { get => (Thickness)this.GetValue(IconMarginProperty); set => this.SetValue(IconMarginProperty, value); }

        public double IconWidth { get => (double)this.GetValue(IconWidthProperty); set => this.SetValue(IconWidthProperty, value); }

        public double IconHeight { get => (double)this.GetValue(IconHeightProperty); set => this.SetValue(IconHeightProperty, value); }

        public HorizontalAlignment HorizontalIconAlignment { get => (HorizontalAlignment)this.GetValue(HorizontalIconAlignmentProperty); set => this.SetValue(HorizontalIconAlignmentProperty, value); }

        public VerticalAlignment VerticalIconAlignment { get => (VerticalAlignment)this.GetValue(VerticalIconAlignmentProperty); set => this.SetValue(VerticalIconAlignmentProperty, value); }

        public Visual Visual { get => (Visual)this.GetValue(VisualProperty); set => this.SetValue(VisualProperty, value); }

        public ToggleButton3D()
        {
            InitializeComponent();
        }

        protected override void OnClick()
        {
            try
            {
                if (this.IsChecked.HasValue == false) this.IsChecked = false;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                base.OnClick();
            }
        }
    }
}
