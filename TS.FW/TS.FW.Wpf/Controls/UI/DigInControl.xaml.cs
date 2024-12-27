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

namespace TS.FW.Wpf.Controls.UI
{
    /// <summary>
    /// DigInControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DigInControl : Grid
    {
        public static readonly DependencyProperty OnBitOnOffCmdProperty = ToCommandProperty("OnBitOnOffCmd");
        public static readonly DependencyProperty OnBitNoCmdProperty = ToCommandProperty("OnBitNoCmd");
        public static readonly DependencyProperty OnChangedCmdProperty = ToCommandProperty("OnChangedCmd");

        public ICommand OnBitOnOffCmd { get => (ICommand)this.GetValue(OnBitOnOffCmdProperty); set => this.SetValue(OnBitOnOffCmdProperty, value); }

        public ICommand OnBitNoCmd { get => (ICommand)this.GetValue(OnBitNoCmdProperty); set => this.SetValue(OnBitNoCmdProperty, value); }

        public ICommand OnChangedCmd { get => (ICommand)this.GetValue(OnChangedCmdProperty); set => this.SetValue(OnChangedCmdProperty, value); }
        public DigInControl()
        {
            InitializeComponent();
        }


        private void Label3D_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (this.OnBitOnOffCmd == null) return;

                this.OnBitOnOffCmd.Execute(this.DataContext);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private static DependencyProperty ToDependencyProperty(string name, Type propertyType, object defaultValue)
        {
            return DependencyProperty.RegisterAttached(name, propertyType, typeof(DigInControl), new FrameworkPropertyMetadata(defaultValue, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDependencyProeprtyChanged));
        }

        private static DependencyProperty ToCommandProperty(string name)
        {
            return DependencyProperty.RegisterAttached(name, typeof(ICommand), typeof(DigInControl));
        }

        private static void OnDependencyProeprtyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FrameworkElement;
            if (control == null) return;

            control.SetValue(e.Property, e.NewValue);
        }
    }
}
