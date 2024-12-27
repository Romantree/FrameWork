using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using TS.FW.Wpf.Helper;

namespace TS.FW.Wpf.Controls.Pu
{
    /// <summary>
    /// PuLodingView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PuLodingView : Window
    {
        public PuLodingView()
        {
            InitializeComponent();
        }

        public static void ShowLoding(string title, DoWorkEventHandler dowork, RunWorkerCompletedEventHandler completed)
        {
            PuLodingViewModel.Show(title, dowork, completed);
        }
    }

    public class PuLodingViewModel : ViewModelBase
    {
        private readonly PuLodingView _view;
        private readonly BackgroundWorker worker = new BackgroundWorker();
        private readonly DoWorkEventHandler _dowork;
        private readonly RunWorkerCompletedEventHandler _workerCompleted;

        public string Title { get => this.GetValue<string>(); set => this.SetValue(value); }

        public string Caption { get => this.GetValue<string>(); set => this.SetValue(value); }

        public int Value { get => this.GetValue<int>(); set => this.SetValue(value); }

        internal PuLodingViewModel(string title, DoWorkEventHandler dowork, RunWorkerCompletedEventHandler completed)
        {
            this.Title = title;
            this._dowork = dowork;
            this._workerCompleted = completed;

            this._view = new PuLodingView()
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                DataContext = this,
            };

            this._view.Loaded += _view_Loaded;
            this._view.Closed += _view_Closed;

            this.worker.DoWork += Worker_DoWork;
            this.worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            this.worker.WorkerSupportsCancellation = true;
            this.worker.WorkerReportsProgress = true;
        }

        public void SetCaption(string caption, int value, int delayTime = 100)
        {
            this.Caption = caption;
            this.Value = value;

            System.Threading.Thread.Sleep(delayTime);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                this._dowork?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void _view_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTimeHelper.Ins.StartWatch();
                this.worker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void _view_Closed(object sender, EventArgs e)
        {
            try
            {
                DateTimeHelper.Ins.StopWatch();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                this._workerCompleted?.Invoke(this, e);

                this._view.Close();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Close()
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    this._view.Close();
                });
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        internal void ShowDialog()
        {
            this._view.ShowDialog();
        }

        internal static void Show(string title, DoWorkEventHandler dowork, RunWorkerCompletedEventHandler completed)
        {
            var view = new PuLodingViewModel(title, dowork, completed);
            view.ShowDialog();
        }
    }
}
