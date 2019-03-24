using CodingArena.Annotations;
using CodingArena.Common;
using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Bullets;
using CodingArena.Player;
using System;
using System.Configuration;
using System.Threading.Tasks;

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
            var reloadTimeInMilliseconds = double.Parse(ConfigurationManager.AppSettings["PistolReloadTimeInMilliseconds"]);
            ReloadTime = TimeSpan.FromMilliseconds(reloadTimeInMilliseconds);
            var aimTimeInMilliseconds = double.Parse(ConfigurationManager.AppSettings["PistolAimTimeInMilliseconds"]);
            AimTime = TimeSpan.FromMilliseconds(aimTimeInMilliseconds);
            MaxRange = double.Parse(ConfigurationManager.AppSettings["PistolMaxRange"]);
            Bullet = new PistolBullet();
        }

        public string Name { get; }
        public double MaxRange { get; }
        public TimeSpan ReloadTime { get; }
        public TimeSpan AimTime { get; }
        public bool IsReloading => RemainingReloadTime > TimeSpan.Zero;
        public TimeSpan RemainingReloadTime { get; private set; }
        public IBulletSpecification Bullet { get; }

        public override async Task UpdateAsync()
        {
            await base.UpdateAsync();
            if (IsReloading)
            {
                Reload();
            }
        }

        public Bullet Fire(Bot shooter)
        {
            if (IsReloading) return null;
            RemainingReloadTime = ReloadTime;
            return new Bullet(myBattlefield, shooter, Bullet.Speed, Bullet.Damage, MaxRange);
        }

        public void Reload()
        {
            if (IsReloading)
            {
                RemainingReloadTime -= DeltaTime;
            }
        }
    }
}