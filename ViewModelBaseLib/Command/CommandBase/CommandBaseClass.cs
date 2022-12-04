using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModelBaseLib.Command.CommandBase
{
    public abstract class CommandBaseClass : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value; 
        }

        public abstract bool CanExecute(object? parameter); //Можно ли выполнить команду

        public abstract void Execute(object? parameter); //Тут выполняется комманда
    }
}
