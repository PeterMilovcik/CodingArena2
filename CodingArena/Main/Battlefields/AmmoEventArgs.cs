using CodingArena.Annotations;
using CodingArena.Player;
using System;

namespace CodingArena.Main.Battlefields
{
    public class AmmoEventArgs : EventArgs
    {
        public AmmoEventArgs([NotNull] IAmmo ammo)
        {
            Ammo = ammo ?? throw new ArgumentNullException(nameof(ammo));
        }

        public IAmmo Ammo { get; }
    }
}