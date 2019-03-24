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