using Sudoku.Commands;
using Sudoku.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Sudoku.CommandsForGame
{
    public class SaveGameCommand : CommandBase
    {
        private ObservableCollection<ObservableCollection<Cell>> Elements;

        private string Path;

        public SaveGameCommand(ObservableCollection<ObservableCollection<Cell>> elements)
        {
            Elements = elements ?? throw new ArgumentNullException(nameof(elements));
        }

        public override void Execute(object parameter)
        {
            if (parameter is string name)
            {
                if (Elements.Count > 1)
                {
                    Path = name + ".txt";

                    if (File.Exists(Path))
                        File.Delete(Path);

                    Grid.SaveGameInfile(Elements, Path);
                    Elements.Clear();
                }
                else MessageBox.Show("Please start a game and than you can save it!!", "Info");
            }
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
