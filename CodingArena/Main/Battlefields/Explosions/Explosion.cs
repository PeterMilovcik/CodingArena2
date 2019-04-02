using CodingArena.Annotations;
using CodingArena.Common;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;

namespace CodingArena.Main.Battlefields.Explosions
{
    public class Explosion : Collider
    {
        [NotNull] private readonly Battlefield myBattlefield;
        private TimeSpan myGrenadeExplosionDuration;

        public Explosion([NotNull] Battlefield battlefield, Point position)
        {
            myBattlefield = battlefield ?? throw new ArgumentNullException(nameof(battlefield));
            Position = position;
            Radius = double.Parse(ConfigurationManager.AppSettings["GrenadeExplosionRadius"]);
            myGrenadeExplosionDuration =
                TimeSpan.FromMilliseconds(
                    double.Parse(ConfigurationManager.AppSettings["GrenadeExplosionDurationInMilliseconds"]));
        }

        public override async Task UpdateAsync()
        {
            await base.UpdateAsync();
            myGrenadeExplosionDuration -= DeltaTime;
            if (myGrenadeExplosionDuration < TimeSpan.Zero)
            {
                myBattlefield.Remove(this);
            }
        }
    }
}