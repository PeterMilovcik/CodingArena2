using CodingArena.AI;
using CodingArena.Annotations;
using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Explosions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace CodingArena.Main.Battlefields.Bullets
{
    public class ReleasedGrenade : Bullet
    {
        private readonly Point myTarget;
        private readonly double myExplosionRadius;

        public ReleasedGrenade(
            [NotNull] Battlefield battlefield,
            [NotNull] IBot shooter,
            double speed,
            double damage,
            double maxBulletDistance,
            Point target)
            : base(battlefield, shooter, speed, damage, maxBulletDistance)
        {
            myTarget = target;
            myExplosionRadius = double.Parse(ConfigurationManager.AppSettings["GrenadeExplosionRadius"]);
        }

        protected override bool OnCollisionWith(List<Bot> bots) => false;

        protected override void OnMoved(Bullet afterMove)
        {
            if (DeltaTime == TimeSpan.Zero)
            {
                base.OnMoved(afterMove);
                return;
            }
            Debug.WriteLine($"Released grenade deltaTime.TotalSeconds = {DeltaTime.TotalSeconds}");
            var distanceAfterMove = afterMove.DistanceTo(myTarget) * DeltaTime.TotalSeconds;
            Debug.WriteLine($"Released grenade after move distance * deltaTime.TotalSeconds = {distanceAfterMove}");
            if (distanceAfterMove < 3)
            {
                var botsToDamage = Battlefield.Bots
                    .OfType<Bot>()
                    .Where(bot => afterMove.DistanceTo(bot) < myExplosionRadius)
                    .ToList();
                if (botsToDamage.Any())
                {
                    botsToDamage.ForEach(bot => bot.TakeDamageFrom(this));
                }
                Battlefield.Remove(this);
                Battlefield.Add(new Explosion(Battlefield, Position));
            }
            else
            {
                base.OnMoved(afterMove);
            }
        }
    }
}