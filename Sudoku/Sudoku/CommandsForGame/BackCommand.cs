using Sudoku.Commands;
using System.Windows;

namespace Sudoku.CommandsForGame
{
    public class BackCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            MainWindow sudokuWindow = new MainWindow();
            sudokuWindow.Show();
            ((Window)parameter).Hide();
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
