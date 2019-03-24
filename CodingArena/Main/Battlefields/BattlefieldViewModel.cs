using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Bullets;
using CodingArena.Main.Battlefields.Resources;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CodingArena.Main.Battlefields.Homes;

namespace CodingArena.Main.Battlefields
{
    public class BattlefieldViewModel : Observable
    {
        private Battlefield myBattlefield;

        public BattlefieldViewModel()
        {
            Width = 1600;
            Height = 900;
            Bases = new ObservableCollection<HomeViewModel>();
            Bots = new ObservableCollection<BotViewModel>();
            Bullets = new ObservableCollection<BulletViewModel>();
            Resources = new ObservableCollection<ResourceViewModel>();
        }

        public double Width { get; set; }
        public double Height { get; set; }

        public ObservableCollection<HomeViewModel> Bases { get; set; }
        public ObservableCollection<BotViewModel> Bots { get; set; }
        public ObservableCollection<BulletViewModel> Bullets { get; set; }
        public ObservableCollection<ResourceViewModel> Resources { get; set; }

        public void Set(Battlefield battlefield, List<Bot> bots)
        {
            Bots.Clear();
            Bullets.Clear();
            Resources.Clear();

            if (myBattlefield != null)
            {
                myBattlefield.BotAdded -= OnBotAdded;
                myBattlefield.BotRemoved -= OnBotRemoved;
                myBattlefield.BulletAdded -= OnBulletAdded;
                myBattlefield.BulletRemoved -= OnBulletRemoved;
                myBattlefield.ResourceAdded -= OnResourceAdded;
                myBattlefield.ResourceRemoved -= OnResourceRemoved;
            }

            foreach (var bot in bots)
            {
                Bots.Add(new BotViewModel(bot));
            }

            foreach (var resource in battlefield.Resources)
            {
                Resources.Add(new ResourceViewModel(resource));
            }

            myBattlefield = battlefield;
            myBattlefield.BotAdded += OnBotAdded;
            myBattlefield.BotRemoved += OnBotRemoved;
            myBattlefield.BulletAdded += OnBulletAdded;
            myBattlefield.BulletRemoved += OnBulletRemoved;
            myBattlefield.ResourceAdded += OnResourceAdded;
            myBattlefield.ResourceRemoved += OnResourceRemoved;
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
    }
}