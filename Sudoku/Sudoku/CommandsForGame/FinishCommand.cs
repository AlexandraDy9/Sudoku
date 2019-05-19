using Sudoku.Commands;
using Sudoku.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Sudoku.CommandsForGame
{
    public class FinishCommand : CommandBase
    {
        private ObservableCollection<ObservableCollection<Cell>> Elements;

        private int SizeOfGrid;
        private string Path;

        public FinishCommand(ObservableCollection<ObservableCollection<Cell>> elements)
        {
            Elements = elements ?? throw new ArgumentNullException(nameof(elements));
        }

        public override void Execute(object parameter)
        {
            if (parameter is User user)
            {
                if (Elements.Count > 1)
                {
                    SizeOfGrid = Elements.Count;

                    Path = Grid.GetTheResolvedFile(Elements);

                    List<string> numbers = new List<string>();

                    Grid.ReadFromFile(numbers, Path);

                    if (Grid.FinishGame(numbers, Elements, SizeOfGrid))
                    {
                        int value = Int32.Parse(user.WonGames);
                        int won = value + 1;
                        user.WonGames = won.ToString();
                    }
                    else
                    {
                        int value = Int32.Parse(user.LostGames);
                        int lost = value + 1;
                        user.LostGames = lost.ToString();
                    }

                    ReadAndWriteUser(user);

                    Elements.Clear();
                    Grid.CreateGrid(SizeOfGrid, Elements);
                }

                else MessageBox.Show("Please start a game!", "Info");
            }
        }


        private const string userPath = "Users.txt";
        private void ReadAndWriteUser(User user)
        {
            List<User> Users = new List<User>();
            string line;

            //first read all users from file
            using (StreamReader file = new StreamReader(userPath))
            {
                while ((line = file.ReadLine()) != null)
                {
                    string[] words = line.Split(',');
                    Users.Add(new User(words[0], new BitmapImage(new Uri(words[1], UriKind.Relative)), words[2], words[3]));
                }

                file.Dispose();
            }

            //change the user points 
            foreach (User u in Users)
            {
                if(u.Name.Equals(user.Name))
                {
                    u.WonGames = user.WonGames;
                    u.LostGames = user.LostGames;
                }
            }

            //write all users in file
            using (StreamWriter file = new StreamWriter(userPath))
            {
                foreach (User u in Users)
                    file.WriteLine(u.Name + ',' + u.Picture + ',' + u.WonGames + ',' + u.LostGames);
                file.Dispose();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
