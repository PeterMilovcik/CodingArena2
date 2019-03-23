using CodingArena.Annotations;
using CodingArena.Common;
using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Bullets;
using CodingArena.Player;
using System;
using System.Configuration;

namespace CodingArena.Main.Battlefields.Weapons
{
    public class PistolBullet : IBulletSpecification
    {
        public PistolBullet()
        {
            Speed = double.Parse(ConfigurationManager.AppSettings["PistolBulletSpeed"]);
            Damage = double.Parse(ConfigurationManager.AppSettings["PistolBulletDamage"]);
        }

        public double Speed { get; }
        public double Damage { get; }
    }

    public class Pistol : Collider, IWeapon
    {
        private readonly Battlefield myBattlefield;

        public Pistol([NotNull] Battlefield battlefield)
        {
            myBattlefield = battlefield ?? throw new ArgumentNullException(nameof(battlefield));
            Name = "Pistol";
            var reloadTimeInSeconds = double.Parse(ConfigurationManager.AppSettings["PistolReloadTimeInSeconds"]);
            ReloadTime = TimeSpan.FromSeconds(reloadTimeInSeconds);
            MaxRange = double.Parse(ConfigurationManager.AppSettings["PistolMaxRange"]);
            Bullet = new PistolBullet();
        }

        public string Name { get; }
        public double Damage { get; }
        public double MaxRange { get; }
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