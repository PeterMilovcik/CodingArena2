using CodingArena.Annotations;
using System.Windows;

namespace CodingArena.Main.Battlefields.Weapons
{
    public class SniperRifle : Weapon
    {
        public SniperRifle([NotNull] Battlefield battlefield, Point position)
            : base(battlefield, position, "Sniper Rifle")
        {
        }
    }
}