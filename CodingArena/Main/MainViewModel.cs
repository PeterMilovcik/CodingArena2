using CodingArena.Main.Battlefields;

namespace CodingArena.Main
{
    public class MainViewModel : Observable
    {
        private string myStatus;

        public MainViewModel()
        {
            Status = "Ready";
            Battlefield = new BattlefieldViewModel();
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

        public BattlefieldViewModel Battlefield { get; }
    }
}