﻿using CodingArena.Annotations;
using CodingArena.Common;
using CodingArena.Main.Battlefields.Homes;
using CodingArena.Main.Battlefields.Resources;
using CodingArena.Main.Battlefields.Weapons;
using CodingArena.Player;
using CodingArena.Player.TurnActions;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using IWeapon = CodingArena.Main.Battlefields.Weapons.IWeapon;

namespace CodingArena.Main.Battlefields.Bots
{
    public sealed class Bot : Movable, IBot
    {
        private const double MinDistance = 0.1;
        private readonly Battlefield myBattlefield;
        private readonly IWeapon myWeapon;
        private TimeSpan myRemainingAimTime;

        public Bot([NotNull] Battlefield battlefield, IBotAI botAI) : base(battlefield)
        {
            BotAI = botAI;
            Name = BotAI.BotName;
            myBattlefield = battlefield ?? throw new ArgumentNullException(nameof(battlefield));
            Radius = 20;
            var maxHitPoints = double.Parse(ConfigurationManager.AppSettings["MaxHitPoints"]);
            HitPoints = new Value(maxHitPoints, maxHitPoints);
            MaxSpeed = double.Parse(ConfigurationManager.AppSettings["MaxBotSpeed"]);
            MinSpeed = double.Parse(ConfigurationManager.AppSettings["MinBotSpeed"]);
            Speed = MaxSpeed;
            myWeapon = new Pistol(myBattlefield);
            Angle = 90;
            myRemainingAimTime = TimeSpan.Zero;
        }

        public IBotAI BotAI { get; }
        public string Name { get; }
        public IValue HitPoints { get; private set; }
        public IResource Resource { get; private set; }
        public bool HasResource => Resource != null;
        public bool IsAiming => myRemainingAimTime > TimeSpan.Zero;
        public Player.IWeapon Weapon => myWeapon;

        public override async Task UpdateAsync()
        {
            await base.UpdateAsync();
            await myWeapon.UpdateAsync();
            try
            {
                if (IsAiming)
                {
                    Aim();
                    return;
                }
                var turnAction = BotAI.Update(this, myBattlefield);
                switch (turnAction)
                {
                    case ShootTurnAction shoot:
                        Execute(shoot);
                        break;
                    case MoveTowardsTurnAction moveTowards:
                        Execute(moveTowards);
                        break;
                    case MoveAwayFromTurnAction moveAway:
                        Execute(moveAway);
                        break;
                    case PickUpResourceTurnAction pickUpResource:
                        Execute(pickUpResource);
                        break;
                    case DropDownResourceTurnAction dropDownResource:
                        Execute(dropDownResource);
                        break;
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void Execute(PickUpResourceTurnAction pickUpResource)
        {
            var resource = Battlefield.Resources.OrderBy(DistanceTo).FirstOrDefault();
            if (resource != null)
            {
                if (DistanceTo(resource) < Radius)
                {
                    PickUpResource(resource);
                }
            }
        }

        private void Execute(DropDownResourceTurnAction dropDownResource)
        {
            if (Resource == null) return;
            var home = Battlefield.Homes.OfType<Home>().Single(h => h.Name == Name);
            if (DistanceTo(home) < Radius)
            {
                Speed = MaxSpeed;
                OnResourceDropped(Resource);
                home.IncreaseCount();
                Resource = null;
            }
            else
            {
                DropDownResource();
            }
        }

        private void Execute(MoveTowardsTurnAction moveTowards)
        {
            if (DistanceTo(moveTowards.Position) < MinDistance)
            {
                return;
            }
            var movement = new Vector(moveTowards.Position.X - Position.X, moveTowards.Position.Y - Position.Y);
            movement.Normalize();
            Direction = movement;
            Move();
        }

        private void Execute(MoveAwayFromTurnAction moveAway)
        {
            var movement = new Vector(Position.X - moveAway.Position.X, Position.Y - moveAway.Position.Y);
            movement.Normalize();
            Direction = movement;
            Move();
        }

        private void Execute(ShootTurnAction shoot)
        {
            if (IsAiming) return;
            if (Weapon.IsReloading) return;
            Shoot();
        }

        private bool Move()
        {
            var movement = new Vector(Direction.X, Direction.Y);
            movement.X *= Speed * DeltaTime.TotalSeconds;
            movement.Y *= Speed * DeltaTime.TotalSeconds;

            var afterMove = new Bot(Battlefield, BotAI)
            {
                Position = new Point(Position.X + movement.X, Position.Y + movement.Y),
                Radius = Radius
            };

            if (afterMove.Position.X > Battlefield.Width - 1 ||
                afterMove.Position.X < 0 ||
                afterMove.Position.Y > Battlefield.Height - 1 ||
                afterMove.Position.Y < 0)
            {
                Die();
                return true;
            }

            var otherBots = Battlefield.Bots.Except(new[] { this });
            foreach (var bot in otherBots)
            {
                if (bot.IsInCollisionWith(afterMove))
                {
                    OnCollisionWith(bot);

                    return false;
                }
            }


            Position = new Point(afterMove.Position.X, afterMove.Position.Y);
            OnChanged();

            OnMoved(movement);

            return true;
        }

        private void OnCollisionWith(IBot bot)
        {
            try
            {
                BotAI.OnCollisionWith(bot);
            }
            catch
            {
                // ignored
            }
        }

        private void OnMoved(Vector movement)
        {
            try
            {
                BotAI.OnMoved(movement.Length);
            }
            catch
            {
                // ignored
            }
        }

        public void TakeDamageFrom(IBullet bullet)
        {
            var newActual = HitPoints.Actual - bullet.Damage;
            newActual = Math.Max(newActual, 0);
            HitPoints = new Value(HitPoints.Maximum, newActual);

            OnDamaged(bullet);

            OnChanged();
            if (HitPoints.Actual <= 0)
            {
                Die(bullet.Shooter);
            }
        }

        private void OnDamaged(IBullet bullet)
        {
            try
            {
                BotAI.OnDamaged(bullet.Damage, bullet.Shooter);
            }
            catch
            {
                // ignored
            }
        }

        private void Die()
        {
            HitPoints = new Value(HitPoints.Maximum, 0);
            Battlefield.Remove(this);
            OnDied();
        }

        private void Die(IBot shooter)
        {
            Die();
        }

        private void Shoot()
        {
            myRemainingAimTime = myWeapon.AimTime;
        }

        public void PickUpResource(IResource resource)
        {
            if (HasResource) return;
            Resource = resource;
            Battlefield.Remove(resource);
            Speed = MinSpeed;
            OnResourcePicked();
            OnResourcePicked(Resource);
        }

        private void OnResourcePicked()
        {
            try
            {
                BotAI.OnResourcePicked();
            }
            catch
            {
                // ignore
            }
        }

        public void DropDownResource()
        {
            Speed = MaxSpeed;
            OnResourceDropped(Resource);
            Battlefield.Add(new Resource(new Point(Position.X, Position.Y)));
            Resource = null;
        }

        public event EventHandler<ResourceEventArgs> ResourcePicked;

        private void OnResourcePicked(IResource resource) =>
            ResourcePicked?.Invoke(this, new ResourceEventArgs(resource));

        public event EventHandler<ResourceEventArgs> ResourceDropped;

        private void OnResourceDropped(IResource resource) =>
            ResourceDropped?.Invoke(this, new ResourceEventArgs(resource));

        public event EventHandler Died;

        private void OnDied() => Died?.Invoke(this, EventArgs.Empty);

        private void Aim()
        {
            myRemainingAimTime -= DeltaTime;
            if (myRemainingAimTime <= TimeSpan.Zero)
            {
                var bullet = myWeapon.Fire(this);
                Battlefield.Add(bullet);
            }
        }

        public void SetPositionTo(Point position) => Position = position;
    }
}