using System;
using System.Windows.Input;
using TS.FW.Wpf.Controls.Pu;

namespace TS.FW.Wpf.Core
{
    public abstract class DataModelBase : ModelBase, ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            try
            {
                var name = parameter as string;
                if (string.IsNullOrWhiteSpace(name)) return false;

                //return this.GetType().GetProperty(name) != null;

                return true;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return false;
            }
        }

        public void Execute(object parameter)
        {
            this.OnNotifyCommand(parameter);
        }

        protected virtual void OnNotifyCommand(object commandParameter)
        {
            try
            {
                var name = commandParameter as string;
                if (string.IsNullOrWhiteSpace(name)) return;

                var type = this.GetType();
                var info = type.GetProperty(name);
                if (info == null) return;

                if (info.PropertyType == typeof(sbyte))
                {
                    var old = Convert.ToSByte(info.GetValue(this));

                    var value = NumberPad.Show(old);
                    if (value == old) return;

                    info.SetValue(this, value);
                }
                else if (info.PropertyType == typeof(short))
                {
                    var old = Convert.ToInt16(info.GetValue(this));

                    var value = NumberPad.Show(old);
                    if (value == old) return;

                    info.SetValue(this, value);
                }
                else if (info.PropertyType == typeof(int))
                {
                    var old = Convert.ToInt32(info.GetValue(this));

                    var value = NumberPad.Show(old);
                    if (value == old) return;

                    info.SetValue(this, value);
                }
                else if (info.PropertyType == typeof(long))
                {
                    var old = Convert.ToInt64(info.GetValue(this));

                    var value = NumberPad.Show(old);
                    if (value == old) return;

                    info.SetValue(this, value);
                }
                else if (info.PropertyType == typeof(byte))
                {
                    var old = Convert.ToByte(info.GetValue(this));

                    var value = NumberPad.Show(old);
                    if (value == old) return;

                    info.SetValue(this, value);
                }
                else if (info.PropertyType == typeof(ushort))
                {
                    var old = Convert.ToUInt16(info.GetValue(this));

                    var value = NumberPad.Show(old);
                    if (value == old) return;

                    info.SetValue(this, value);
                }
                else if (info.PropertyType == typeof(uint))
                {
                    var old = Convert.ToUInt32(info.GetValue(this));

                    var value = NumberPad.Show(old);
                    if (value == old) return;

                    info.SetValue(this, value);
                }
                else if (info.PropertyType == typeof(ulong))
                {
                    var old = Convert.ToUInt64(info.GetValue(this));

                    var value = NumberPad.Show(old);
                    if (value == old) return;

                    info.SetValue(this, value);
                }
                else if (info.PropertyType == typeof(float))
                {
                    var old = Convert.ToSingle(info.GetValue(this));

                    var value = NumberPad.Show(old);
                    if (value == old) return;

                    info.SetValue(this, value);
                }
                else if (info.PropertyType == typeof(double))
                {
                    var old = Convert.ToDouble(info.GetValue(this));

                    var value = NumberPad.Show(old);
                    if (value == old) return;

                    info.SetValue(this, value);
                }
                else if (info.PropertyType == typeof(string))
                {
                    var old = Convert.ToString(info.GetValue(this));

                    var value = Controls.Pu.Keyboard.Show(old);
                    if (value == old) return;

                    info.SetValue(this, value);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                CommandLog.Write(this, commandParameter);
            }
        }
    }
}
