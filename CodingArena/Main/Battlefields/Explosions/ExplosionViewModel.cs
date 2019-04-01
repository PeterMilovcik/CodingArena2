using CodingArena.Annotations;
using System;

namespace CodingArena.Main.Battlefields.Explosions
{
    public class ExplosionViewModel : Observable
    {
        public ExplosionViewModel([NotNull] Explosion explosion)
        {
            Explosion = explosion ?? throw new ArgumentNullException(nameof(explosion));
            X = explosion.Position.X;
            Y = explosion.Position.Y;
            Size = explosion.Radius * 2;
            Offset = -Size / 2;
        }

        public Explosion Explosion { get; }
        public double X { get; }
        public double Y { get; }
        public double Size { get; }
        public double Offset { get; }
    }
}