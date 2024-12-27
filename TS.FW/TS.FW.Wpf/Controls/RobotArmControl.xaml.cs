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
    /// RobotArmControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RobotArmControl : Border
    {
        private static ControlProperty<RobotArmControl> Property = new ControlProperty<RobotArmControl>();

        public static readonly DependencyProperty WaferColorProperty = Property.ToProperty("WaferColor", typeof(Brush), new SolidColorBrush(Colors.Blue));
        public static readonly DependencyProperty WaferSizeProperty = Property.ToProperty("WaferSize", typeof(double), 20D);

        public static readonly DependencyProperty TGlassColorProperty = Property.ToProperty("TGlassColor", typeof(Brush), new SolidColorBrush(Colors.Blue));
        public static readonly DependencyProperty BGlassColorProperty = Property.ToProperty("BGlassColor", typeof(Brush), new SolidColorBrush(Colors.Blue));
        public static readonly DependencyProperty GlassSizeProperty = Property.ToProperty("GlassSize", typeof(double), 20D);

        public static readonly DependencyProperty TopArmColorProperty = Property.ToProperty("TopArmColor", typeof(Brush), new SolidColorBrush(Colors.Gray));
        public static readonly DependencyProperty BottomArmColorProperty = Property.ToProperty("BottomArmColor", typeof(Brush), new SolidColorBrush(Colors.DarkGray));        

        public static readonly DependencyProperty RobotArmWidthProperty = Property.ToProperty("RobotArmWidth", typeof(double), 5D);
        public static readonly DependencyProperty RobotArmWidth2Property = Property.ToProperty("RobotArmWidth2", typeof(double), 10D);
        public static readonly DependencyProperty RobotArmHeightProperty = Property.ToProperty("RobotArmHeight", typeof(double), 30D);

        public static readonly DependencyProperty TopWaferVisibilityProperty = Property.ToProperty("TopWaferVisibility", typeof(Visibility), Visibility.Hidden);
        public static readonly DependencyProperty BottomWaferVisibilityProperty = Property.ToProperty("BottomWaferVisibility", typeof(Visibility), Visibility.Hidden);

        public static readonly DependencyProperty TopGlassVisibilityProperty = Property.ToProperty("TopGlassVisibility", typeof(Visibility), Visibility.Hidden);
        public static readonly DependencyProperty BottomGlassVisibilityProperty = Property.ToProperty("BottomGlassVisibility", typeof(Visibility), Visibility.Hidden);

        public static readonly DependencyProperty RobotArmPosProperty = Property.ToProperty("RobotArmPos", typeof(double), 0D);
        public static readonly DependencyProperty RobotTopArmProperty = Property.ToProperty("RobotTopArmPos", typeof(double), 0D);
        public static readonly DependencyProperty RobotBottomArmProperty = Property.ToProperty("RobotBottomArmPos", typeof(double), 0D);

        public static readonly DependencyProperty TopArmMarginProperty = Property.ToProperty("TopArmMargin", typeof(Thickness), new Thickness(0));
        public static readonly DependencyProperty BottomArmMarginProperty = Property.ToProperty("BottomArmMargin", typeof(Thickness), new Thickness(0));

        public Brush WaferColor { get => (Brush)this.GetValue(WaferColorProperty); set => this.SetValue(WaferColorProperty, value); }

        public double WaferSize { get => (double)this.GetValue(WaferSizeProperty); set => this.SetValue(WaferSizeProperty, value); }

        public Brush TGlassColor { get => (Brush)this.GetValue(TGlassColorProperty); set => this.SetValue(TGlassColorProperty, value); }

        public Brush BGlassColor { get => (Brush)this.GetValue(BGlassColorProperty); set => this.SetValue(BGlassColorProperty, value); }

        public double GlassSize { get => (double)this.GetValue(GlassSizeProperty); set => this.SetValue(GlassSizeProperty, value); }

        public Brush TopArmColor { get => (Brush)this.GetValue(TopArmColorProperty); set => this.SetValue(TopArmColorProperty, value); }

        public Brush BottomArmColor { get => (Brush)this.GetValue(BottomArmColorProperty); set => this.SetValue(BottomArmColorProperty, value); }

        public double RobotArmWidth { get => (double)this.GetValue(RobotArmWidthProperty); set => this.SetValue(RobotArmWidthProperty, value); }

        public double RobotArmWidth2 { get => (double)this.GetValue(RobotArmWidth2Property); set => this.SetValue(RobotArmWidth2Property, value); }

        public double RobotArmHeight { get => (double)this.GetValue(RobotArmHeightProperty); set => this.SetValue(RobotArmHeightProperty, value); }

        public Visibility TopWaferVisibility { get => (Visibility)this.GetValue(TopWaferVisibilityProperty); set => this.SetValue(TopWaferVisibilityProperty, value); }

        public Visibility BottomWaferVisibility { get => (Visibility)this.GetValue(BottomWaferVisibilityProperty); set => this.SetValue(BottomWaferVisibilityProperty, value); }

        public Visibility TopGlassVisibility { get => (Visibility)this.GetValue(TopGlassVisibilityProperty); set => this.SetValue(TopGlassVisibilityProperty, value); }

        public Visibility BottomGlassVisibility { get => (Visibility)this.GetValue(BottomGlassVisibilityProperty); set => this.SetValue(BottomGlassVisibilityProperty, value); }

        public double RobotArmPos { get => (double)this.GetValue(RobotArmPosProperty); set => this.SetValue(RobotArmPosProperty, value); }

        public double RobotTopArmPos { get => (double)this.GetValue(RobotTopArmProperty); set => this.SetValue(RobotTopArmProperty, value); }

        public double RobotBottomArmPos { get => (double)this.GetValue(RobotBottomArmProperty); set => this.SetValue(RobotBottomArmProperty, value); }

        public Thickness TopArmMargin { get => (Thickness)this.GetValue(TopArmMarginProperty); set => this.SetValue(TopArmMarginProperty, value); }

        public Thickness BottomArmMargin { get => (Thickness)this.GetValue(BottomArmMarginProperty); set => this.SetValue(BottomArmMarginProperty, value); }

        public RobotArmControl()
        {
            InitializeComponent();
        }
    }
}
