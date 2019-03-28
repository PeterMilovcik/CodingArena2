using CodingArena.Annotations;
using CodingArena.Main.Battlefields.Weapons;
using System;

namespace CodingArena.Main.Battlefields
{
    public class WeaponEventArgs : EventArgs
    {
        public WeaponEventArgs([NotNull] Weapon weapon)
        {
            Weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
        }

        public Weapon Weapon { get; }
    }
}