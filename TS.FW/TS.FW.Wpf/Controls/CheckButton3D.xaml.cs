using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace TS.FW.Wpf.Controls
{
    /// <summary>
    /// CheckButton3D.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CheckButton3D : ToggleButton
    {
        private static ControlProperty<CheckButton3D> Property = new ControlProperty<CheckButton3D>();

        public static readonly DependencyProperty CornerRadiusProperty = Property.ToProperty("CornerRadius", typeof(CornerRadius), new CornerRadius(0));
        public static readonly DependencyProperty ContentMarginProperty = Property.ToProperty("ContentMargin", typeof(Thickness), new Thickness(0, 0, 5, 0));
        public static readonly DependencyProperty IconSizeProperty = Property.ToProperty("IconSize", typeof(double), 0D);

        public CornerRadius CornerRadius { get => (CornerRadius)this.GetValue(CornerRadiusProperty); set => this.SetValue(CornerRadiusProperty, value); }

        public Thickness ContentMargin { get => (Thickness)this.GetValue(ContentMarginProperty); set => this.SetValue(ContentMarginProperty, value); }

        public double IconSize { get => (double)this.GetValue(IconSizeProperty); set => this.SetValue(IconSizeProperty, value); }

        public CheckButton3D()
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
