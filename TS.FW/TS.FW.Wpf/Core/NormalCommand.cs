using System;
using System.Windows.Input;

namespace TS.FW.Wpf.Core
{
    public class NormalCommand : ICommand
    {
        private readonly Action<object> _action = null;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public NormalCommand(Action<object> action)
        {
            this._action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                this._action?.Invoke(parameter);
            }
            finally
            {
                CommandLog.Write(this, parameter);
            }
        }
    }
}
