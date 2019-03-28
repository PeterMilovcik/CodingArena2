using CodingArena.Annotations;
using System.Windows;

namespace CodingArena.Main.Battlefields.Weapons
{
    public class MachineGun : Weapon
    {
        public MachineGun([NotNull] Battlefield battlefield, Point position)
            : base(battlefield, position, "Machine Gun")
        {
        }
    }
}