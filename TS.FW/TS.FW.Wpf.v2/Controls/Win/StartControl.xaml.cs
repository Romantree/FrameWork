using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace TS.FW.Wpf.v2.Controls.Win
{
    /// <summary>
    /// StartControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StartControl : Window
    {
        private static Dictionary<string, Action> _workList = new Dictionary<string,Action>();

        private DispatcherTimer timer = new DispatcherTimer();
        private readonly Action cmpAction;
        private DateTime _startTime = DateTime.Now;

        public StartControl(Action cmpAction)
        {
            this.cmpAction= cmpAction;

            InitializeComponent();

            this.Loaded += StartControl_Loaded;
        }

        private void StartControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                timer.Interval = TimeSpan.FromMilliseconds(100);
                timer.Tick += timer_Tick;
                timer.Start();

                this.xWork.Text = string.Empty;
                this.xProgress.Value = 0;

                Task.Factory.StartNew(this.DoWork);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                this.xRunTime.Content = $"{(DateTime.Now - _startTime).TotalSeconds:f0} Sec";
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void xRoot_MouseMove(object sender, MouseEventArgs e)
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

        private void SettingData(string name, double value)
        {
            try
            {
                xWork.Text = $"{name} 중....";
                xProgress.Value = (value) * 100.0D;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void DoWork()
        {
            try
            {
                double max = _workList.Count;
                double index = 1;

                foreach (var item in _workList)
                {
                    this.Dispatcher.Invoke(new Action<string, double>(this.SettingData), item.Key, index++ / max);

                    if (item.Value != null)
                    {
                        System.Threading.Thread.Sleep(100);

                        item.Value?.Invoke();
                    }
                }                
            }
            catch (Exception ex)
            {
                MsgBox.Show("프로그램 초기화에 실패하였습니다.\r\n"
                    + "상세내용은 로그파일을 참조 하세요.\r\n" + $@"경로:{Logger.RootPath}\년\월\일");

                Logger.Write(this, ex);
            }
            finally
            {
                this.Dispatcher.Invoke(this.cmpAction);

                _workList.Clear();
                this.Dispatcher.Invoke(this.Close);                
            }
        }

        public static void SetData(string name, Action doWork) => _workList[name] = doWork;

        public static void Start(Action cmpAction)
        {
            var view = new StartControl(cmpAction);
            view.ShowDialog();
        }
    }
}
