using Sudoku.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace Sudoku
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<User> users;
        private List<BitmapImage> images;

        private User selectedUser;
        private BitmapImage selectedImage;

        private const string path = "Users.txt";

        public AddPersonCommand AddPersonCommand { get; }
        public DeleteUserCommand DeleteUserCommand { get; }
        public PlayGameCommand PlayGameCommand { get; }
        public ExitCommand ExitCommand { get;  }

        public User SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<User> Users
        {
            get => users;
            set
            {
                users = value;
                OnPropertyChanged();
            }
        }

        public List<BitmapImage> Images
        {
            get => images;
            set
            {
                images = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage SelectedImage
        {
            get => selectedImage;
            set
            {
                selectedImage = value;
                OnPropertyChanged();
            }
        }

        public ViewModel()
        {
            Users = new ObservableCollection<User>();
            Images = new List<BitmapImage>
            {
                new BitmapImage(new Uri("Photo/chihuahua.png", UriKind.Relative)),
                new BitmapImage(new Uri("Photo/bird.png", UriKind.Relative)),
                new BitmapImage(new Uri("Photo/cat.png", UriKind.Relative)),
                new BitmapImage(new Uri("Photo/panda.png", UriKind.Relative)),
                new BitmapImage(new Uri("Photo/pony.png", UriKind.Relative)),
                new BitmapImage(new Uri("Photo/whale.png", UriKind.Relative)),
                new BitmapImage(new Uri("Photo/pinguin.png", UriKind.Relative)),
                new BitmapImage(new Uri("Photo/bunny.png", UriKind.Relative)),
                new BitmapImage(new Uri("Photo/frog.png", UriKind.Relative)),
                new BitmapImage(new Uri("Photo/bee.png", UriKind.Relative))
            };

            string line;
            using (StreamReader file = new StreamReader(path))
            {
                while ((line = file.ReadLine()) != null)
                {
                    string[] words = line.Split(',');
                    Users.Add(new User(words[0], new BitmapImage(new Uri(words[1], UriKind.Relative)), words[2], words[3]));
                }

                file.Dispose();
            }

            AddPersonCommand = new AddPersonCommand(Users);
            DeleteUserCommand = new DeleteUserCommand(Users);
            PlayGameCommand = new PlayGameCommand();
            ExitCommand = new ExitCommand();
        }
    }
}
