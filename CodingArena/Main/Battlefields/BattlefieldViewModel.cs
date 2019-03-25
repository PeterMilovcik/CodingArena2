using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Bullets;
using CodingArena.Main.Battlefields.Homes;
using CodingArena.Main.Battlefields.Resources;
using System.Collections.ObjectModel;
using System.Linq;

namespace CodingArena.Main.Battlefields
{
    public class BattlefieldViewModel : Observable
    {
        private Battlefield myBattlefield;

        public BattlefieldViewModel()
        {
            Width = 1600;
            Height = 900;
            Homes = new ObservableCollection<HomeViewModel>();
            Bots = new ObservableCollection<BotViewModel>();
            Bullets = new ObservableCollection<BulletViewModel>();
            Resources = new ObservableCollection<ResourceViewModel>();
        }

        public double Width { get; set; }
        public double Height { get; set; }

        public ObservableCollection<HomeViewModel> Homes { get; set; }
        public ObservableCollection<BotViewModel> Bots { get; set; }
        public ObservableCollection<BulletViewModel> Bullets { get; set; }
        public ObservableCollection<ResourceViewModel> Resources { get; set; }

        public void Set(Battlefield battlefield)
        {
            Bots.Clear();
            Bullets.Clear();
            Resources.Clear();
            Homes.Clear();

            if (myBattlefield != null)
            {
                myBattlefield.BotAdded -= OnBotAdded;
                myBattlefield.BotRemoved -= OnBotRemoved;
                myBattlefield.BulletAdded -= OnBulletAdded;
                myBattlefield.BulletRemoved -= OnBulletRemoved;
                myBattlefield.ResourceAdded -= OnResourceAdded;
                myBattlefield.ResourceRemoved -= OnResourceRemoved;
                myBattlefield.HomeAdded -= OnHomeAdded;
                myBattlefield.HomeRemoved -= OnHomeRemoved;
            }

            foreach (var bot in battlefield.Bots.OfType<Bot>())
            {
                Bots.Add(new BotViewModel(bot));
            }

            foreach (var resource in battlefield.Resources)
            {
                Resources.Add(new ResourceViewModel(resource));
            }

            foreach (var home in battlefield.Homes.OfType<Home>())
            {
                Homes.Add(new HomeViewModel(home));
            }

            myBattlefield = battlefield;
            myBattlefield.BotAdded += OnBotAdded;
            myBattlefield.BotRemoved += OnBotRemoved;
            myBattlefield.BulletAdded += OnBulletAdded;
            myBattlefield.BulletRemoved += OnBulletRemoved;
            myBattlefield.ResourceAdded += OnResourceAdded;
            myBattlefield.ResourceRemoved += OnResourceRemoved;
            myBattlefield.HomeAdded += OnHomeAdded;
            myBattlefield.HomeRemoved += OnHomeRemoved;
        }

        private void OnBotAdded(object sender, BotEventArgs e) =>
            Bots.Add(new BotViewModel(e.Bot));

        private void OnBotRemoved(object sender, BotEventArgs e)
        {
            var viewModel = Bots.FirstOrDefault(b => b.Bot == e.Bot);
            if (viewModel != null)
            {
                Bots.Remove(viewModel);
            }
        }

        private void OnBulletAdded(object sender, BulletEventArgs e) =>
            Bullets.Add(new BulletViewModel(e.Bullet));

        private void OnBulletRemoved(object sender, BulletEventArgs e)
        {
            var viewModel = Bullets.FirstOrDefault(b => b.Bullet == e.Bullet);
            if (viewModel != null)
            {
                Bullets.Remove(viewModel);
            }
        }

        private void OnResourceAdded(object sender, ResourceEventArgs e) =>
            Resources.Add(new ResourceViewModel(e.Resource));

        private void OnResourceRemoved(object sender, ResourceEventArgs e)
        {
            var viewModel = Resources.FirstOrDefault(r => r.Resource == e.Resource);
            if (viewModel != null)
            {
                Resources.Remove(viewModel);
            }
        }

        private void OnHomeAdded(object sender, HomeEventArgs e) =>
            Homes.Add(new HomeViewModel(e.Home));

        private void OnHomeRemoved(object sender, HomeEventArgs e)
        {
            var viewModel = Homes.FirstOrDefault(r => r.Home == e.Home);
            if (viewModel != null)
            {
                Homes.Remove(viewModel);
            }
        }
    }
}