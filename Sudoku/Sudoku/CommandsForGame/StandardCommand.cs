using Sudoku.Commands;
using Sudoku.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Sudoku.CommandsForGame
{
    public class StandardCommand : CommandBase
    {
        private ObservableCollection<ObservableCollection<Cell>> Elements;

        private string Path;
        private const int SizeOfTable = 9;

        public StandardCommand(ObservableCollection<ObservableCollection<Cell>> elements)
        {
            Elements = elements ?? throw new ArgumentNullException(nameof(elements));
        }

        public override void Execute(object parameter)
        {
            if (Elements.Count > 1)
                Elements.Clear();

            List<string> numbers = new List<string>();
            Path = Grid.GetRandomFileForStandard();

            Grid.ReadFromFile(numbers, Path);

            Grid.CreateGrid(SizeOfTable, Elements);
            Grid.PopulateGrid(numbers, Elements, SizeOfTable, Path);
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
