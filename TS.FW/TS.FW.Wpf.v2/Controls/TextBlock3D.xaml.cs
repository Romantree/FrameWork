using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TS.FW.Wpf.v2.Controls
{
    /// <summary>
    /// TextBlock3D.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TextBlock3D : UserControl
    {
        private static readonly ControlProperty<TextBlock3D> DP = new ControlProperty<TextBlock3D>();

        public static readonly DependencyProperty TitleProperty = DP.ToProperty(string.Empty);
        public static readonly DependencyProperty TitleFontSizeProperty = DP.ToProperty(8.0D);
        public static readonly DependencyProperty TitleFontWeightProperty = DP.ToProperty(FontWeight.FromOpenTypeWeight(1));
        public static readonly DependencyProperty TitleForegroundProperty = DP.ToProperty<Brush>(Brushes.Black);

        public static readonly DependencyProperty TextProperty = DP.ToProperty(string.Empty);
        public static readonly DependencyProperty MarginTextProperty = DP.ToProperty(new Thickness(3,0,0,0));
        public static readonly DependencyProperty CornerRadiusProperty = DP.ToProperty(new CornerRadius(3));
        public static readonly DependencyProperty TextAlignmentProperty = DP.ToProperty(TextAlignment.Center);

        public static readonly DependencyProperty PasswordProperty = DP.ToProperty(false);

        public static readonly DependencyProperty ShadowProperty = DP.ToProperty(true);

        private static readonly DependencyPropertyDescriptor TextDescriptor = DP.ToPropertyDescriptor(TextProperty);

        private PasswordBox xPassword;
        private TextBlock xText;

        public string Title { get => (string)this.GetValue(TitleProperty); set => this.SetValue(TitleProperty, value); }

        public double TitleFontSize { get => (double)this.GetValue(TitleFontSizeProperty); set => this.SetValue(TitleFontSizeProperty, value); }

        public FontWeight TitleFontWeight { get => (FontWeight)this.GetValue(TitleFontWeightProperty); set => this.SetValue(TitleFontWeightProperty, value); }

        public Brush TitleForeground { get => (Brush)this.GetValue(TitleForegroundProperty); set => this.SetValue(TitleForegroundProperty, value); }

        public string Text { get => (string)this.GetValue(TextProperty); set => this.SetValue(TextProperty, value); }

        public Thickness MarginText { get => (Thickness)this.GetValue(MarginTextProperty); set => this.SetValue(MarginTextProperty, value); }

        public CornerRadius CornerRadius { get => (CornerRadius)this.GetValue(CornerRadiusProperty); set => this.SetValue(CornerRadiusProperty, value); }

        public TextAlignment TextAlignment { get => (TextAlignment)this.GetValue(TextAlignmentProperty); set => this.SetValue(TextAlignmentProperty, value); }

        public bool Password { get => (bool)this.GetValue(PasswordProperty); set => this.SetValue(PasswordProperty, value); }

        public bool Shadow { get => (bool)this.GetValue(ShadowProperty); set => this.SetValue(ShadowProperty, value); }

        public TextBlock3D()
        {
            InitializeComponent();
        }

        public override void OnApplyTemplate()
        {
            try
            {
                this.xPassword = (PasswordBox)this.Template.FindName("xPassword", this);
                this.xText = (TextBlock)this.Template.FindName("xText", this);

                if (this.xPassword != null)
                {
                    this.xPassword.Visibility = this.Password ? Visibility.Visible : Visibility.Hidden;
                    this.xPassword.Password = this.Text;
                }

                if (this.xText != null)
                {
                    this.xText.Visibility = this.Password ? Visibility.Hidden : Visibility.Visible;
                }

                TextDescriptor.AddValueChanged(this, OnTextChanged);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                base.OnApplyTemplate();
            }
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                this.xPassword.Password = this.Text;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
