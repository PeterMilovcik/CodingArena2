using CodingArena.Annotations;
using CodingArena.Common;
using CodingArena.Main.Battlefields.Bullets;
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
        private readonly Battlefield myBattlefield;
        private readonly IWeapon myWeapon;

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
        }

        public IBotAI BotAI { get; }

        public string Name { get; }

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
            var afterMove = new Bot(Battlefield, BotAI)
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

            Position = new Point(afterMove.Position.X, afterMove.Position.Y);
            LastUpdate = DateTime.Now;
            OnChanged();
            return true;
        }

        public void TakeDamageFrom(IBullet bullet)
        {
            HitPoints = new Value(HitPoints.Maximum, HitPoints.Actual - bullet.Damage);
            Debug.WriteLine($"{Name} takes {bullet.Damage} damage. Remaining HP: {HitPoints.Actual}");
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

        public async Task UpdateAsync()
        {
            try
            {
                var turnAction = BotAI.Update(this, myBattlefield);
                switch (turnAction)
                {
                    case ShootTurnAction shoot:
                        Execute(shoot);
                        break;
                    case MoveTowardsTurnAction moveTowards:
                        await ExecuteAsync(moveTowards);
                        break;
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private async Task ExecuteAsync(MoveTowardsTurnAction moveTowards)
        {
            var movement = new Vector(moveTowards.Position.X - Position.X, moveTowards.Position.Y - Position.Y);
            movement.Normalize();
            Direction = movement;
            await MoveAsync();
        }

        private void Execute(ShootTurnAction shoot)
        {
            var bullet = Shoot();
            if (bullet != null)
            {
                myBattlefield.Add(bullet);
            }
            else
            {

            }
        }

        public void SetPositionTo(Point position) => Position = position;
    }
}