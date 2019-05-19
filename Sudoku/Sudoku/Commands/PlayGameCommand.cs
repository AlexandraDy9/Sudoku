using System;
using System.Windows;

namespace Sudoku.Commands
{
    public class PlayGameCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            if (parameter != null)
            {
                var values = ((object[])parameter);
                User user = (User)values[0];
                if (user != null)
                {
                    var window = values[1];
                    SudokuWindow gameWindow = new SudokuWindow(user);
                    gameWindow.Show();
                    ((Window)window).Hide();
                }
            }
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
