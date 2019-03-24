using System;
using System.Windows.Input;

namespace CodingArena
{
    public sealed class DelegateCommand : ICommand
    {
        private readonly Func<bool> myCanExecute;
        private readonly Action myExecute;

        public DelegateCommand(Action execute) : this(() => true, execute)
        {
        }

        public DelegateCommand(Func<bool> canExecute, Action execute)
        {
            myCanExecute = canExecute;
            myExecute = execute;
        }

        public bool CanExecute(object parameter) => myCanExecute();

        public void Execute(object parameter) => myExecute();

        public event EventHandler CanExecuteChanged;

        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}