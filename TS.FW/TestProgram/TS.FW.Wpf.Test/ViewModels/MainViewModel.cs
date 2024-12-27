using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Wpf.Core;
using System.IO.Ports;
using TS.FW.Wpf.Controls;
using System.Runtime.CompilerServices;
using TS.FW.Wpf.Converters;
using System.Windows.Media;
using TS.FW.Wpf.Controls.Pu;
using TS.FW.Wpf.Test.Models;
using TS.FW.Wpf.Helper;
using TS.FW.Diagnostics;
using System.Windows.Controls;
using System.ComponentModel;

namespace TS.FW.Wpf.Test.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly BackgroundTimer _trUpdatet = new BackgroundTimer();

        public ObservableCollection<IMenuViewModel> MenuList { get; set; } = new ObservableCollection<IMenuViewModel>();

        public IMenuViewModel SelectedMenu { get => this.GetValue<IMenuViewModel>(); set => this.SetValue(value); }

        public MainViewModel()
        {
            MainViewModel.SourceLevels = System.Diagnostics.SourceLevels.Critical;
            OnOffColorConverter.InitColorList(this.ToOnOffColor().ToArray());

            if (this.IsDesignMode) return;

            this.MainWindow.Loaded += MainWindow_Loaded;
            this.MainWindow.Closed += MainWindow_Closed;
        }

        private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            PuLodingView.ShowLoding("프로그램 초기화...", InitDoWork, InitCompleted);
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            BackgroundTimer.AllStop();
        }

        private void InitDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var model = sender as PuLodingViewModel;

                model.Caption = "프로그램 메뉴 생성 중....";
                model.Value = 0;
                System.Threading.Thread.Sleep(500);

                for (int i = 0; i < 100; i++)
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

                this._trUpdatet.DoWork += _trUpdatet_DoWork;
                this._trUpdatet.SleepTimeMsc = 100;
                this._trUpdatet.Start();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void _trUpdatet_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
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
                if (this.SelectedMenu != null)
                {
                    this.SelectedMenu.Update();
                }
            }
        }

        private IEnumerable<OnOffColor> ToOnOffColor()
        {
            yield return OnOffColor.ToSolidColor("MENU", Colors.Lime, Colors.LightGray);
            yield return OnOffColor.ToSolidColor("ONOFF", Colors.Lime, Colors.DimGray);
        }
    }
}
