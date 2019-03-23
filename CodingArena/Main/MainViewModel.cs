using CodingArena.Main.Battlefields;
using CodingArena.Main.Rounds;
using System.Threading.Tasks;

namespace CodingArena.Main
{
    public class MainViewModel : Observable
    {
        private string myStatus;

        public MainViewModel()
        {
            Status = "Ready";
            Battlefield = new BattlefieldViewModel();
            StartGameCommand = new DelegateCommand(async () => await StartGameAsync());
        }

        public DelegateCommand StartGameCommand { get; }

        public BattlefieldViewModel Battlefield { get; }

        public async Task StartGameAsync()
        {
            var round = new Round();
            Battlefield.Set(round.Battlefield, round.Bots);
            await round.StartAsync();
        }


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