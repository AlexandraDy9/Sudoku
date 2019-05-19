using System.Windows;

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for SudokuWindow.xaml
    /// </summary>
    public partial class SudokuWindow : Window
    {
        public SudokuWindow(User user)
        {
            InitializeComponent();
            DataContext = new ViewModelGame(user);
        }

        private void CustomButton(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = Visibility.Visible;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = Visibility.Collapsed;
        }
    }
}
