using System;
using System.Windows.Input;

namespace Sudoku.CommandsForGame
{
    public class RelayCommand : ICommand
    {
        private Action Function;
        private Func<bool> Predicate;

        public RelayCommand(Action Function)
        {
            this.Function = Function;
        }

        public RelayCommand(Action Function, Func<bool> Predicate)
        {
            this.Function = Function;
            this.Predicate = Predicate;
        }

        public bool CanExecute(object parameter)
        {
            if (this.Predicate != null)
            {
                return this.Predicate();
            }

            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (this.Function != null)
            {
                this.Function();
            }
        }
    }
}
