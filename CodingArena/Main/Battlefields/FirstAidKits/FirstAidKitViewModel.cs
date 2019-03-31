using CodingArena.Annotations;
using System;

namespace CodingArena.Main.Battlefields.FirstAidKits
{
    public sealed class FirstAidKitViewModel : Observable
    {
        public FirstAidKitViewModel([NotNull] FirstAidKit firstAidKit)
        {
            FirstAidKit = firstAidKit ?? throw new ArgumentNullException(nameof(firstAidKit));
            X = firstAidKit.Position.X;
            Y = firstAidKit.Position.Y;
        }

        public FirstAidKit FirstAidKit { get; }
        public double X { get; }
        public double Y { get; }
    }
}