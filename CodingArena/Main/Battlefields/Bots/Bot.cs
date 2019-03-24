using CodingArena.Annotations;
using CodingArena.Common;
using CodingArena.Main.Battlefields.Resources;
using CodingArena.Main.Battlefields.Weapons;
using CodingArena.Player;
using CodingArena.Player.TurnActions;
using System;
using System.Configuration;
using System.Diagnostics;
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
            Speed = double.Parse(ConfigurationManager.AppSettings["BotSpeed"]);
            myWeapon = new Pistol(myBattlefield);
            Angle = 90;
            myRemainingAimTime = TimeSpan.Zero;
        }

        public IBotAI BotAI { get; }

        public string Name { get; }

        public IValue HitPoints { get; private set; }
        public Resource Resource { get; private set; }
        public bool HasResource => Resource != null;
        public bool IsAiming => myRemainingAimTime > TimeSpan.Zero;
        public Player.IWeapon Weapon => myWeapon;

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

            if (Battlefield.Bots.Except(new[] { this })
                .Any(bot => bot.IsInCollisionWith(afterMove)))
            {
                return false;
            }

            Position = new Point(afterMove.Position.X, afterMove.Position.Y);
            OnChanged();
            return true;
        }

        public void TakeDamageFrom(IBullet bullet)
        {
            var newActual = HitPoints.Actual - bullet.Damage;
            newActual = Math.Max(newActual, 0);
            HitPoints = new Value(HitPoints.Maximum, newActual);
            Debug.WriteLine($"{Name} takes {bullet.Damage} damage. Remaining HP: {HitPoints.Actual}");
            OnChanged();
            if (HitPoints.Actual <= 0)
            {
                Die(bullet.Shooter);
            }
        }

        private void Die(IBot shooter)
        {
            HitPoints = new Value(HitPoints.Maximum, 0);
            Battlefield.Remove(this);
            OnDied();
        }

        private void Shoot()
        {
            myRemainingAimTime = myWeapon.AimTime;
        }

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

        public override async Task UpdateAsync()
        {
            await base.UpdateAsync();
            try
            {
                if (Weapon.IsReloading)
                {
                    myWeapon.Reload();
                }

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
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void Aim()
        {
            myRemainingAimTime -= DeltaTime;
            if (myRemainingAimTime <= TimeSpan.Zero)
            {
                var bullet = myWeapon.Fire(this);
                Battlefield.Add(bullet);
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

        private void Execute(ShootTurnAction shoot)
        {
            if (IsAiming) return;
            if (Weapon.IsReloading) return;
            Shoot();
        }

        public void SetPositionTo(Point position) => Position = position;
    }
}