using CodingArena.Annotations;
using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Bullets;
using System.Collections.Generic;
using System.Windows;

namespace CodingArena.Main.Battlefields.Weapons
{
    public sealed class Grenade : Weapon
    {
        public Grenade([NotNull] Battlefield battlefield, Point position)
            : base(battlefield, position, "Grenade")
        {
        }

        protected override IEnumerable<Bullet> CreateBullets(Bot shooter) =>
            new List<Bullet>
            {
                new ReleasedGrenade(myBattlefield, shooter, Ammunition.Speed, Ammunition.Damage, MaxRange, myTarget)
            };
    }
}