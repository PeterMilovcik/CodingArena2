using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CodingArena.Main.Battlefields.Bases;
using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Bullets;
using CodingArena.Main.Battlefields.Resources;

namespace CodingArena.Main.Battlefields
{
    public class BattlefieldViewModel : Observable
    {
        public BattlefieldViewModel()
        {
            Width = 1600;
            Height = 900;
            Bases = new ObservableCollection<BaseViewModel>();
            Bots = new ObservableCollection<BotViewModel>();
            Bullets = new ObservableCollection<BulletViewModel>();
            Resources = new ObservableCollection<ResourceViewModel>();
            StartGameCommand = new DelegateCommand(async () => await StartGameAsync());
        }

        public DelegateCommand StartGameCommand { get; }

        public Task StartGameAsync()
        {
            return Task.CompletedTask;
        }

        public double Width { get; set; }
        public double Height { get; set; }

        public ObservableCollection<BaseViewModel> Bases { get; set; }
        public ObservableCollection<BotViewModel> Bots { get; set; }
        public ObservableCollection<BulletViewModel> Bullets { get; set; }
        public ObservableCollection<ResourceViewModel> Resources { get; set; }
    }
}