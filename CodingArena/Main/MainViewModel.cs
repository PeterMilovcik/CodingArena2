using CodingArena.Main.Battlefields;
using CodingArena.Main.Rounds;
using System.Threading.Tasks;
using System.Windows;

namespace CodingArena.Main
{
    public class MainViewModel : Observable
    {
        private string myStatus;
        private bool myIsGameRunning;
        private Round myRound;

        public MainViewModel()
        {
            Status = "Ready";
            Battlefield = new BattlefieldViewModel();
            StartGameCommand = new DelegateCommand(() => !IsGameRunning, async () => await StartGameAsync());
            ExitCommand = new DelegateCommand(Exit);
        }


        public DelegateCommand StartGameCommand { get; }

        public DelegateCommand ExitCommand { get; }

        public BattlefieldViewModel Battlefield { get; }

        public async Task StartGameAsync()
        {
            if (IsGameRunning) return;

            IsGameRunning = true;
            Round = new Round();
            Battlefield.Set(Round.Battlefield);
            await Round.StartAsync();
            IsGameRunning = false;
        }

        public Round Round
        {
            get => myRound;
            private set
            {
                if (Equals(value, myRound)) return;
                myRound = value;
                OnPropertyChanged();
            }
        }

        public bool IsGameRunning
        {
            get => myIsGameRunning;
            set
            {
                if (value == myIsGameRunning) return;
                myIsGameRunning = value;
                OnPropertyChanged();
            }
        }

        private void Exit() => Application.Current.MainWindow?.Close();

        public string Status
        {
            get => myStatus;
            set
            {
                if (value == myStatus) return;
                myStatus = value;
                OnPropertyChanged();
            }
        }
    }
}