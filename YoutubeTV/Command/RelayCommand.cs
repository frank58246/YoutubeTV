using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace YoutubeTV.Command
{
    public class RelayCommand : ICommand
    {
        private readonly Func<bool> _canExecute;

        private readonly Action _execute;

        // 建構子(多型)
        public RelayCommand(Action execute) :
            this(execute, null)
        {
        }

        // 建構子(傳入參數)
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
        }

        // 當_canExecute發生變更時，加入或是移除Action觸發事件
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null) CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_canExecute != null) CommandManager.RequerySuggested += value;
            }
        }

        // 下面兩個方法是提供給 View 使用的
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}