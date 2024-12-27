using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using TS.FW.Diagnostics;
using TS.FW.Plc.Config;
using TS.FW.Plc.Dto;
using TS.FW.Plc.Test.Managers;
using TS.FW.Wpf.Controls;
using TS.FW.Wpf.Controls.Pu;
using TS.FW.Wpf.Core;
using TS.FW.Wpf.Helper;

namespace TS.FW.Plc.Test.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly BackgroundTimer _trUpdate = new BackgroundTimer();

        public System.Collections.ObjectModel.ObservableCollection<IMenuViewModel> MenuList { get; set; } = new System.Collections.ObjectModel.ObservableCollection<IMenuViewModel>();

        public IMenuViewModel SelectedMenu { get => this.GetValue<IMenuViewModel>(); set => this.SetValue(value); }

        public MainViewModel()
        {
            Logger.FileDeleteDay = 25;
            Logger.LogClear();

            Logger.LogLevel = Logger.LogEventLevel.Information;
            ViewModelBase.SourceLevels = System.Diagnostics.SourceLevels.Critical; // Binding 오류 출력 레벨 조정 : 이런 오류 사라짐. -> System.Windows.Data Error: 4 : Cannot find source for binding with reference 'RelativeSource FindAncestor.....

            if (this.IsDesignMode) return;

            this.MainWindow.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                PuLodingView.ShowLoding("프로그램 초기화...", InitDoWork, InitCompleted);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void InitDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var model = sender as PuLodingViewModel;

                model.Caption = "데이터베이스 초기화...";
                model.Value = 0;
                System.Threading.Thread.Sleep(500);

                model.Caption = "데이터베이스(환경설정) 초기화...";

                for (int i = 0; i < 10; i++)
                {
                    model.Value += 1;
                    System.Threading.Thread.Sleep(30);
                }
                System.Threading.Thread.Sleep(500);

                model.Caption = "데이터베이스(알람) 초기화...";

                for (int i = 0; i < 10; i++)
                {
                    model.Value += 1;
                    System.Threading.Thread.Sleep(30);
                }
                System.Threading.Thread.Sleep(500);

                model.Caption = "데이터베이스(HSMS) 초기화...";

                for (int i = 0; i < 10; i++)
                {
                    model.Value += 1;
                    System.Threading.Thread.Sleep(30);
                }
                System.Threading.Thread.Sleep(500);

                model.Caption = "통신 설정(PLC) 초기화...";

                for (int i = 0; i < 10; i++)
                {
                    model.Value += 1;
                    System.Threading.Thread.Sleep(30);
                }
                System.Threading.Thread.Sleep(500);

                model.Caption = "통신 설정(Index) 초기화...";

                for (int i = 0; i < 10; i++)
                {
                    model.Value += 1;
                    System.Threading.Thread.Sleep(30);
                }
                System.Threading.Thread.Sleep(500);

                model.Caption = "통신 설정(Client) 초기화...";

                for (int i = 0; i < 10; i++)
                {
                    model.Value += 1;
                    System.Threading.Thread.Sleep(30);
                }
                System.Threading.Thread.Sleep(500);

                model.Caption = "통신 설정(HSMS) 초기화...";

                for (int i = 0; i < 30; i++)
                {
                    model.Value += 1;
                    System.Threading.Thread.Sleep(30);
                }
                System.Threading.Thread.Sleep(500);

                model.Caption = "프로그램 메뉴 생성 중....";

                for (int i = 0; i < 10; i++)
                {
                    model.Value += 1;
                    System.Threading.Thread.Sleep(30);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void InitCompleted(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in IMenuViewModel.ToMenuViewModelList<IMainMenuViewModel>())
                {
                    item.Init();
                    this.MenuList.Add(item);

                    System.Threading.Thread.Sleep(1000);
                    Wpf.Helper.DoEvent.DoEvents();
                }

                this.SelectedMenu = this.MenuList.First();

                Wpf.Helper.DoEvent.DoEvents();
                System.Threading.Thread.Sleep(1 * 1000);

                this._trUpdate.DoWork += _trUpdate_DoWork;
                this._trUpdate.SleepTimeMsc = 100;
                this._trUpdate.Start();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void _trUpdate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                if (this.SelectedMenu != null) this.SelectedMenu.Update();
            }
        }

        protected override void OnNotifyCommand(object commandParameter)
        {
            try
            {
                switch (commandParameter as string)
                {
                    case "EXIT":
                        {
                            BackgroundTimer.AllStop();
                            this.MainWindow.Close();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                MsgBox.ShowMsg(ex.Message);
            }
        }
    }

    public abstract class IMainMenuViewModel : IMenuViewModel
    {
        public abstract string Title { get; }

        public virtual string SubTitle { get; }
    }
}
