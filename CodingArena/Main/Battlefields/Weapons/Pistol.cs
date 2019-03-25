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
    public class PistolAmmunition : Ammunition
    {
        public PistolAmmunition()
        {
            Speed = double.Parse(ConfigurationManager.AppSettings["PistolAmmunitionSpeed"]);
            Damage = double.Parse(ConfigurationManager.AppSettings["PistolAmmunitionDamage"]);
            Remaining = int.Parse(ConfigurationManager.AppSettings["PistolAmmunitionCount"]);
        }
    }

    public class Pistol : Collider, IWeapon
    {
        private readonly Battlefield myBattlefield;
        private readonly TimeSpan myAimTime;
        private readonly TimeSpan myReloadTime;
        private readonly PistolAmmunition myAmmunition;
        private TimeSpan myRemainingReloadTime;

        public Pistol([NotNull] Battlefield battlefield)
        {
            myBattlefield = battlefield ?? throw new ArgumentNullException(nameof(battlefield));
            Name = "Pistol";
            var reloadTimeInMilliseconds = double.Parse(ConfigurationManager.AppSettings["PistolReloadTimeInMilliseconds"]);
            myReloadTime = TimeSpan.FromMilliseconds(reloadTimeInMilliseconds);
            var aimTimeInMilliseconds = double.Parse(ConfigurationManager.AppSettings["PistolAimTimeInMilliseconds"]);
            myAimTime = TimeSpan.FromMilliseconds(aimTimeInMilliseconds);
            MaxRange = double.Parse(ConfigurationManager.AppSettings["PistolMaxRange"]);
            myAmmunition = new PistolAmmunition();
        }

        public string Name { get; }
        public double MaxRange { get; }
        public double ReloadTime => myReloadTime.TotalSeconds;
        public double AimTime => myAimTime.TotalSeconds;
        public bool IsReloading => myRemainingReloadTime > TimeSpan.Zero;
        public double RemainingReloadTime => myRemainingReloadTime.TotalSeconds;
        public IAmmunition Ammunition => myAmmunition;

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
            if (Ammunition.Remaining <= 0) return null;
            myRemainingReloadTime = myReloadTime;
            myAmmunition.Remove(1);
            return new Bullet(myBattlefield, shooter, Ammunition.Speed, Ammunition.Damage, MaxRange);
        }

        public void Reload()
        {
            if (IsReloading)
            {
                myRemainingReloadTime -= DeltaTime;
            }
        }
    }
}