using Sudoku.Commands;
using System.Windows;

namespace Sudoku.CommandsForGame
{
    public class AboutCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            MessageBox.Show(" Nume: Nica\n Prenume: Diana - Alexandra\n Grupa: 10LF373\n Specializarea: IA", "Info");
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
