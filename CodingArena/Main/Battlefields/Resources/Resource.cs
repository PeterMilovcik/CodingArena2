using CodingArena.Common;
using CodingArena.Player;
using System.Windows;

namespace CodingArena.Main.Battlefields.Resources
{
    public class Resource : Collider, IResource
    {
        public Resource(Point position)
        {
            Radius = 10;
            Position = position;
        }
    }
}