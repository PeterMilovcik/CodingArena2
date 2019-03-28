using CodingArena.Annotations;
using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Bullets;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;

namespace CodingArena.Main.Battlefields.Weapons
{
    public class Pistol : Weapon, IWeapon
    {
        public Pistol([NotNull] Battlefield battlefield) : base(battlefield)
        {
            Init();
        }

        public Pistol([NotNull] Battlefield battlefield, Point position) : base(battlefield, position)
        {
            Init();
        }

        private void Init()
        {
            Name = "Pistol";
            var reloadTimeInMilliseconds = double.Parse(ConfigurationManager.AppSettings["PistolReloadTimeInMilliseconds"]);
            myReloadTime = TimeSpan.FromMilliseconds(reloadTimeInMilliseconds);
            var aimTimeInMilliseconds = double.Parse(ConfigurationManager.AppSettings["PistolAimTimeInMilliseconds"]);
            myAimTime = TimeSpan.FromMilliseconds(aimTimeInMilliseconds);
            MaxRange = double.Parse(ConfigurationManager.AppSettings["PistolMaxRange"]);
            myAmmunition = new PistolAmmunition();
        }

        public override IEnumerable<Bullet> Fire(Bot shooter)
        {
            if (IsReloading) return null;
            if (Ammunition.Remaining <= 0) return null;
            myRemainingReloadTime = myReloadTime;
            myAmmunition.Remove(1);
            return new List<Bullet> { new Bullet(myBattlefield, shooter, Ammunition.Speed, Ammunition.Damage, MaxRange) };
        }
    }
}