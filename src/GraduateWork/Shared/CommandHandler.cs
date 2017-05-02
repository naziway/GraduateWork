using System;
using System.Windows.Input;

namespace Shared
{
    public class CommandHandler : ICommand
    {
        public CommandHandler(Action action, bool canExecute = true)
        {
            Action = action;
            CanExecuteCommand = canExecute;
        }

        private Action Action { get; }
        private bool CanExecuteCommand { get; }

        public bool CanExecute(object parameter)
        {
            return CanExecuteCommand;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            Action();
        }
    }

    public class CommandWithParameters : ICommand
    {
        public CommandWithParameters(Action<object> action, bool canExecute = true)
        {
            Action = action;
            CanExecuteCommand = canExecute;
        }

        private Action<object> Action { get; }
        private bool CanExecuteCommand { get; }

        public bool CanExecute(object parameter)
        {
            return CanExecuteCommand;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            Action(parameter);
        }
    }

}