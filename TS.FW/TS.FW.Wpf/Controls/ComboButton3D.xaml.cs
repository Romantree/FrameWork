using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TS.FW.Wpf.Controls
{
    /// <summary>
    /// ComboButton3D.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ComboButton3D : Button
    {
        private static ControlProperty<ComboButton3D> Property = new ControlProperty<ComboButton3D>();

        public static readonly DependencyProperty ItemsSourceProperty = Property.ToProperty("ItemsSource", typeof(IEnumerable), null, FrameworkPropertyMetadataOptions.None);
        public static readonly DependencyProperty ItemContainerStyleProperty = Property.ToProperty("ItemContainerStyle", typeof(Style), null);

        public new static readonly DependencyProperty ContentProperty = Property.ToProperty("Content", typeof(object), null);

        public static readonly DependencyProperty CornerRadiusProperty = Property.ToProperty("CornerRadius", typeof(CornerRadius), new CornerRadius(0));
        public static readonly DependencyProperty ContentMarginProperty = Property.ToProperty("ContentMargin", typeof(Thickness), new Thickness(0));
        public static readonly DependencyProperty IconSizeProperty = Property.ToProperty("IconSize", typeof(double), 0D);

        private ComboBox comboBox;

        public IEnumerable ItemsSource { get => (IEnumerable)this.GetValue(ItemsSourceProperty); set => this.SetValue(ItemsSourceProperty, value); }

        public Style ItemContainerStyle { get => (Style)this.GetValue(ItemContainerStyleProperty); set => this.SetValue(ItemContainerStyleProperty, value); }

        public new object Content { get => (object)this.GetValue(ContentProperty); set => this.SetValue(ContentProperty, value); }

        public CornerRadius CornerRadius { get => (CornerRadius)this.GetValue(CornerRadiusProperty); set => this.SetValue(CornerRadiusProperty, value); }

        public Thickness ContentMargin { get => (Thickness)this.GetValue(ContentMarginProperty); set => this.SetValue(ContentMarginProperty, value); }

        public double IconSize { get => (double)this.GetValue(IconSizeProperty); set => this.SetValue(IconSizeProperty, value); }

        public ComboButton3D()
        {
            InitializeComponent();
        }

        protected override void OnClick()
        {
            try
            {
                this.InitControl();

                comboBox.IsDropDownOpen = true;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void InitControl()
        {
            try
            {
                if (this.comboBox != null) return;

                this.comboBox = (ComboBox)this.Template.FindName("xComboBox", this);
                this.comboBox.Width = this.ActualWidth;
                this.comboBox.Height = this.ActualHeight;

                this.comboBox.UpdateLayout();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
