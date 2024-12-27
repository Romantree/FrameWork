using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TS.FW;
using TS.FW.Wpf.v2.Core;
using TS.FW.Wpf.v2.Helpers;
using WPF.UI.TEST.Views.Pages.DashBoard;
using WPF.UI.TEST.Views.Pages.Maint;

namespace WPF.UI.TEST.ViewModels.Pages.Maint
{
    public class MaintManualViewModel : IMaintViewModel
    {
        private readonly FrameworkElement view = new MaintManualView();

        public override int No => 0;

        public override string Name => "Manual";

        public override FrameworkElement View => view;

        public override Visual Icon => ResourceHelper.Ins["ts_Manual", "TsIcon"] as Visual;

        public ObservableCollection<TestModel> List { get; set; } = new ObservableCollection<TestModel>();

        public override void Init()
        {
            for (int i = 0; i < 100; i++)
            {
                List.Add(i);
            }

            base.Init();
        }

        protected override void OnCommand(object parameter)
        {
            try
            {
                switch (parameter as string)
                {
                    case "SETTING":
                        {
                            List.Clear();

                            for (int i = 0; i < 1000; i++)
                            {
                                List.Add(i);
                            }
                        }
                        break;
                    case "CLEAR":
                        {
                            List.Clear();
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

    public class TestModel : IModel
    {
        public static Random Random = new Random((int)DateTime.Now.Ticks);

        public int No { get => this.GetValue<int>(); set => this.SetValue(value); }

        public string Name { get => this.GetValue<string>(); set => this.SetValue(value); }

        public bool Check01 { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool Radio1 { get => this.GetValue<bool>(); set => SetValue(value); }

        public bool Radio2 { get => this.GetValue<bool>(); set => SetValue(value); }

        public static implicit operator TestModel(int i) => new TestModel()
        {
            No = i,
            Name = Random.Next(0, 1000000000).ToString().PadLeft(10, '0'),
        };
    }
}
