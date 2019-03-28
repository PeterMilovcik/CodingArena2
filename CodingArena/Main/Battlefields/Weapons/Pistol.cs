using CodingArena.Annotations;
using System.Windows;

namespace CodingArena.Main.Battlefields.Weapons
{
    public class Pistol : Weapon
    {
        public Pistol([NotNull] Battlefield battlefield, Point position)
            : base(battlefield, position, "Pistol")
        {
        }
    }
}