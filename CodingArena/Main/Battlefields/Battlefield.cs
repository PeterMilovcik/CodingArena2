using CodingArena.AI;
using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Bullets;
using CodingArena.Main.Battlefields.Explosions;
using CodingArena.Main.Battlefields.FirstAidKits;
using CodingArena.Main.Battlefields.Homes;
using CodingArena.Main.Battlefields.Hospitals;
using CodingArena.Main.Battlefields.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using IWeapon = CodingArena.AI.IWeapon;

namespace CodingArena.Main.Battlefields
{
    public sealed class Battlefield : IBattlefield
    {
        private readonly List<IHome> myHomes;
        private readonly List<IBot> myBots;
        private readonly List<IBullet> myBullets;
        private readonly List<IResource> myResources;
        private readonly List<Weapon> myWeapons;
        private readonly List<Hospital> myHospitals;
        private readonly List<FirstAidKit> myFirstAidKits;
        private readonly List<Explosion> myExplosions;

        public Battlefield(double width, double height)
        {
            Width = width;
            Height = height;
            myHomes = new List<IHome>();
            myBots = new List<IBot>();
            myBullets = new List<IBullet>();
            myResources = new List<IResource>();
            myWeapons = new List<Weapon>();
            myHospitals = new List<Hospital>();
            myFirstAidKits = new List<FirstAidKit>();
            myExplosions = new List<Explosion>();
        }

        public double Width { get; }
        public double Height { get; }
        public IReadOnlyList<IHome> Homes => myHomes;
        public IReadOnlyList<IBot> Bots => myBots;
        public IReadOnlyList<IBullet> Bullets => myBullets;
        public IReadOnlyList<IResource> Resources => myResources;
        public IReadOnlyList<IWeapon> Weapons => myWeapons.OfType<IWeapon>().ToList();
        public IReadOnlyList<IHospital> Hospitals => myHospitals.OfType<IHospital>().ToList();
        public IReadOnlyList<IFirstAidKit> FirstAidKits => myFirstAidKits.OfType<IFirstAidKit>().ToList();
        public IReadOnlyList<Explosion> Explosions => myExplosions.ToList();

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

        public void Add(Hospital hospital)
        {
            myHospitals.Add(hospital);
            OnHospitalAdded(hospital);
        }

        public void Remove(Hospital hospital)
        {
            myHospitals.Remove(hospital);
            OnHospitalRemoved(hospital);
        }

        public void Add(FirstAidKit firstAidKit)
        {
            myFirstAidKits.Add(firstAidKit);
            OnFirstAidKitAdded(firstAidKit);
        }

        public void Remove(FirstAidKit firstAidKit)
        {
            myFirstAidKits.Remove(firstAidKit);
            OnFirstAidKitRemoved(firstAidKit);
        }

        public void Add(Explosion explosion)
        {
            myExplosions.Add(explosion);
            OnExplosionAdded(explosion);
        }

        public void Remove(Explosion explosion)
        {
            myExplosions.Remove(explosion);
            OnExplosionRemoved(explosion);
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

        public event EventHandler<HospitalEventArgs> HospitalAdded;

        public event EventHandler<HospitalEventArgs> HospitalRemoved;

        public event EventHandler<FirstAidKitEventArgs> FirstAidKitAdded;

        public event EventHandler<FirstAidKitEventArgs> FirstAidKitRemoved;

        public event EventHandler<ExplosionEventArgs> ExplosionAdded;

        public event EventHandler<ExplosionEventArgs> ExplosionRemoved;

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

        private void OnHospitalAdded(Hospital hospital) =>
            HospitalAdded?.Invoke(this, new HospitalEventArgs(hospital));

        private void OnHospitalRemoved(Hospital hospital) =>
            HospitalRemoved?.Invoke(this, new HospitalEventArgs(hospital));

        private void OnFirstAidKitAdded(FirstAidKit firstAidKit) =>
            FirstAidKitAdded?.Invoke(this, new FirstAidKitEventArgs(firstAidKit));

        private void OnFirstAidKitRemoved(FirstAidKit firstAidKit) =>
            FirstAidKitRemoved?.Invoke(this, new FirstAidKitEventArgs(firstAidKit));

        private void OnExplosionAdded(Explosion explosion) =>
            ExplosionAdded?.Invoke(this, new ExplosionEventArgs(explosion));

        private void OnExplosionRemoved(Explosion explosion) =>
            ExplosionRemoved?.Invoke(this, new ExplosionEventArgs(explosion));
    }
}