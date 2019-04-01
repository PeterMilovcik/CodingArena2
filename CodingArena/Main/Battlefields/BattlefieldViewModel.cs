using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Bullets;
using CodingArena.Main.Battlefields.FirstAidKits;
using CodingArena.Main.Battlefields.Homes;
using CodingArena.Main.Battlefields.Hospitals;
using CodingArena.Main.Battlefields.Resources;
using CodingArena.Main.Battlefields.Stats;
using CodingArena.Main.Battlefields.Weapons;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CodingArena.Main.Battlefields
{
    public class BattlefieldViewModel : Observable
    {
        private Battlefield myBattlefield;
        private ObservableCollection<BotStatsViewModel> myStats;
        private double myWidth;
        private double myHeight;

        public BattlefieldViewModel()
        {
            Width = 1600;
            Height = 900;
            Homes = new ObservableCollection<HomeViewModel>();
            Bots = new ObservableCollection<BotViewModel>();
            Bullets = new ObservableCollection<BulletViewModel>();
            Resources = new ObservableCollection<ResourceViewModel>();
            Weapons = new ObservableCollection<WeaponViewModel>();
            Stats = new ObservableCollection<BotStatsViewModel>();
            Hospitals = new ObservableCollection<HospitalViewModel>();
            FirstAidKits = new ObservableCollection<FirstAidKitViewModel>();
        }

        public double Width
        {
            get => myWidth;
            set
            {
                if (value.Equals(myWidth)) return;
                myWidth = value;
                OnPropertyChanged();
            }
        }

        public double Height
        {
            get => myHeight;
            set
            {
                if (value.Equals(myHeight)) return;
                myHeight = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<HomeViewModel> Homes { get; set; }
        public ObservableCollection<BotViewModel> Bots { get; set; }
        public ObservableCollection<BulletViewModel> Bullets { get; set; }
        public ObservableCollection<ResourceViewModel> Resources { get; set; }
        public ObservableCollection<WeaponViewModel> Weapons { get; set; }
        public ObservableCollection<HospitalViewModel> Hospitals { get; set; }
        public ObservableCollection<FirstAidKitViewModel> FirstAidKits { get; set; }

        public ObservableCollection<BotStatsViewModel> Stats
        {
            get => myStats;
            set
            {
                if (Equals(value, myStats)) return;
                myStats = value;
                OnPropertyChanged();
            }
        }

        public void Set(Battlefield battlefield)
        {
            Width = battlefield.Width;
            Height = battlefield.Height;
            Bots.Clear();
            Bullets.Clear();
            Resources.Clear();
            Homes.Clear();
            Weapons.Clear();
            Hospitals.Clear();
            FirstAidKits.Clear();
            Stats.Clear();

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
                myBattlefield.WeaponAdded -= OnWeaponAdded;
                myBattlefield.WeaponRemoved -= OnWeaponRemoved;
                myBattlefield.HospitalAdded -= OnHospitalAdded;
                myBattlefield.HospitalRemoved -= OnHospitalRemoved;
                myBattlefield.FirstAidKitAdded -= OnFirstAidKitAdded;
                myBattlefield.FirstAidKitRemoved -= OnFirstAidKitRemoved;
            }

            foreach (var bot in battlefield.Bots.OfType<Bot>())
            {
                Bots.Add(new BotViewModel(bot));
                Stats.Add(new BotStatsViewModel(bot));
                bot.Changed += UpdateStats;
            }

            foreach (var resource in battlefield.Resources)
            {
                Resources.Add(new ResourceViewModel(resource));
            }

            foreach (var weapon in battlefield.Weapons.OfType<Weapon>())
            {
                Weapons.Add(new WeaponViewModel(weapon));
            }

            foreach (var home in battlefield.Homes.OfType<Home>())
            {
                Homes.Add(new HomeViewModel(home));
            }

            foreach (var hospital in battlefield.Hospitals.OfType<Hospital>())
            {
                Hospitals.Add(new HospitalViewModel(hospital));
            }

            foreach (var firstAidKit in battlefield.FirstAidKits.OfType<FirstAidKit>())
            {
                FirstAidKits.Add(new FirstAidKitViewModel(firstAidKit));
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
            myBattlefield.WeaponAdded += OnWeaponAdded;
            myBattlefield.WeaponRemoved += OnWeaponRemoved;
            myBattlefield.HospitalAdded += OnHospitalAdded;
            myBattlefield.HospitalRemoved += OnHospitalRemoved;
            myBattlefield.FirstAidKitAdded += OnFirstAidKitAdded;
            myBattlefield.FirstAidKitRemoved += OnFirstAidKitRemoved;
        }

        private void UpdateStats(object sender, EventArgs e) =>
            Stats = new ObservableCollection<BotStatsViewModel>(
                Stats.OrderByDescending(b => b.ResourceCount));

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

        private void OnWeaponAdded(object sender, WeaponEventArgs e) =>
            Weapons.Add(new WeaponViewModel(e.Weapon));

        private void OnWeaponRemoved(object sender, WeaponEventArgs e)
        {
            var viewModel = Weapons.FirstOrDefault(r => r.Weapon == e.Weapon);
            if (viewModel != null)
            {
                Weapons.Remove(viewModel);
            }
        }

        private void OnHospitalAdded(object sender, HospitalEventArgs e) =>
            Hospitals.Add(new HospitalViewModel(e.Hospital));

        private void OnHospitalRemoved(object sender, HospitalEventArgs e)
        {
            var viewModel = Hospitals.FirstOrDefault(r => r.Hospital == e.Hospital);
            if (viewModel != null)
            {
                Hospitals.Remove(viewModel);
            }
        }

        private void OnFirstAidKitAdded(object sender, FirstAidKitEventArgs e) =>
            FirstAidKits.Add(new FirstAidKitViewModel(e.FirstAidKit));

        private void OnFirstAidKitRemoved(object sender, FirstAidKitEventArgs e)
        {
            var viewModel = FirstAidKits.FirstOrDefault(r => r.FirstAidKit == e.FirstAidKit);
            if (viewModel != null)
            {
                FirstAidKits.Remove(viewModel);
            }
        }
    }
}