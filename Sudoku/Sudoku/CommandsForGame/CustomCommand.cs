using Sudoku.Commands;
using Sudoku.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sudoku.CommandsForGame
{
    public class CustomCommand : CommandBase
    {
        private ObservableCollection<ObservableCollection<Cell>> Elements;

        private int SizeOfGrid;
        private string Path;

        public CustomCommand(ObservableCollection<ObservableCollection<Cell>> elements)
        {
            Elements = elements ?? throw new ArgumentNullException(nameof(elements));
        }

        public override void Execute(object parameter)
        {
            if (parameter is string size)
            {
                if (Elements.Count > 1)
                    Elements.Clear();

                SizeOfGrid = Int32.Parse(size);

                List<string> numbers = new List<string>();
                Path = Grid.GetRandomFile(SizeOfGrid);

                Grid.ReadFromFile(numbers, Path);

                Grid.CreateGrid(SizeOfGrid, Elements);
                Grid.PopulateGrid(numbers, Elements, SizeOfGrid, Path);
            }
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
