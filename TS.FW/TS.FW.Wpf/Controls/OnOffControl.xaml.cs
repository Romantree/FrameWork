using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace TS.FW.Wpf.Controls
{
    /// <summary>
    /// OnOffControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class OnOffControl : ToggleButton
    {
        private static ControlProperty<OnOffControl> Property = new ControlProperty<OnOffControl>();

        public static readonly DependencyProperty OnContentProperty = Property.ToProperty("OnContent", typeof(object), "ON");
        public static readonly DependencyProperty OffContentProperty = Property.ToProperty("OffContent", typeof(object), "OFF");

        public static readonly DependencyProperty ContentColorProperty = Property.ToProperty("ContentColor", typeof(Brush), new SolidColorBrush(Colors.DimGray));
        public static readonly DependencyProperty CornerRadiusProperty = Property.ToProperty("CornerRadius", typeof(CornerRadius), new CornerRadius(0));
        public static readonly DependencyProperty ContentMarginProperty = Property.ToProperty("ContentMargin", typeof(Thickness), new Thickness(2));
        public static readonly DependencyProperty BntModeProperty = Property.ToProperty("BntMode", typeof(bool), false);

        private new object Content { get; set; }

        private new object ContentStringFormat { get; set; }

        private new object ContentTemplate { get; set; }

        public object OnContent { get => (object)this.GetValue(OnContentProperty); set => this.SetValue(OnContentProperty, value); }

        public object OffContent { get => (object)this.GetValue(OffContentProperty); set => this.SetValue(OffContentProperty, value); }

        public Brush ContentColor { get => (Brush)this.GetValue(ContentColorProperty); set => this.SetValue(ContentColorProperty, value); }

        public CornerRadius CornerRadius { get => (CornerRadius)this.GetValue(CornerRadiusProperty); set => this.SetValue(CornerRadiusProperty, value); }

        public Thickness ContentMargin { get => (Thickness)this.GetValue(ContentMarginProperty); set => this.SetValue(ContentMarginProperty, value); }

        public bool BntMode { get => (bool)this.GetValue(BntModeProperty); set => this.SetValue(BntModeProperty, value); }

        public OnOffControl()
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

        private void XOnBtn_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                var cnt = sender as Label3D;
                if (cnt == null) return;

                if(cnt.Content == this.OnContent)
                {
                    e.Handled = true;

                    if (this.IsChecked == true) return;
                    
                    this.IsChecked = true;

                    if (this.Command == null) return;

                    if (this.Command.CanExecute(this.CommandParameter) == true)
                    {
                        this.Command.Execute(this.CommandParameter);
                    }
                }
                else if(cnt.Content == this.OffContent)
                {
                    e.Handled = true;

                    if (this.IsChecked == false) return;
                    
                    this.IsChecked = false;
                    
                    if (this.Command == null) return;

                    if (this.Command.CanExecute(this.CommandParameter) == true)
                    {
                        this.Command.Execute(this.CommandParameter);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
