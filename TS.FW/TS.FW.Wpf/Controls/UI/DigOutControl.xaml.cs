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
    /// DigOutControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DigOutControl : Grid
    {
        public static readonly DependencyProperty OnBitNoCmdProperty = ToCommandProperty("OnBitNoCmd");
        public static readonly DependencyProperty OnChangedCmdProperty = ToCommandProperty("OnChangedCmd");
        public static readonly DependencyProperty OnDigOnCmdProperty = ToCommandProperty("OnDigOnCmd");
        public static readonly DependencyProperty OnDigOffCmdProperty = ToCommandProperty("OnDigOffCmd");

        public ICommand OnBitNoCmd { get => (ICommand)this.GetValue(OnBitNoCmdProperty); set => this.SetValue(OnBitNoCmdProperty, value); }

        public ICommand OnChangedCmd { get => (ICommand)this.GetValue(OnChangedCmdProperty); set => this.SetValue(OnChangedCmdProperty, value); }

        public ICommand OnDigOnCmd { get => (ICommand)this.GetValue(OnDigOnCmdProperty); set => this.SetValue(OnDigOnCmdProperty, value); }

        public ICommand OnDigOffCmd { get => (ICommand)this.GetValue(OnDigOffCmdProperty); set => this.SetValue(OnDigOffCmdProperty, value); }
        public DigOutControl()
        {
            InitializeComponent();
        }

        private static DependencyProperty ToDependencyProperty(string name, Type propertyType, object defaultValue)
        {
            return DependencyProperty.RegisterAttached(name, propertyType, typeof(DigOutControl), new FrameworkPropertyMetadata(defaultValue, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDependencyProeprtyChanged));
        }

        private static DependencyProperty ToCommandProperty(string name)
        {
            return DependencyProperty.RegisterAttached(name, typeof(ICommand), typeof(DigOutControl));
        }

        private static void OnDependencyProeprtyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FrameworkElement;
            if (control == null) return;

            control.SetValue(e.Property, e.NewValue);
        }
    }
}
