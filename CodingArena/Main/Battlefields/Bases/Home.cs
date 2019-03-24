using CodingArena.Annotations;
using CodingArena.Common;
using CodingArena.Player;
using System;
using System.Windows.Media;

namespace CodingArena.Main.Battlefields.Bases
{
    public class Home : Collider, IHome
    {
        public Home([NotNull] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            Radius = 30;
            Name = name;
            Color = Color.FromRgb(0, 0, 0);
        }

        public string Name { get; }
        public Color Color { get; }
    }
}