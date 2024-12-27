using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TS.FW.Wpf.Controls
{
    /// <summary>
    /// IconControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class IconControl : UserControl
    {
        private static ControlProperty<IconControl> Property = new ControlProperty<IconControl>();

        public static readonly DependencyProperty CommandProperty = Property.ToCommand("Command");
        public static readonly DependencyProperty CommandParameterProperty = Property.ToCommandParameter("CommandParameter");

        public static readonly DependencyProperty IconProperty = Property.ToProperty("Icon", typeof(eIconType), eIconType.None);
        public static readonly DependencyProperty VisualProperty = Property.ToProperty("Visual", typeof(Visual), null);

        public ICommand Command { get => (ICommand)this.GetValue(CommandProperty); set => this.SetValue(CommandProperty, value); }

        public object CommandParameter { get => (object)this.GetValue(CommandParameterProperty); set => this.SetValue(CommandParameterProperty, value); }

        public eIconType Icon { get => (eIconType)this.GetValue(IconProperty); set => this.SetValue(IconProperty, value); }

        public Visual Visual { get => (Visual)this.GetValue(VisualProperty); set => this.SetValue(VisualProperty, value); }

        public IconControl()
        {
            InitializeComponent();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            try
            {
                this.Command?.Execute(this.CommandParameter);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                base.OnMouseLeftButtonDown(e);
            }
        }
    }

    public enum eIconType
    {
        None,
        Custom,
        Select,
        Refresh,
        New,
        Delete,
        Copy,
        Save,
        Buzzer,
        Alarm,
        Light,
        Left,
        Right,
        Up,
        Down,
        CW,
        CCW,
        Lock,
        Unlock,
        Search,

        Home,
        Laptop,
        MonitorPlay,
        Cogs,
        Cabinet,
        Card,
        Chat,
        Folder,
        FromBasic,
        ListGear,
        ListSelect,
        Connect,
        Disconnect,
        Server,
        Settings,
        Link,
    }
}
