using System;
using System.Windows;
using System.Windows.Controls;

namespace TS.FW.Wpf.Controls
{
    /// <summary>
    /// RadioButton3D.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RadioButton3D : RadioButton
    {
        private static ControlProperty<RadioButton3D> Property = new ControlProperty<RadioButton3D>();

        public static readonly DependencyProperty CornerRadiusProperty = Property.ToProperty("CornerRadius", typeof(CornerRadius), new CornerRadius(0));
        public static readonly DependencyProperty ContentMarginProperty = Property.ToProperty("ContentMargin", typeof(Thickness), new Thickness(0, 0, 5, 0));
        public static readonly DependencyProperty IconSizeProperty = Property.ToProperty("IconSize", typeof(double), 16D);

        public CornerRadius CornerRadius { get => (CornerRadius)this.GetValue(CornerRadiusProperty); set => this.SetValue(CornerRadiusProperty, value); }

        public Thickness ContentMargin { get => (Thickness)this.GetValue(ContentMarginProperty); set => this.SetValue(ContentMarginProperty, value); }

        public double IconSize { get => (double) this.GetValue(IconSizeProperty); set => this.SetValue(IconSizeProperty, value); }

        public RadioButton3D()
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
