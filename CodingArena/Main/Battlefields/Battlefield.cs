using CodingArena.Main.Battlefields.Bases;
using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Bullets;
using CodingArena.Main.Battlefields.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace CodingArena.Main.Battlefields
{
    public sealed class Battlefield
    {
        public Battlefield()
        {
            Width = double.Parse(ConfigurationManager.AppSettings["BattlefieldWidth"]);
            Height = double.Parse(ConfigurationManager.AppSettings["BattlefieldHeight"]);
            Bases = new List<Base>();
            Bots = new List<DeathMatchBot>();
            Bullets = new List<Bullet>();
            Resources = new List<Resource>();
        }

        public double Width { get; }
        public double Height { get; }
        public List<Base> Bases { get; }
        public List<DeathMatchBot> Bots { get; }
        public List<Bullet> Bullets { get; }
        public List<Resource> Resources { get; }

        public void RemoveBot(DeathMatchBot bot)
        {
            Bots.Remove(bot);
            OnBotRemoved(bot);
        }

        public void RemoveBullet(Bullet bullet)
        {
            Bullets.Remove(bullet);
            OnBulletRemoved(bullet);
        }

        public void RemoveResource(Resource resource)
        {
            Resources.Remove(resource);
            OnResourceRemoved(resource);
        }

        public event EventHandler<BotEventArgs> BotAdded;
        public event EventHandler<BotEventArgs> BotRemoved;
        public event EventHandler<BulletEventArgs> BulletAdded;
        public event EventHandler<BulletEventArgs> BulletRemoved;
        public event EventHandler<ResourceEventArgs> ResourceAdded;
        public event EventHandler<ResourceEventArgs> ResourceRemoved;

        private void OnBotAdded(DeathMatchBot bot) => BotAdded?.Invoke(this, new BotEventArgs(bot));
        private void OnBotRemoved(DeathMatchBot bot) => BotRemoved?.Invoke(this, new BotEventArgs(bot));
        private void OnBulletAdded(Bullet bullet) => BulletAdded?.Invoke(this, new BulletEventArgs(bullet));
        private void OnBulletRemoved(Bullet bullet) => BulletRemoved?.Invoke(this, new BulletEventArgs(bullet));
        private void OnResourceAdded(Resource resource) => ResourceAdded?.Invoke(this, new ResourceEventArgs(resource));
        private void OnResourceRemoved(Resource resource) => ResourceRemoved?.Invoke(this, new ResourceEventArgs(resource));
    }
}