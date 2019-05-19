using Sudoku.Commands;
using System.Windows;

namespace Sudoku.CommandsForGame
{
    public class StatisticsCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            if(parameter is User user)
                MessageBox.Show("User name: " + user.Name + " \nWon games: " + user.WonGames + " \nLost games: " + user.LostGames);
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
