using CodingArena.Common;
using CodingArena.Player;
using System.Windows;

namespace CodingArena.Main.Battlefields.Ammos
{
    public class Ammo : Collider, IAmmo
    {
        public Ammo(Point position, string weapon, int count)
        {
            Radius = 10;
            Position = position;
            Weapon = weapon;
            Count = count;
        }

        public string Weapon { get; }
        public int Count { get; }
    }
}