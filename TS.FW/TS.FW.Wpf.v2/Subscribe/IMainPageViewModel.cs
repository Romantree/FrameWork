using System;
using System.Runtime.CompilerServices;

namespace TS.FW.Wpf.v2.Subscribe
{
    public abstract class IMainPageViewModel : IPageViewModel
    {
        public override void Init()
        {
            try
            {
                base.Init();
                this.MethodCall();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public override void Show()
        {
            try
            {
                base.Show();
                this.MethodCall();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public override void Update()
        {
            try
            {
                base.Update();
                this.MethodCall();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void MethodCall([CallerMemberName] string name = null)
        {
            var info = this.GetType().GetProperty("Menu");
            if (info == null) return;

            var menu = info.GetValue(this);
            if (menu == null) return;

            var func = menu.GetType().GetMethod(name);
            if (func == null) return;

            func.Invoke(menu, null);
        }
    }
}
