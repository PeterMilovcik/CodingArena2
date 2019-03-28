using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Bullets;
using CodingArena.Main.Battlefields.Homes;
using CodingArena.Main.Battlefields.Weapons;
using CodingArena.Player;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using IWeapon = CodingArena.Player.IWeapon;

namespace CodingArena.Main.Battlefields
{
    public sealed class Battlefield : IBattlefield
    {
        private readonly List<IHome> myHomes;
        private readonly List<IBot> myBots;
        private readonly List<IBullet> myBullets;
        private readonly List<IResource> myResources;
        private readonly List<Weapon> myWeapons;

        public Battlefield()
        {
            Width = double.Parse(ConfigurationManager.AppSettings["BattlefieldWidth"]);
            Height = double.Parse(ConfigurationManager.AppSettings["BattlefieldHeight"]);
            myHomes = new List<IHome>();
            myBots = new List<IBot>();
            myBullets = new List<IBullet>();
            myResources = new List<IResource>();
            myWeapons = new List<Weapon>();
        }

        public double Width { get; }
        public double Height { get; }
        public IReadOnlyList<IHome> Homes => myHomes;
        public IReadOnlyList<IBot> Bots => myBots;
        public IReadOnlyList<IBullet> Bullets => myBullets;
        public IReadOnlyList<IResource> Resources => myResources;
        public IReadOnlyList<IWeapon> Weapons => myWeapons.OfType<IWeapon>().ToList();

        public void Add(Bot bot)
        {
            myBots.Add(bot);
            OnBotAdded(bot);
        }

        public void Remove(Bot bot)
        {
            myBots.Remove(bot);
            OnBotRemoved(bot);
        }

        public void Add(Bullet bullet)
        {
            myBullets.Add(bullet);
            OnBulletAdded(bullet);
        }

        public void Remove(Bullet bullet)
        {
            myBullets.Remove(bullet);
            OnBulletRemoved(bullet);
        }

        public void Add(IResource resource)
        {
            myResources.Add(resource);
            OnResourceAdded(resource);
        }

        public void Remove(IResource resource)
        {
            myResources.Remove(resource);
            OnResourceRemoved(resource);
        }

        public void Add(Home home)
        {
            myHomes.Add(home);
            OnHomeAdded(home);
        }

        public void Remove(Home home)
        {
            myHomes.Remove(home);
            OnHomeRemoved(home);
        }

        public void Add(Weapon weapon)
        {
            myWeapons.Add(weapon);
            OnWeaponAdded(weapon);
        }

        public void Remove(Weapon weapon)
        {
            myWeapons.Remove(weapon);
            OnWeaponRemoved(weapon);
        }

        public event EventHandler<BotEventArgs> BotAdded;

        public event EventHandler<BotEventArgs> BotRemoved;

        public event EventHandler<BulletEventArgs> BulletAdded;

        public event EventHandler<BulletEventArgs> BulletRemoved;

        public event EventHandler<ResourceEventArgs> ResourceAdded;

        public event EventHandler<ResourceEventArgs> ResourceRemoved;

        public event EventHandler<HomeEventArgs> HomeAdded;

        public event EventHandler<HomeEventArgs> HomeRemoved;

        public event EventHandler<WeaponEventArgs> WeaponAdded;

        public event EventHandler<WeaponEventArgs> WeaponRemoved;

        private void OnBotAdded(Bot bot) =>
            BotAdded?.Invoke(this, new BotEventArgs(bot));

        private void OnBotRemoved(Bot bot) =>
            BotRemoved?.Invoke(this, new BotEventArgs(bot));

        private void OnBulletAdded(Bullet bullet) =>
            BulletAdded?.Invoke(this, new BulletEventArgs(bullet));

        private void OnBulletRemoved(Bullet bullet) =>
            BulletRemoved?.Invoke(this, new BulletEventArgs(bullet));

        private void OnResourceAdded(IResource resource) =>
            ResourceAdded?.Invoke(this, new ResourceEventArgs(resource));

        private void OnResourceRemoved(IResource resource) =>
            ResourceRemoved?.Invoke(this, new ResourceEventArgs(resource));

        private void OnHomeAdded(Home home) =>
            HomeAdded?.Invoke(this, new HomeEventArgs(home));

        private void OnHomeRemoved(Home home) =>
            HomeRemoved?.Invoke(this, new HomeEventArgs(home));

        private void OnWeaponAdded(Weapon weapon) =>
            WeaponAdded?.Invoke(this, new WeaponEventArgs(weapon));

        private void OnWeaponRemoved(Weapon weapon) =>
            WeaponRemoved?.Invoke(this, new WeaponEventArgs(weapon));
    }
}