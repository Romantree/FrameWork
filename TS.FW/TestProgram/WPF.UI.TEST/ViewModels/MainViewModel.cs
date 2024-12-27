using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TS.FW;
using TS.FW.Diagnostics;
using TS.FW.Wpf.v2.Controls.InPut;
using TS.FW.Wpf.v2.Controls.Win;
using TS.FW.Wpf.v2.Core;
using TS.FW.Wpf.v2.Helpers;
using TS.FW.Wpf.v2.Subscribe;

namespace WPF.UI.TEST.ViewModels
{
    public class MainViewModel : IViewModel
    {
        private readonly BackgroundTimer trUpdate = new BackgroundTimer(ApartmentState.MTA);

        public MainViewModel()
        {
            IViewModel.SourceLevels = System.Diagnostics.SourceLevels.Critical; // 바인딩 관련 로그 출력 X

            KeyPad.Scale = 1; // Keypad 크기 배율
            KeyboardPad.Scale = 1; // Keyboard 크기 배율

            if (IViewModel.IsDesignMode) return;

            this.trUpdate.SleepTimeMsc = 100;
            this.trUpdate.DoWork += trUpdate_DoWork;

            this.ProgramStart();
        }

        public bool IsAlarm { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool Buzzer { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool IsAuto { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public string UserLevel { get => this.GetValue<string>(); set => this.SetValue(value); }

        public TowerLampModel TowerLamp { get; set; } = new TowerLampModel();

        public ObservableCollection<IMainPageViewModel> MenuList { get; set; } = new ObservableCollection<IMainPageViewModel>();

        public IMainPageViewModel SelectedMenu { get => this.GetValue<IMainPageViewModel>(); set => this.SetValue(value); }

        private void ProgramStart()
        {
            // 프로그램 초기화 함수에는 try catch 사용하지 마세요. 오류 발생시 확인 필요
            //StartControl.SetData("Database 로딩", () => System.Threading.Thread.Sleep(500));
            //StartControl.SetData("Alarm 설정", () => System.Threading.Thread.Sleep(500));
            //StartControl.SetData("Recipe 불러오기", () => System.Threading.Thread.Sleep(500));
            //StartControl.SetData("Device 초기화", () => System.Threading.Thread.Sleep(500));
            //StartControl.SetData("In/Out 초기화", () => System.Threading.Thread.Sleep(500));
            //StartControl.SetData("Motion 초기화", () => System.Threading.Thread.Sleep(500));
            //StartControl.SetData("Network 연결", () => System.Threading.Thread.Sleep(500));
            StartControl.SetData("Memory 수집 시작", ProgramHelper.Ins.Start);
            StartControl.SetData("프로그램 메뉴 생성", InitMeneu);
            StartControl.SetData("프로그램 실행", ProcessStart);

            StartControl.Start(ProgramCmp);
        }

        private void ProgramCmp()
        {
            this.UserLevel = "Lock";

            this.SelectedMenu = MenuList.FirstOrDefault();
            this.SelectedMenu.Show();
        }

        private void InitMeneu() => IViewModel.Dispatcher.Invoke(MenuCreate);

        private void MenuCreate()
        {
            foreach (var item in IPageViewModel.ToPageViewList<IMainPageViewModel>())
            {
                item.Init();
                this.MenuList.Add(item);
            }
        }

        private void ProcessStart()
        {
            this.trUpdate.Start();
        }

        private void trUpdate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                TowerLamp.Update();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                this.SelectedMenu?.Update();
            }
        }

        protected override void OnCommand(object parameter)
        {
            try
            {
                switch (parameter as string)
                {
                    case "Auto/Manual":
                        {
                            this.IsAuto = !this.IsAuto;
                        }
                        break;
                    case "Log":
                        {
                            MsgBox.Show("프로그램을 종료하시겠습니까?");
                        }
                        break;
                    case "Login":
                        {
                            KeyboardPad.ShowPassword();

                            this.UserLevel = this.UserLevel == "Programmer" ? "Lock" : "Programmer";
                        }
                        break;
                    case "Buzzer":
                        {
                            this.Buzzer = !this.Buzzer;
                        }
                        break;
                    case "Alarm":
                        {
                            this.IsAlarm = !this.IsAlarm;
                        }
                        break;
                    case "EXIT":
                        {
                            BackgroundTimer.AllStop();
                            this.MainView.Close();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }

    public class TowerLampModel : IModel
    {
        private int isBlank = 0;

        public bool Red { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool Yellow { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool Green { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool Blue { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public void Update()
        {
            try
            {
                isBlank++;

                if (isBlank != 20) return;

                this.Red = !this.Red;
                this.Yellow = this.Red;
                this.Green = this.Red;
                this.Blue = this.Red;

                isBlank = 0;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
