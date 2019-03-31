using CodingArena.Annotations;
using CodingArena.Main.Battlefields.FirstAidKits;
using System;

namespace CodingArena.Main.Battlefields
{
    public class FirstAidKitEventArgs : EventArgs
    {
        public FirstAidKitEventArgs([NotNull] FirstAidKit firstAidKit)
        {
            FirstAidKit = firstAidKit ?? throw new ArgumentNullException(nameof(firstAidKit));
        }

        public FirstAidKit FirstAidKit { get; }
    }
}