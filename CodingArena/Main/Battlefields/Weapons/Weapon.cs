using CodingArena.Annotations;
using CodingArena.Common;
using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Bullets;
using CodingArena.Player;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;

namespace CodingArena.Main.Battlefields.Weapons
{
    public abstract class Weapon : Collider, IWeapon
    {
        protected Battlefield myBattlefield;
        protected TimeSpan myAimTime;
        protected TimeSpan myReloadTime;
        protected Ammunition myAmmunition;
        protected TimeSpan myRemainingReloadTime;

        protected Weapon([NotNull] Battlefield battlefield, Point position, string name)
        {
            myBattlefield = battlefield ?? throw new ArgumentNullException(nameof(battlefield));
            Position = position;
            Init(name);
        }

        public string Name { get; protected set; }
        public double MaxRange { get; protected set; }
        public TimeSpan ReloadTime => new TimeSpan(myReloadTime.Ticks);
        public TimeSpan AimTime => new TimeSpan(myAimTime.Ticks);
        public double Accuracy { get; protected set; }
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

        protected void Init(string name)
        {
            Name = name;
            string prefix = Name.Replace(" ", "");
            var reloadTimeInMilliseconds =
                double.Parse(ConfigurationManager.AppSettings[prefix + "ReloadTimeInMilliseconds"]);
            myReloadTime = TimeSpan.FromMilliseconds(reloadTimeInMilliseconds);
            var aimTimeInMilliseconds = double.Parse(ConfigurationManager.AppSettings[prefix + "AimTimeInMilliseconds"]);
            myAimTime = TimeSpan.FromMilliseconds(aimTimeInMilliseconds);
            MaxRange = double.Parse(ConfigurationManager.AppSettings[prefix + "MaxRange"]);
            Accuracy = double.Parse(ConfigurationManager.AppSettings[prefix + "Accuracy"]);
            myAmmunition = new Ammunition(Name);
        }

        protected void DecreaseAmmunitionBy(int count) => myAmmunition.Remove(count);
        protected bool CanFire() => !IsReloading && Ammunition.Remaining > 0;
        protected void ResetRemainingReloadTime() => myRemainingReloadTime = myReloadTime;

        protected virtual IEnumerable<Bullet> CreateBullets(Bot shooter) =>
            new List<Bullet>
            {
                new Bullet(myBattlefield, shooter, Ammunition.Speed, Ammunition.Damage, MaxRange)
            };

        public virtual IEnumerable<Bullet> Fire(Bot shooter)
        {
            if (!CanFire()) return new List<Bullet>();
            ResetRemainingReloadTime();
            DecreaseAmmunitionBy(1);
            return CreateBullets(shooter);
        }
    }
}