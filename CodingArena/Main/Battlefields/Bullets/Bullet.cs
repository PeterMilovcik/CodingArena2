using CodingArena.Annotations;
using CodingArena.Common;
using CodingArena.Main.Battlefields.Bots;
using CodingArena.Player;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CodingArena.Main.Battlefields.Bullets
{
    public class Bullet : Movable, IBullet
    {
        private static readonly Random myRandom = new Random();

        public Bullet(
            [NotNull] Battlefield battlefield,
            [NotNull] IBot shooter,
            double speed,
            double damage,
            double maxBulletDistance) : base(battlefield)
        {
            Radius = 3;
            Shooter = shooter ?? throw new ArgumentNullException(nameof(shooter));
            Direction = CalculateDirection();
            Speed = speed;
            Damage = damage;
            var weaponX = Shooter.Position.X + 30 * Math.Cos(Shooter.Angle * Math.PI / 180);
            var weaponY = Shooter.Position.Y + 30 * Math.Sin(Shooter.Angle * Math.PI / 180);
            Position = new Point(weaponX, weaponY);
            MaxDistance = maxBulletDistance;
        }

        private Vector CalculateDirection()
        {
            var angle = Shooter.Angle;
            var accuracy = Shooter.EquippedWeapon.Accuracy;
            var angleDif = (360 - 360 * accuracy / 100) / 2;
            angleDif = myRandom.NextDouble() * angleDif;
            var newAngle = myRandom.Next(2) == 1 ? angle - angleDif : angle + angleDif;
            return new Vector(Math.Cos(newAngle * Math.PI / 180), Math.Sin(newAngle * Math.PI / 180));
        }

        public IBot Shooter { get; }
        public double Damage { get; }
        public double Distance { get; private set; }
        public double MaxDistance { get; }
        public override async Task UpdateAsync()
        {
            await base.UpdateAsync();
            Move();
        }

        public bool Move()
        {
            var movement = new Vector(Direction.X, Direction.Y);
            movement.X *= Speed * DeltaTime.TotalSeconds;
            movement.Y *= Speed * DeltaTime.TotalSeconds;

            var afterMove = new Bullet(Battlefield, Shooter, Speed, Damage, MaxDistance)
            {
                Position = new Point(Position.X + movement.X, Position.Y + movement.Y),
                Radius = Radius
            };

            if (afterMove.Position.X > Battlefield.Width - 1 ||
                afterMove.Position.X < 0 ||
                afterMove.Position.Y > Battlefield.Height - 1 ||
                afterMove.Position.Y < 0)
            {
                Battlefield.Remove(this);
                return true;
            }

            Distance += movement.Length;
            if (Distance > MaxDistance)
            {
                Battlefield.Remove(this);
                return true;
            }

            var damageBots = Battlefield.Bots.Except(new[] { Shooter }).OfType<Bot>()
                .Where(bot => bot.IsInCollisionWith(afterMove)).ToList();
            if (damageBots.Any())
            {
                foreach (var bot in damageBots)
                {
                    bot.TakeDamageFrom(this);
                }

                OnChanged();
                Battlefield.Remove(this);
                return false;
            }

            Position = new Point(afterMove.Position.X, afterMove.Position.Y);
            OnChanged();
            return true;
        }
    }
}