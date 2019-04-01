using CodingArena.Annotations;
using CodingArena.Main.Battlefields.Explosions;
using System;

namespace CodingArena.Main.Battlefields
{
    public class ExplosionEventArgs : EventArgs
    {
        public ExplosionEventArgs([NotNull] Explosion explosion)
        {
            Explosion = explosion ?? throw new ArgumentNullException(nameof(explosion));
        }

        public Explosion Explosion { get; }
    }
}