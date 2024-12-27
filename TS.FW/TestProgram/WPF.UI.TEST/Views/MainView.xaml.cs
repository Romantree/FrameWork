using System;
using System.Windows;
using System.Windows.Input;
using TS.FW;
using TS.FW.Wpf.v2.Controls.InPut;
using TS.FW.Wpf.v2.Controls.Win;
using WPF.UI.TEST.ViewModels;

namespace WPF.UI.TEST.Views
{
    /// <summary>
    /// MainView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainView : Window
    {
        private const string msg = "The problem is that the HeaderTemplate is used for templating the Header thus within the HeaderTemplate your DataContext is whatever you bind or assign to the Header property of your GroupBox.\r\n\r\nThink of the Header property as almost like the DataContext for the header of the control. Normally the DataContext property inherits its value from its parent but since not every control has a Header the Header is blank unless you set it.\r\n\r\nBy binding your Header explicitly to the current DataContext Header=\"{Binding}\" your example should work as you expect. To help illustrate how this works I've created a simple example below that shows how the Header and DataContext work independently from each other for providing data to either the body or header of the control.";

        public MainView()
        {
            InitializeComponent();

            this.Loaded += MainView_Loaded;
        }

        private void MainView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DataContext = new MainViewModel();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.LeftButton == MouseButtonState.Released) return;

                this.DragMove();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void EventBtn_Click(object sender, RoutedEventArgs e)
        {
            KeyPad.Show(0.0D);
        }

        private void EventBtn_Click_1(object sender, RoutedEventArgs e)
        {
            KeyboardPad.Show("");
        }

        private void EventBtn_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void EventBtn_Click_3(object sender, RoutedEventArgs e)
        {
            MsgBox.Show(msg, MsgBoxType.CLOSE);
        }

        private void EventBtn_Click_4(object sender, RoutedEventArgs e)
        {
            MsgBox.Show(msg, MsgBoxType.YES_NO);
        }

        private void EventBtn_Click_5(object sender, RoutedEventArgs e)
        {
            MsgBox.Show(msg, MsgBoxType.OK_CANCEL);
        }
    }
}
