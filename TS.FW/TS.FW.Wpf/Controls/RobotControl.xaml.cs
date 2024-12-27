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
using TS.FW.Wpf.Core;

namespace TS.FW.Wpf.Controls
{
    /// <summary>
    /// RobotControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RobotControl : UserControl
    {
        private readonly static ControlProperty<RobotControl> Property = new ControlProperty<RobotControl>();

        public static readonly DependencyProperty StrokeThicknessProperty = Property.ToProperty("StrokeThickness", typeof(double), 5D);

        public static readonly DependencyProperty RobotSizeProperty = Property.ToProperty("RobotSize", typeof(double), 100D);

        public static readonly DependencyProperty ArmBackgroundProperty = Property.ToProperty("ArmBackground", typeof(Brush), new SolidColorBrush(Colors.LightGray));
        public static readonly DependencyProperty ArmBorderBrushProperty = Property.ToProperty("ArmBorderBrush", typeof(Brush), new SolidColorBrush(Colors.DimGray));
        public static readonly DependencyProperty ArmBorderThicknessProperty = Property.ToProperty("ArmBorderThickness", typeof(Thickness), new Thickness(1));

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

        public static readonly DependencyProperty RobotMoveProperty = Property.ToProperty("RobotMove", typeof(RobotMoveModel), new RobotMoveModel());

        public static readonly DependencyProperty TopArmMarginProperty = Property.ToProperty("TopArmMargin", typeof(Thickness), new Thickness(0));
        public static readonly DependencyProperty BottomArmMarginProperty = Property.ToProperty("BottomArmMargin", typeof(Thickness), new Thickness(0));

        public double StrokeThickness { get => (double)this.GetValue(StrokeThicknessProperty); set => this.SetValue(StrokeThicknessProperty, value); }

        public double RobotSize { get => (double)this.GetValue(RobotSizeProperty); set => this.SetValue(RobotSizeProperty, value); }

        public Brush ArmBackground { get => (Brush)this.GetValue(ArmBackgroundProperty); set => this.SetValue(ArmBackgroundProperty, value); }

        public Brush ArmBorderBrush { get => (Brush)this.GetValue(ArmBorderBrushProperty); set => this.SetValue(ArmBorderBrushProperty, value); }

        public Thickness ArmBorderThickness { get => (Thickness)this.GetValue(ArmBorderThicknessProperty); set => this.SetValue(ArmBorderThicknessProperty, value); }

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

        public RobotMoveModel RobotMove { get => (RobotMoveModel)this.GetValue(RobotMoveProperty); set => this.SetValue(RobotMoveProperty, value); }

        public Thickness TopArmMargin { get => (Thickness)this.GetValue(TopArmMarginProperty); set => this.SetValue(TopArmMarginProperty, value); }

        public Thickness BottomArmMargin { get => (Thickness)this.GetValue(BottomArmMarginProperty); set => this.SetValue(BottomArmMarginProperty, value); }

        public RobotControl()
        {
            InitializeComponent();
        }

        public static void Move(RobotMoveModel item, RobotMoveType type, int movePos, int delay = 10)
        {
            try
            {
                switch (type)
                {
                    case RobotMoveType.X:
                        {
                            while (item.RobotPosX != movePos)
                            {
                                item.RobotPosX += movePos > item.RobotPosX ? 1 : -1;
                                System.Threading.Thread.Sleep(delay);
                            }
                        }
                        break;
                    case RobotMoveType.Y:
                        {
                            while (item.RobotPosY != movePos)
                            {
                                item.RobotPosY += movePos > item.RobotPosY ? 1 : -1;
                                System.Threading.Thread.Sleep(delay);
                            }
                        }
                        break;
                    case RobotMoveType.Angle:
                        {
                            while (item.RobotAngle != movePos)
                            {
                                item.RobotAngle += movePos > item.RobotAngle ? 1 : -1;
                                System.Threading.Thread.Sleep(delay);
                            }
                        }
                        break;
                    case RobotMoveType.Arm:
                        {
                            while (item.RobotArmPos != movePos)
                            {
                                item.RobotArmPos += movePos > item.RobotArmPos ? 1 : -1;
                                System.Threading.Thread.Sleep(delay);
                            }
                        }
                        break;
                    case RobotMoveType.TopArm:
                        {
                            while (item.RobotTopArmPos != movePos)
                            {
                                item.RobotTopArmPos += movePos > item.RobotTopArmPos ? 1 : -1;
                                System.Threading.Thread.Sleep(delay);
                            }
                        }
                        break;
                    case RobotMoveType.BottomArm:
                        {
                            while (item.RobotBottomArmPos != movePos)
                            {
                                item.RobotBottomArmPos += movePos > item.RobotBottomArmPos ? 1 : -1;
                                System.Threading.Thread.Sleep(delay);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(RobotControl), ex);
            }
        }
    }

    public class RobotMoveModel : ModelBase
    {
        public double RobotPosX { get => this.GetValue<double>(); set => this.SetValue(value); }

        public double RobotPosY { get => this.GetValue<double>(); set => this.SetValue(value); }

        public double RobotAngle { get => this.GetValue<double>(); set => this.SetValue(value); }

        public double RobotArmPos { get => this.GetValue<double>(); set => this.SetValue(value); }

        public double RobotTopArmPos { get => this.GetValue<double>(); set => this.SetValue(value); }

        public double RobotBottomArmPos { get => this.GetValue<double>(); set => this.SetValue(value); }

        public Visibility TopWaferVisibility { get => this.GetValue<Visibility>(); set => this.SetValue(value); }

        public Visibility BottomWaferVisibility { get => this.GetValue<Visibility>(); set => this.SetValue(value); }

        public Visibility TopGlassVisibility { get => this.GetValue<Visibility>(); set => this.SetValue(value); }

        public Visibility BottomGlassVisibility { get => this.GetValue<Visibility>(); set => this.SetValue(value); }

        public RobotMoveModel()
        {
            this.TopWaferVisibility = Visibility.Hidden;
            this.BottomWaferVisibility = Visibility.Hidden;

            this.TopGlassVisibility = Visibility.Hidden;
            this.BottomGlassVisibility = Visibility.Hidden;
        }

    }

    public enum RobotMoveType
    {
        X,
        Y,
        Angle,
        Arm,
        TopArm,
        BottomArm,
    }
}
