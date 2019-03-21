using CodingArena.Annotations;
using CodingArena.Common;
using CodingArena.Main.Battlefields.Bots;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CodingArena.Main.Battlefields.Bullets
{
    public class Bullet : Movable
    {
        public Bullet([NotNull] Battlefield battlefield, [NotNull] Bot shooter) : base(battlefield)
        {
            Radius = 3;
            Shooter = shooter ?? throw new ArgumentNullException(nameof(shooter));
            Direction = new Vector(Shooter.Direction.X, Shooter.Direction.Y);
            Speed = double.Parse(ConfigurationManager.AppSettings["BulletSpeed"]);
            Damage = double.Parse(ConfigurationManager.AppSettings["BulletDamage"]);
        }

        public Bot Shooter { get; }
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
            var afterMove = new Bullet(Battlefield, Shooter) { X = X + movement.X, Y = Y + movement.Y, Radius = Radius };

            var damageBots = Battlefield.Bots.Where(bot => bot.IsInCollisionWith(afterMove));
            if (damageBots.Any())
            {
                foreach (var bot in damageBots)
                {
                    bot.TakeDamageFrom(this);
                }

                Battlefield.RemoveBullet(this);
                LastUpdate = DateTime.Now;
                return false;
            }

            X = afterMove.X;
            Y = afterMove.Y;
            LastUpdate = DateTime.Now;
            return true;
        }
    }
}