﻿using CodingArena.Annotations;
using CodingArena.Common;
using CodingArena.Main.Battlefields.Bullets;
using CodingArena.Main.Battlefields.Resources;
using CodingArena.Main.Battlefields.Weapons;
using CodingArena.Player;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using IWeapon = CodingArena.Main.Battlefields.Weapons.IWeapon;

namespace CodingArena.Main.Battlefields.Bots
{
    public sealed class DeathMatchBot : Movable, IBot
    {
        private readonly Battlefield myBattlefield;
        private double myAngle;
        private readonly IWeapon myWeapon;

        public DeathMatchBot([NotNull] Battlefield battlefield, IDeathMatchAI botAI) : base(battlefield)
        {
            BotAI = botAI;
            Name = BotAI.BotName;
            myBattlefield = battlefield ?? throw new ArgumentNullException(nameof(battlefield));
            Radius = 20;
            var maxHitPoints = double.Parse(ConfigurationManager.AppSettings["MaxHitPoints"]);
            HitPoints = new Value(maxHitPoints, maxHitPoints);
            Speed = double.Parse(ConfigurationManager.AppSettings["BotSpeed"]);
            myWeapon = new Pistol(myBattlefield);
        }

        public IDeathMatchAI BotAI { get; }

        public string Name { get; }

        public double Angle
        {
            get => myAngle;
            set
            {
                myAngle = value;
                Direction = new Vector(Math.Cos(Angle), Math.Sin(Angle));
            }
        }

        public IValue HitPoints { get; private set; }
        public Resource Resource { get; private set; }
        public bool HasResource => Resource != null;
        public Player.IWeapon Weapon => myWeapon;

        public override async Task<bool> MoveAsync()
        {
            if (LastUpdate == DateTime.MinValue)
            {
                LastUpdate = DateTime.Now;
            }

            await Task.Delay(1);

            var deltaTime = DateTime.Now - LastUpdate;

            var movement = new Vector(Direction.X, Direction.Y);
            movement.X *= Speed * deltaTime.TotalSeconds;
            movement.Y *= Speed * deltaTime.TotalSeconds;
            var afterMove = new DeathMatchBot(Battlefield, BotAI)
            {
                Position = new Point(Position.X + movement.X, Position.Y + movement.Y),
                Radius = Radius
            };

            if (Battlefield.Bots.Except(new[] { this })
                .Any(bot => bot.IsInCollisionWith(afterMove)))
            {
                LastUpdate = DateTime.Now;
                return false;
            }

            var takeBullets = Battlefield.Bullets.Where(bullet => bullet.IsInCollisionWith(afterMove));
            if (takeBullets.Any())
            {
                foreach (var takeBullet in takeBullets)
                {
                    TakeDamageFrom(takeBullet);
                }
                LastUpdate = DateTime.Now;
                return false;
            }

            Position = new Point(afterMove.Position.X, afterMove.Position.Y);
            LastUpdate = DateTime.Now;
            return true;
        }

        public void TakeDamageFrom(Bullet bullet)
        {
            HitPoints = new Value(HitPoints.Maximum, HitPoints.Actual - bullet.Damage);
            if (HitPoints.Actual <= 0)
            {
                Die(bullet.Shooter);
            }
        }

        private void Die(DeathMatchBot shooter)
        {
            HitPoints = new Value(HitPoints.Maximum, 0);
            Battlefield.RemoveBot(this);
            OnDied();
        }

        public Bullet Shoot() => myWeapon.Fire(this);

        public void PickResource(Resource resource)
        {
            Resource = resource;
            OnResourcePicked(Resource);
        }

        public void DropResource()
        {
            OnResourceDropped(Resource);
            Resource = null;
        }

        public event EventHandler<ResourceEventArgs> ResourcePicked;
        private void OnResourcePicked(Resource resource) =>
            ResourcePicked?.Invoke(this, new ResourceEventArgs(resource));

        public event EventHandler<ResourceEventArgs> ResourceDropped;
        private void OnResourceDropped(Resource resource) =>
            ResourceDropped?.Invoke(this, new ResourceEventArgs(resource));

        public event EventHandler Died;
        private void OnDied() => Died?.Invoke(this, EventArgs.Empty);
    }
}