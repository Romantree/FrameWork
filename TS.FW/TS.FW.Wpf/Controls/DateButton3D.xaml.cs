using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TS.FW.Wpf.Controls
{
    /// <summary>
    /// DateButton3D.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DateButton3D : Button
    {
        private static ControlProperty<DateButton3D> Property = new ControlProperty<DateButton3D>();

        public new static readonly DependencyProperty ContentProperty = Property.ToProperty("Content", typeof(object), null);

        public static readonly DependencyProperty CornerRadiusProperty = Property.ToProperty("CornerRadius", typeof(CornerRadius), new CornerRadius(0));
        public static readonly DependencyProperty ContentMarginProperty = Property.ToProperty("ContentMargin", typeof(Thickness), new Thickness(0));
        public static readonly DependencyProperty IconSizeProperty = Property.ToProperty("IconSize", typeof(double), 0D);

        private DatePicker datePicker;

        public new object Content { get => (object)this.GetValue(ContentProperty); set => this.SetValue(ContentProperty, value); }

        public CornerRadius CornerRadius { get => (CornerRadius)this.GetValue(CornerRadiusProperty); set => this.SetValue(CornerRadiusProperty, value); }

        public Thickness ContentMargin { get => (Thickness)this.GetValue(ContentMarginProperty); set => this.SetValue(ContentMarginProperty, value); }

        public double IconSize { get => (double)this.GetValue(IconSizeProperty); set => this.SetValue(IconSizeProperty, value); }

        public DateButton3D()
        {
            InitializeComponent();
        }

        protected override void OnClick()
        {
            try
            {
                this.InitControl();

                datePicker.IsDropDownOpen = true;
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
                if (this.datePicker != null) return;

                this.datePicker = (DatePicker)this.Template.FindName("xDatePicker", this);
                this.datePicker.Width = this.ActualWidth;
                this.datePicker.Height = this.ActualHeight;

                this.datePicker.UpdateLayout();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
