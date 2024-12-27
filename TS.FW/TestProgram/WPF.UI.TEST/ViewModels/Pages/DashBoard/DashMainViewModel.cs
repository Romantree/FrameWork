using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TS.FW;
using TS.FW.Wpf.v2.Controls.Win;
using TS.FW.Wpf.v2.Core;
using TS.FW.Wpf.v2.Helpers;
using WPF.UI.TEST.Views.Pages.DashBoard;

namespace WPF.UI.TEST.ViewModels.Pages.DashBoard
{
    public class DashMainViewModel : IDashViewModel
    {
        private readonly FrameworkElement view = new DashMainView();

        public override int No => 0;

        public override string Name => "Main";

        public override FrameworkElement View => view;

        public override Visual Icon => ResourceHelper.Ins["appbar_city_chicago", "Icons"] as Visual;

        public string ID { get => this.GetValue<string>(); set => this.SetValue(value); }

        public string PASSWORD { get => this.GetValue<string>(); set => this.SetValue(value); }

        public int Value { get => this.GetValue<int>(); set => this.SetValue(value); }

        public int Min { get => this.GetValue<int>(); set => this.SetValue(value); }

        public int Max { get => this.GetValue<int>(); set => this.SetValue(value); }

        public string Message { get => this.GetValue<string>(); set => this.SetValue(value); }

        public bool IsAuto { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool IsManual { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public List<string> List { get; set; } = new List<string>();

        public string SelectedItem { get => this.GetValue<string>(); set => this.SetValue(value); }

        public DateTime Date { get => this.GetValue<DateTime>(); set => this.SetValue(value); }

        public override void Init()
        {
            this.Date = DateTime.Now;

            base.Init();
            IsAuto = false;

            for (int i = 1; i <= 50; i++)
            {
                List.Add(i.ToString());
            }

            this.SelectedItem = "1";
        }

        protected override void OnCommand(object parameter)
        {
            try
            {
                switch (parameter as string)
                {
                    case "CLOSE":
                        {
                            MsgBox.Show(this.Message);
                        }
                        break;
                    case "YES/NO":
                        {
                            MsgBox.Show(this.Message, MsgBoxType.YES_NO);
                        }
                        break;
                    case "OK/CANCLE":
                        {
                            MsgBox.Show(this.Message, MsgBoxType.OK_CANCEL);
                        }
                        break;
                    case "LOGIN":
                        {
                            MsgBox.Show($"ID:{this.ID}\r\nPASSWORD:{this.PASSWORD}", MsgBoxType.OK_CANCEL);
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
}
