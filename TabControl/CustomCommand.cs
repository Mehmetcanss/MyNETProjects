using System;
using System.Windows.Input;

namespace TabControl.Commands
{
    public class CustomCommand : ICommand
    {
        public Func<object, bool> CanExecuteDelegate;
        public Action<object> ExecuteDelegate;
        public Action<object> UnexecuteDelegate;

        public CustomCommand(Func<object, bool> canExecute, Action<object> execute)
        {
            this.CanExecuteDelegate = canExecute;
            this.ExecuteDelegate = execute;
        }

        public CustomCommand(Action<object> execute)
        {
            this.CanExecuteDelegate = (par) => { return true; };
            this.ExecuteDelegate = execute;
        }

        public CustomCommand(Action<object> execute, Action<object> unexecute)
        {
            this.CanExecuteDelegate = (par) => { return true; };
            this.ExecuteDelegate = execute;
            this.UnexecuteDelegate = unexecute;
        }


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteDelegate.Invoke(parameter);
        }

        public void Execute(object parameter = null)
        {
            ExecuteDelegate.Invoke(parameter);
        }
        public void Unexecute(object parameter = null)
        {
            UnexecuteDelegate.Invoke(parameter);
        }

    }
}
