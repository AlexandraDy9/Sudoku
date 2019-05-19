using System.Windows;

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewUser(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = Visibility.Visible;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = Visibility.Collapsed;
        }

        private void PlayButton(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
