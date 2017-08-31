using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PingPongGame
{
    class CustomCommand : ICommand
    {
        Func<object, bool> canExecute;

        Action<object> execute;

        Action<object> unexecute;

        public CustomCommand(Func<object, bool> can, Action<object> exec)
        {
            this.canExecute = can;
            this.execute = exec;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
