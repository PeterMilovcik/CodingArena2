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
            MaxCount = int.Parse(ConfigurationManager.AppSettings["PistolAmmunitionCount"]);
            Remaining = MaxCount / 2;
        }
    }

    public abstract class Weapon : Collider
    {
        protected Battlefield myBattlefield;
        protected TimeSpan myAimTime;
        protected TimeSpan myReloadTime;
        protected Ammunition myAmmunition;
        protected TimeSpan myRemainingReloadTime;

        protected Weapon([NotNull] Battlefield battlefield)
        {
            myBattlefield = battlefield ?? throw new ArgumentNullException(nameof(battlefield));
        }

        public string Name { get; protected set; }
        public double MaxRange { get; protected set; }
        public TimeSpan ReloadTime => new TimeSpan(myReloadTime.Ticks);
        public TimeSpan AimTime => new TimeSpan(myAimTime.Ticks);
        public bool IsReloading => myRemainingReloadTime > TimeSpan.Zero;
        public TimeSpan RemainingReloadTime => new TimeSpan(myRemainingReloadTime.Ticks);
        public IAmmunition Ammunition => myAmmunition;

        public override async Task UpdateAsync()
        {
            await base.UpdateAsync();
            if (IsReloading)
            {
                Reload();
            }
        }

        public void Reload()
        {
            if (IsReloading)
            {
                myRemainingReloadTime -= DeltaTime;
            }
        }

        public abstract Bullet Fire(Bot shooter);
    }

    public class Pistol : Weapon, IWeapon
    {
        public Pistol([NotNull] Battlefield battlefield) : base(battlefield)
        {
            Name = "Pistol";
            var reloadTimeInMilliseconds = double.Parse(ConfigurationManager.AppSettings["PistolReloadTimeInMilliseconds"]);
            myReloadTime = TimeSpan.FromMilliseconds(reloadTimeInMilliseconds);
            var aimTimeInMilliseconds = double.Parse(ConfigurationManager.AppSettings["PistolAimTimeInMilliseconds"]);
            myAimTime = TimeSpan.FromMilliseconds(aimTimeInMilliseconds);
            MaxRange = double.Parse(ConfigurationManager.AppSettings["PistolMaxRange"]);
            myAmmunition = new PistolAmmunition();
        }

        public override Bullet Fire(Bot shooter)
        {
            if (IsReloading) return null;
            if (Ammunition.Remaining <= 0) return null;
            myRemainingReloadTime = myReloadTime;
            myAmmunition.Remove(1);
            return new Bullet(myBattlefield, shooter, Ammunition.Speed, Ammunition.Damage, MaxRange);
        }
    }
}