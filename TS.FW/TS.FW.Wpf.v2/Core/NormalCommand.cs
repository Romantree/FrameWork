using System;
using System.Windows.Input;

namespace TS.FW.Wpf.v2.Core
{
    public class NormalCommand : ICommand
    {
        private readonly Action<object> _cmd = null;

        public NormalCommand(Action<object> cmd) => _cmd = cmd;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _cmd != null;

        public void Execute(object parameter) => _cmd.Invoke(parameter);
    }
}
