using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TS.FW.Wpf.Core;
using TS.FW.Wpf.Helper;

namespace TS.FW.Wpf.Popup
{
    public abstract class IPopupBase : ViewModelBase
    {
        public static event EventHandler OnShow;
        public static event EventHandler OnClose;

        public virtual FrameworkElement Control { get; protected set; }

        public bool IsShowDialog { get; private set; }

        public virtual bool IsShowLock { get; protected set; } = false;

        public virtual bool IsMove { get; protected set; } = true;

        public virtual bool IsCenter { get; protected set; } = true;

        public virtual double Left { get; protected set; } = 0;

        public virtual double Top { get; protected set; } = 0;

        public bool? DialogResult { get; protected set; } = null;

        public virtual void InitControl<T>() where T : FrameworkElement
        {
            DoEvent.DoEvents();

            Application.Current.Dispatcher.Invoke(() =>
            {
                this.Control = (T)Activator.CreateInstance(typeof(T));
                this.Control.DataContext = this;
                this.Control.Tag = this;
            });
        }

        public virtual void Show()
        {
            try
            {
                this.IsShowDialog = false;
                OnShow?.Invoke(this, new EventArgs());
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public virtual bool ShowDialog()
        {
            try
            {
                this.IsShowDialog = true;
                OnShow?.Invoke(this, new EventArgs());

                do
                {
                    System.Threading.Thread.Sleep(10);

                    DoEvent.DoEvents();

                } while (this.DialogResult.HasValue == false);

                return this.DialogResult.Value;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return false;
            }
        }

        public virtual void Close()
        {
            try
            {
                if (this.DialogResult.HasValue == false) this.DialogResult = false;

                OnClose?.Invoke(this, new EventArgs());
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
