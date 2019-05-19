using Sudoku.Commands;
using Sudoku.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Sudoku.CommandsForGame
{
    public class OpenGameCommand : CommandBase
    {
        private ObservableCollection<ObservableCollection<Cell>> Elements;

        private int SizeOfGrid;
        private string Path;

        public OpenGameCommand(ObservableCollection<ObservableCollection<Cell>> elements)
        {
            Elements = elements ?? throw new ArgumentNullException(nameof(elements));
        }

        public override void Execute(object parameter)
        {
            if (parameter is string name)
            {
                if (Elements.Count > 1)
                    Elements.Clear();

                List<string> numbers = new List<string>();

                Path = name + ".txt";

                if (File.Exists(Path))
                {
                    Grid.ReadFromFile(numbers, Path);

                    SizeOfGrid = (int)Math.Sqrt(numbers.Count);

                    Grid.CreateGrid(SizeOfGrid, Elements);
                    Grid.PopulateGrid(numbers, Elements, SizeOfGrid, Path);

                    MessageBox.Show(Path + " was opened");
                }

                else MessageBox.Show("You don`t have any game saved.", "Info");
            }
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
