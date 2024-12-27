using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TS.FW.Wpf.Controls;
using TS.FW.Wpf.Controls.Pu;
using TS.FW.Wpf.Core;
using TS.FW.Wpf.Helper;
using TS.FW.Wpf.Test.Models;
using TS.FW.Wpf.Test.Views.Page;

namespace TS.FW.Wpf.Test.ViewModels.Page
{
    public class AutoRunViewModel : IMainMenuViewModel
    {
        private readonly AutoRunView _view = new AutoRunView();

        public override int No => 1;

        public override string Name => "Auto Run";

        public override ContentControl View => _view;

        public override eIconType Icon => eIconType.MonitorPlay;

        public string ProcessState { get => this.GetValue<string>(); set => this.SetValue(value); }

        public string ButtonMsg { get => this.GetValue<string>(); set => this.SetValue(value); }

        public List<eIconType> IconList { get; set; } = new List<eIconType>();

        public eIconType SelectedIcon { get => this.GetValue<eIconType>(); set => this.SetValue(value); }

        public DateTime DateTime { get => this.GetValue<DateTime>(); set => this.SetValue(value); }

        public NormalCommand OnLeftDownCmd => new NormalCommand((o) => { this.ButtonMsg = "OnLeftDownCmd"; });

        public NormalCommand OnLeftUpCmd => new NormalCommand((o) => { this.ButtonMsg = "OnLeftUpCmd"; });

        public NormalCommand OnRightDownCmd => new NormalCommand((o) => { this.ButtonMsg = "OnRightDownCmd"; });

        public NormalCommand OnRightUpCmd => new NormalCommand((o) => { this.ButtonMsg = "OnRightUpCmd"; });

        public NormalCommand OnDoubleButton => new NormalCommand((o) => { this.ButtonMsg = "OnDoubleButton"; });

        public string PopupMsg { get => this.GetValue<string>(); set => this.SetValue(value); }

        public string Password { get => this.GetValue<string>(); set => this.SetValue(value); }

        public NormalCommand OnPopupCommand => new NormalCommand(PopupCommand);

        public NormalCommand OnTestCommand => new NormalCommand(TestCommand);

        private void TestCommand(object param)
        {
            if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftCtrl))
            {

            }
        }

        public DataTestModel DataTest { get => this.GetValue<DataTestModel>(); set => this.SetValue(value); }
        private DataTestModel _DataTest;

        public bool IsSave { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public override void Init()
        {
            try
            {
                base.Init();

                this.DateTime = DateTime.Now;

                this.ProcessState = "Stop";
                this.Password = "";

                foreach (eIconType item in Enum.GetValues(typeof(eIconType)))
                {
                    IconList.Add(item);
                }

                this.DataTest = new DataTestModel();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public override void Hide()
        {
            try
            {
                if (this.IsSave == true)
                {
                    if (MsgBox.ShowMsg("수정 된 데이터가 있습니다.\r\n저장 하시겠습니까?", true) == true)
                    {
                        this.OnNotifyCommand("Save");
                    }
                    else
                    {
                        this.OnNotifyCommand("Refresh");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                base.Hide();
            }
        }

        public override void Update()
        {
            try
            {
                if (this._DataTest != null && this.DataTest != null)
                {
                    this.IsSave = this._DataTest.Check(this.DataTest);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        protected override void OnNotifyCommand(object commandParameter)
        {
            try
            {
                switch (commandParameter as string)
                {
                    case "Start":
                        {
                            this.ProcessState = "Start";
                        }
                        break;
                    case "Stop":
                        {
                            this.ProcessState = "Stop";
                        }
                        break;
                    case "Exit":
                        {
                            if (MsgBox.ShowMsg("프로그램 종료 하시겠습니까?", true) == false) return;

                            App.Current.MainWindow.Close();
                        }
                        break;
                    case "Refresh":
                        {
                            this.DataTest = this._DataTest;
                        }
                        break;
                    case "Save":
                        {
                            this.DataTest = this.DataTest;
                            DataHelper.Save("Test");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void PopupCommand(object param)
        {
            try
            {
                switch (param as string)
                {
                    case "Message Box [OK]":
                        {
                            MsgBox.ShowMsg(param as string, false);
                        }
                        break;
                    case "Message Box [YES NO]":
                        {
                            var res = MsgBox.ShowMsg(param as string, true);

                            this.PopupMsg = string.Format("Message Box [{0}]", res);
                        }
                        break;
                    case "Message Box [Invoke]":
                        {
                            Task.Factory.StartNew(() => 
                            {
                                var res = MsgBox.ShowMsgInvoke(param as string, true);

                                this.PopupMsg = string.Format("Message Box [{0}]", res);
                            });
                        }
                        break;
                    case "Number Pad":
                        {
                            var value = NumberPad.Show(0.0D);
                            if (value == 0) return;

                            this.PopupMsg = string.Format("NumberPad [{0}]", value);
                        }
                        break;
                    case "Key Pad":
                        {
                            var value = Keyboard.Show("");
                            if (string.IsNullOrWhiteSpace(value)) return;

                            this.PopupMsg = string.Format("Keyboard [{0}]", value);
                        }
                        break;
                    case "Password":
                        {
                            var value = Keyboard.Show(this.Password);
                            if (value == this.Password) return;

                            this.Password = value;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        protected override void SetValue(object value, [CallerMemberName] string propertyName = null)
        {
            try
            {
                base.SetValue(value, propertyName);
            }
            finally
            {
                if(string.Equals(propertyName, nameof(this.DataTest)))
                {
                    this._DataTest = this.DataTest.DeppCopy();
                }
            }
        }
    }
}
