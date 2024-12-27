using System;
using System.Windows;
using TS.FW.GIGA.ViewModels;

namespace TS.FW.GIGA.Views
{
    /// <summary>
    /// MainView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainView : Window
    {
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

        int test = 0;

        private void EventBtn_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (AP.IsSim == false) return;

                var alarm = (eAlarm)test;

                AP.Alarm.AlarmPost(alarm);

                test++;

                if (test > 20) test = 0;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
