using CodingArena.Common;
using CodingArena.Player;

namespace CodingArena.Main.Battlefields.Resources
{
    public class Resource : Collider, IResource
    {
        public Resource()
        {
            Radius = 10;
        }
    }
}