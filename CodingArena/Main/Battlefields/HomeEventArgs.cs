using CodingArena.Annotations;
using CodingArena.Main.Battlefields.Homes;
using System;

namespace CodingArena.Main.Battlefields
{
    public class HomeEventArgs : EventArgs
    {
        public HomeEventArgs([NotNull] Home home)
        {
            Home = home ?? throw new ArgumentNullException(nameof(home));
        }

        public Home Home { get; }
    }
}