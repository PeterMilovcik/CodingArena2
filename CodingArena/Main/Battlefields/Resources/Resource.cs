using CodingArena.Common;
using System.Windows;
using CodingArena.AI;

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