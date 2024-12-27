using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TS.FW.Wpf.Controls
{
    /// <summary>
    /// TextBox3D.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TextBox3D : TextBox
    {
        private static ControlProperty<TextBox3D> Property = new ControlProperty<TextBox3D>();

        public new static readonly DependencyProperty TextProperty = Property.ToProperty("Text", typeof(string), "");

        public static readonly DependencyProperty CornerRadiusProperty = Property.ToProperty("CornerRadius", typeof(CornerRadius), new CornerRadius(0));
        public static readonly DependencyProperty TitleProperty = Property.ToProperty("Title", typeof(string), "Title");
        public static readonly DependencyProperty TitleFontSizeProperty = Property.ToProperty("TitleFontSize", typeof(double), 10.0D);
        public static readonly DependencyProperty TitleColorSizeProperty = Property.ToProperty("TitleColor", typeof(Brush), new SolidColorBrush(Colors.Gray));

        public new string Text { get => (string)this.GetValue(TextProperty); set => this.SetValue(TextProperty, value); }

        public CornerRadius CornerRadius { get => (CornerRadius)this.GetValue(CornerRadiusProperty); set => this.SetValue(CornerRadiusProperty, value); }

        public string Title { get => (string)this.GetValue(TitleProperty); set => this.SetValue(TitleProperty, value); }

        public double TitleFontSize { get => (double)this.GetValue(TitleFontSizeProperty); set => this.SetValue(TitleFontSizeProperty, value); }

        public Brush TitleColor { get => (Brush)this.GetValue(TitleColorSizeProperty); set => this.SetValue(TitleColorSizeProperty, value); }

        public TextBox3D()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                this.Text = (sender as TextBox).Text;
            }
            catch (System.Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
