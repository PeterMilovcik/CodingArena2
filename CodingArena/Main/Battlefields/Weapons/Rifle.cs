using CodingArena.Annotations;
using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Bullets;
using System;
using System.Configuration;
using System.Windows;

namespace CodingArena.Main.Battlefields.Weapons
{
    public class Rifle : Weapon, IWeapon
    {
        public Rifle([NotNull] Battlefield battlefield) : base(battlefield)
        {
            Init();
        }

        public Rifle([NotNull] Battlefield battlefield, Point position) : base(battlefield, position)
        {
            Init();
        }

        private void Init()
        {
            Name = "Rifle";
            var reloadTimeInMilliseconds =
                double.Parse(ConfigurationManager.AppSettings["RifleReloadTimeInMilliseconds"]);
            myReloadTime = TimeSpan.FromMilliseconds(reloadTimeInMilliseconds);
            var aimTimeInMilliseconds = double.Parse(ConfigurationManager.AppSettings["RifleAimTimeInMilliseconds"]);
            myAimTime = TimeSpan.FromMilliseconds(aimTimeInMilliseconds);
            MaxRange = double.Parse(ConfigurationManager.AppSettings["RifleMaxRange"]);
            myAmmunition = new RifleAmmunition();
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