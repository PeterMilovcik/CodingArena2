using CodingArena.Annotations;
using System.Windows;

namespace CodingArena.Main.Battlefields.Weapons
{
    public class Shotgun : Weapon
    {
        public Shotgun([NotNull] Battlefield battlefield, Point position)
            : base(battlefield, position, "Shotgun")
        {
        }
    }
}