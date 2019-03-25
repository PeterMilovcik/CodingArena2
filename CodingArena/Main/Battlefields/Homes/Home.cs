using CodingArena.Annotations;
using CodingArena.Common;
using CodingArena.Player;
using System;
using System.Windows;
using System.Windows.Media;

namespace CodingArena.Main.Battlefields.Homes
{
    public class Home : Collider, IHome
    {
        public Home([NotNull] IBot bot, Point position)
        {
            Radius = 20;
            Owner = bot ?? throw new ArgumentNullException(nameof(bot));
            Name = bot.Name;
            Position = position;
            Color = Color.FromRgb(0, 0, 0);
            Count = 0;
        }

        public string Name { get; }
        public IBot Owner { get; }
        public Color Color { get; }
        public int Count { get; private set; }

        public void IncreaseCount()
        {
            Count++;
            OnChanged();
        }
    }
}