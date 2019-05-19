using Sudoku.Commands;
using Sudoku.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Sudoku.CommandsForGame
{
    public class NewGameCommand : CommandBase
    {
        private ObservableCollection<ObservableCollection<Cell>> Elements;

        private int SizeOfGrid;
        private string Path;

        public NewGameCommand(ObservableCollection<ObservableCollection<Cell>> elements)
        {
            Elements = elements ?? throw new ArgumentNullException(nameof(elements));   
        }

        public override void Execute(object parameter)
        {
            SizeOfGrid = Elements.Count;

            if (Elements.Count > 1)
            {
                Elements.Clear();

                List<string> numbers = new List<string>();

                Path = Grid.GetRandomFile(SizeOfGrid);

                Grid.ReadFromFile(numbers, Path);

                Grid.CreateGrid(SizeOfGrid, Elements);
                Grid.PopulateGrid(numbers, Elements, SizeOfGrid, Path);
            }
            else
                MessageBox.Show("You have to choose a sudoku version! \nGo to Options.", "Info");
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
