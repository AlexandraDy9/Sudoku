using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media.Imaging;

namespace Sudoku.Commands
{
    public class AddPersonCommand : CommandBase
    {
        private ObservableCollection<User> Users;

        private const string path = "Users.txt";

        public AddPersonCommand(ObservableCollection<User> names)
        {
            Users = names ?? throw new ArgumentNullException(nameof(names));
        }

        public override void Execute(object parameter)
        {
            if (parameter != null)
            {
                var values = ((object[])parameter);
                BitmapImage image = (BitmapImage)values[0];
                var name = values[1].ToString();

                string userImage = image.ToString();
                int pFrom = userImage.IndexOf("component/") + "component/".Length;
                String result = userImage.Substring(pFrom, userImage.Length - pFrom);

                User user = new User(name, new BitmapImage(new Uri(result, UriKind.Relative)), "0", "0");
                Users.Add(user);

                using (StreamWriter file = File.AppendText(path))
                {
                    file.WriteLine(user.Name + ',' + user.Picture + ',' + user.WonGames + ',' + user.LostGames);
                    file.Dispose();
                }
            }
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
