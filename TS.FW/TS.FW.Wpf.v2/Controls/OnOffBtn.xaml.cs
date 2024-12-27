using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TS.FW.Wpf.v2.Controls
{
    /// <summary>
    /// OnOffBtn.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class OnOffBtn : Button
    {
        private static readonly ControlProperty<OnOffBtn> DP = new ControlProperty<OnOffBtn>();

        public static readonly DependencyProperty OnProperty = DP.ToProperty<object>("On");
        public static readonly DependencyProperty OffProperty = DP.ToProperty<object>("Off");

        public static readonly DependencyProperty ContentColorProperty = DP.ToProperty<Brush>(Brushes.Black);

        public static readonly DependencyProperty MarginContentProperty = DP.ToProperty(new Thickness(0));
        public static readonly DependencyProperty CornerRadiusProperty = DP.ToProperty(new CornerRadius(3));
        public static readonly DependencyProperty ShadowProperty = DP.ToProperty(true);

        public static readonly DependencyProperty IsCaptureProperty = DP.ToProperty(false);

        public static readonly DependencyProperty IsCheckedProperty = DP.ToProperty(false);

        public object On { get => (object)this.GetValue(OnProperty); set => this.SetValue(OnProperty, value); }
        
        public object Off { get => (object)this.GetValue(OffProperty); set => this.SetValue(OffProperty, value); }

        public Brush ContentColor { get => (Brush)this.GetValue(ContentColorProperty); set => this.SetValue(ContentColorProperty, value); }

        public Thickness MarginContent { get => (Thickness)this.GetValue(MarginContentProperty); set => this.SetValue(MarginContentProperty, value); }

        public CornerRadius CornerRadius { get => (CornerRadius)this.GetValue(CornerRadiusProperty); set => this.SetValue(CornerRadiusProperty, value); }

        public bool Shadow { get => (bool)this.GetValue(ShadowProperty); set => this.SetValue(ShadowProperty, value); }

        public bool IsCapture { get => (bool)this.GetValue(IsCaptureProperty); set => this.SetValue(IsCaptureProperty, value); }

        public bool IsChecked { get => (bool)this.GetValue(IsCheckedProperty); set => this.SetValue(IsCheckedProperty, value); }

        public OnOffBtn()
        {
            InitializeComponent();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            try
            {
                this.IsChecked = !this.IsChecked;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                base.OnMouseDown(e);
            }
        }

        private void xContent_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = IsCapture;
        }
    }
}
