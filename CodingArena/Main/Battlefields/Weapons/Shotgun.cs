using CodingArena.Annotations;
using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Bullets;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;

namespace CodingArena.Main.Battlefields.Weapons
{
    public class Shotgun : Weapon
    {
        private readonly int myBulletsPerShot;

        public Shotgun([NotNull] Battlefield battlefield, Point position)
            : base(battlefield, position, "Shotgun")
        {
            myBulletsPerShot = int.Parse(ConfigurationManager.AppSettings["ShotgunBulletsPerShot"]);
        }

        protected override IEnumerable<Bullet> CreateBullets(Bot shooter)
        {
            var bullets = new List<Bullet>();
            for (int i = 0; i < myBulletsPerShot; i++)
            {
                var bullet = new Bullet(myBattlefield, shooter, Ammunition.Speed, Ammunition.Damage, MaxRange);
                bullets.Add(bullet);
            }

            return bullets;
        }
    }
}