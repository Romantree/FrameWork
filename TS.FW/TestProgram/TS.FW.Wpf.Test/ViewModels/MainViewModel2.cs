using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Wpf.Core;
using TS.FW.Wpf.Test.Models;

namespace TS.FW.Wpf.Test.ViewModels
{
    public class MainViewModel2 : ViewModelBase
    {
        public System.Collections.ObjectModel.ObservableCollection<object> TestList { get; set; } = new System.Collections.ObjectModel.ObservableCollection<object>();

        public System.Collections.ObjectModel.ObservableCollection<object> TestList2 { get; set; } = new System.Collections.ObjectModel.ObservableCollection<object>();

        public System.Collections.ObjectModel.ObservableCollection<object> TestList3 { get; set; } = new System.Collections.ObjectModel.ObservableCollection<object>();

        public MainViewModel2()
        {
            var list = new List<DataTestModel>();

            for (int i = 0; i < 20; i++)
            {
                list.Add(new DataTestModel() { Int = i });
            }

            var aa = TS.FW.Utils.CollectionsHandler.ToCsv(list);
        }

        protected override void OnNotifyCommand(object commandParameter)
        {
            switch (commandParameter as string)
            {
                case "ADD":
                    {
                        var a = TS.FW.Wpf.Controls.Pu.Keyboard.Show();

                        this.TestList.Add(new object());
                    }
                    break;
                case "RED":
                    {
                        this.TestList.RemoveAt(0);
                    }
                    break;
                case "ADD_100":
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            this.TestList.Add(new object());
                        }
                    }
                    break;

                case "2ADD":
                    {
                        this.TestList2.Add(new object());
                    }
                    break;
                case "2RED":
                    {
                        this.TestList2.RemoveAt(0);
                    }
                    break;
                case "2ADD_100":
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            this.TestList2.Add(new object());
                        }
                    }
                    break;


                case "3ADD":
                    {
                        this.TestList3.Add(new object());
                    }
                    break;
                case "3RED":
                    {
                        this.TestList3.RemoveAt(0);
                    }
                    break;
                case "3ADD_100":
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            this.TestList3.Add(new object());
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
