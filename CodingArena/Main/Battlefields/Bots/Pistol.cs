using CodingArena.Annotations;
using CodingArena.Main.Battlefields.Bullets;
using CodingArena.Player;
using System;
using System.Configuration;

namespace CodingArena.Main.Battlefields.Bots
{
    public class Pistol : IWeapon
    {
        private readonly Battlefield myBattlefield;

        public Pistol([NotNull] Battlefield battlefield)
        {
            myBattlefield = battlefield ?? throw new ArgumentNullException(nameof(battlefield));
            Name = "Pistol";
            Damage = double.Parse(ConfigurationManager.AppSettings["PistolDamage"]);
            var reloadTimeInSeconds = double.Parse(ConfigurationManager.AppSettings["PistolReloadTimeInSeconds"]);
            ReloadTime = TimeSpan.FromSeconds(reloadTimeInSeconds);
            Bullet = new PistolBulletSpecification();
        }

        public string Name { get; }
        public double Damage { get; }
        public TimeSpan ReloadTime { get; }
        public bool IsReloading => RemainingReloadTime > TimeSpan.Zero;
        public TimeSpan RemainingReloadTime { get; private set; }
        public IBulletSpecification Bullet { get; }
        public DateTime LastUpdate { get; private set; }

        public Bullet Fire(Bot shooter)
        {
            LastUpdate = DateTime.Now;
            if (IsReloading) return null;
            RemainingReloadTime = ReloadTime;
            return new Bullet(myBattlefield, shooter, Bullet.Speed, Bullet.Damage);
        }

        public void Reload()
        {
            if (IsReloading)
            {
                var deltaTime = DateTime.Now - LastUpdate;
                RemainingReloadTime -= deltaTime;
            }
            LastUpdate = DateTime.Now;
        }
    }
}