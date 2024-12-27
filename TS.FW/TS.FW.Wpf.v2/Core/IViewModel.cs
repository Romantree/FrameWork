using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace TS.FW.Wpf.v2.Core
{
    public abstract class IViewModel : IModel, ICommand
    {
        public static Dispatcher Dispatcher { get; private set; }

        public static bool IsDesignMode => DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject());

        public static SourceLevels SourceLevels { get => PresentationTraceSources.DataBindingSource.Switch.Level; set => PresentationTraceSources.DataBindingSource.Switch.Level = value; }

        public Window MainView => Application.Current.MainWindow;

        static IViewModel() => Dispatcher = Dispatcher.CurrentDispatcher;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => this.OnCommand(parameter);

        protected virtual void OnCommand(object parameter) { }
    }
}
