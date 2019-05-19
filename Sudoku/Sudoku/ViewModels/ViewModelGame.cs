using Sudoku.CommandsForGame;
using Sudoku.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Sudoku
{
    public class ViewModelGame : ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<ObservableCollection<Cell>> elements;

        private User user;

        public FinishCommand FinishCommand { get; }
        public StandardCommand StandardCommand { get; }
        public CustomCommand CustomCommand { get; }
        public AboutCommand AboutCommand { get; }
        public NewGameCommand NewGameCommand { get; }
        public SaveGameCommand SaveGameCommand { get; }
        public StatisticsCommand StatisticsCommand { get; }
        public OpenGameCommand OpenGameCommand { get; }
        public BackCommand BackCommand { get; }

        public ObservableCollection<ObservableCollection<Cell>> Elements
        {
            get => elements;
            set
            {
                elements = value;
                OnPropertyChanged();
            }
        }

        public User User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }

        public ViewModelGame(User user)
        {
            Elements = new ObservableCollection<ObservableCollection<Cell>>();

            User = user;

            FinishCommand = new FinishCommand(Elements);
            StandardCommand = new StandardCommand(Elements);
            CustomCommand = new CustomCommand(Elements);
            NewGameCommand = new NewGameCommand(Elements);
            SaveGameCommand = new SaveGameCommand(Elements);
            OpenGameCommand = new OpenGameCommand(Elements);
            AboutCommand = new AboutCommand();
            BackCommand = new BackCommand();
            StatisticsCommand = new StatisticsCommand();


            TimeRemaining = delay.ToString();
            StartTimerCommand = new RelayCommand(StartTimer);
            StopTimerCommand = new RelayCommand(StopTimer);
        }


        private string timeRemaining;
        private DispatcherTimer dispatcherTimer;
        private int delay = 60;
        private DateTime deadline;
        private int time;

        public string TimeRemaining
        {
            get => timeRemaining;
            set
            {
                timeRemaining = value;
                RaisePropertyChanged("TimeRemaining");
            }
        }

        public ICommand StartTimerCommand { get; set; }
        public ICommand StopTimerCommand { get; set; }


#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public void StartTimer()
        {
            dispatcherTimer = new DispatcherTimer();
            deadline = DateTime.Now.AddSeconds(delay);
            dispatcherTimer.Tick += new EventHandler(TimerTick);
            dispatcherTimer.Start();
        }

        public void StopTimer()
        {
            if (dispatcherTimer != null)
            {
                dispatcherTimer.Stop();
                delay = (deadline - DateTime.Now).Seconds;
                deadline = DateTime.Now.AddSeconds(delay);
            }

        }

        private void TimerTick(object send, EventArgs e)
        {
            time = Int32.Parse((deadline - DateTime.Now).Minutes.ToString()) * 60 + Int32.Parse((deadline - DateTime.Now).Seconds.ToString());
            TimeRemaining = (time / 60).ToString() + ":" + (time % 60).ToString();
            if (time == 0)
            {
                dispatcherTimer.Stop();
                dispatcherTimer.IsEnabled = false;

                MessageBox.Show("Time has expired! You lose! :(", "Info");
                Elements.Clear();
                int value = Int32.Parse(User.LostGames);
                int lost = value + 1;
                User.LostGames = lost.ToString();

                delay = 600;
                time = delay;
                TimeRemaining = (time / 60).ToString() + ":" + (time % 60).ToString();
            }
        }
    }
}
