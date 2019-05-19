using System;
using System.Collections.ObjectModel;
using System.IO;

namespace Sudoku.Commands
{
    public class DeleteUserCommand : CommandBase
    {
        private readonly ObservableCollection<User> users;

        private const string path = "Users.txt";

        public DeleteUserCommand(ObservableCollection<User> users)
        {
            this.users = users ?? throw new ArgumentNullException(nameof(users));
        }

        public override void Execute(object parameter)
        {
            if (parameter is string name)
            {
                foreach (User user in users)
                {
                    if (user.Name.Equals(name))
                    {
                        users.Remove(user);

                        using (StreamWriter file = new StreamWriter(path))
                        {
                            foreach(User u in users)
                                file.WriteLine(u.Name + ',' + u.Picture + ',' + u.WonGames + ',' + u.LostGames);
                            file.Dispose();
                        }

                        string userPath = name + ".txt";
                        if (File.Exists(userPath))
                            File.Delete(userPath);

                        break;
                    }
                }
            }
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
