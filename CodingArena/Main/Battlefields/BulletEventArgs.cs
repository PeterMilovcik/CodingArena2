using CodingArena.Annotations;
using CodingArena.Main.Battlefields.Bullets;
using System;

namespace CodingArena.Main.Battlefields
{
    public class BulletEventArgs : EventArgs
    {
        public Bullet Bullet { get; }

        public BulletEventArgs([NotNull] Bullet bullet)
        {
            Bullet = bullet ?? throw new ArgumentNullException(nameof(bullet));
        }
    }
}