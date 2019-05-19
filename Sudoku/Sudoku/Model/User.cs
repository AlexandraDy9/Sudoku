using System.Windows.Media.Imaging;

namespace Sudoku
{
    public class User
    {
        public string Name { get; set; }
        public BitmapImage Picture { get; set; }
        public string WonGames { get; set; }
        public string LostGames { get; set; }

        public User() { }

        public User(string name, BitmapImage picture, string wonGames, string lostGames)
        {
            this.Name = name;
            this.Picture = picture;
            this.WonGames = wonGames;
            this.LostGames = lostGames;
        }
    }
}
