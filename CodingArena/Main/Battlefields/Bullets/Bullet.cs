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
        public Bullet([NotNull] Battlefield battlefield, [NotNull] IBot shooter, double speed, double damage) : base(battlefield)
        {
            Radius = 3;
            Shooter = shooter ?? throw new ArgumentNullException(nameof(shooter));
            Direction = new Vector(Shooter.Direction.X, Shooter.Direction.Y);
            Speed = speed;
            Damage = damage;
            Position = new Point(shooter.Position.X, shooter.Position.Y);
        }

        public IBot Shooter { get; }
        public double Damage { get; }
        public double Distance { get; private set; }
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
            var afterMove = new Bullet(Battlefield, Shooter, Speed, Damage)
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
            }

            var damageBots = Battlefield.Bots.Except(new[] { Shooter }).OfType<Bot>()
                .Where(bot => bot.IsInCollisionWith(afterMove));
            if (damageBots.Any())
            {
                foreach (var bot in damageBots)
                {
                    bot.TakeDamageFrom(this);
                }

                LastUpdate = DateTime.Now;
                OnChanged();
                Battlefield.Remove(this);
                return false;
            }

            Position = new Point(afterMove.Position.X, afterMove.Position.Y);
            LastUpdate = DateTime.Now;
            OnChanged();
            return true;
        }
    }
}