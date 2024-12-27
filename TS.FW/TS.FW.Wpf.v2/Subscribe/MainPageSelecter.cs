using System;
using System.Collections.ObjectModel;
using System.Linq;
using TS.FW.Wpf.v2.Core;

namespace TS.FW.Wpf.v2.Subscribe
{
    public class MainPageSelecter<T> : IModel where T : IPageViewModel
    {
        public ObservableCollection<T> SubMenuList { get; set; } = new ObservableCollection<T>();

        public T SubSelectedMenu { get => this.GetValue<T>(); set => this.SetValue(value); }

        public void Init()
        {
            foreach (var item in IPageViewModel.ToPageViewList<T>())
            {
                item.Init();
                this.SubMenuList.Add(item);
            }
        }

        public void Show()
        {
            try
            {
                if (this.SubSelectedMenu == null)
                {
                    this.SubSelectedMenu = this.SubMenuList.FirstOrDefault();
                    this.SubSelectedMenu.Show();
                }
                else
                {
                    var menu = this.SubSelectedMenu;
                    this.SubSelectedMenu = null;
                    this.SubSelectedMenu = menu;
                    this.SubSelectedMenu.Show();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void Update()
        {
            try
            {
                if (this.SubSelectedMenu == null) return;

                this.SubSelectedMenu.Update();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
