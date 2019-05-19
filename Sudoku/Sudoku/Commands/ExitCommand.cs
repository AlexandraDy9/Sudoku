using System.Windows;

namespace Sudoku.Commands
{
    public class ExitCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            ((Window)parameter).Close();
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
