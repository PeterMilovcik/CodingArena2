using CodingArena.Annotations;
using System.Windows;

namespace CodingArena.Main.Battlefields.Weapons
{
    public class Rifle : Weapon
    {
        public Rifle([NotNull] Battlefield battlefield, Point position)
            : base(battlefield, position, "Rifle")
        {
        }
    }
}