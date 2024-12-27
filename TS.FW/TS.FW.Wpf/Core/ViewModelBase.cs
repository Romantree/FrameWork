using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace TS.FW.Wpf.Core
{
    /// <summary>
    /// View Modle 베이스 클래스 정의
    ///  -  모든 ViewModle 클래스는 해당 클래스를 상속 받아야 된다.
    /// </summary>\
    public abstract class ViewModelBase : ModelBase, ICommand
    {
        private static List<ViewModelBase> _AllViewModel = new List<ViewModelBase>();

        public Dispatcher Dispatcher { get; private set; }

        public static SourceLevels SourceLevels { get => PresentationTraceSources.DataBindingSource.Switch.Level; set => PresentationTraceSources.DataBindingSource.Switch.Level = value; }

        public Window MainWindow
        {
            get
            {
                return Application.Current.MainWindow;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return Dispatcher.Thread != Thread.CurrentThread;
            }
        }

        /// <summary>
        /// 명령 사용여부
        /// </summary>
        public bool IsCommandEnable = true;

        /// <summary>
        /// 명령 실행 변경 알림 이벤트
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        protected ViewModelBase()
        {
            _AllViewModel.Add(this);
            Dispatcher = Dispatcher.CurrentDispatcher;
            //UseFlag = true;
        }

        /// <summary>
        /// 명령 실행 여부 확인
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return this.IsCommandEnable;
        }

        /// <summary>
        /// 명령 실행
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            try
            {
                if (parameter == null) return;

                this.OnNotifyCommand(parameter);
            }
            finally
            {
                CommandLog.Write(this, parameter);
            }
        }

        /// <summary>
        /// 명령 실행 알림
        /// </summary>
        /// <param name="commandParameter">명령 인수</param>
        protected virtual void OnNotifyCommand(object commandParameter) { }

        public void OnAllNotifyCommand(object commandParameter)
        {
            foreach (var viewModel in _AllViewModel.ToList())
            {
                if (viewModel == this) continue;

                viewModel.OnNotifyCommand(commandParameter);
            }
        }

        public void OnPageNotifyCommand(string type, object commandParameter)
        {
            foreach (var viewModel in _AllViewModel.ToList())
            {
                if (viewModel == this) continue;
                var name = viewModel.ToString();
                if (name.Contains(type))
                {
                    viewModel.OnNotifyCommand(commandParameter);
                    break;
                }
            }
        }
    }
}
