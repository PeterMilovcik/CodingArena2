using CodingArena.Common;
using CodingArena.Player;

namespace CodingArena.Main.Battlefields.Bases
{
    public class Base : Collider, IBase
    {
        public Base()
        {
            Radius = 30;
        }
    }
}