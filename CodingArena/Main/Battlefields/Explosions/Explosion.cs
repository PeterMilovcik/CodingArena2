using CodingArena.Common;
using System.Configuration;
using System.Windows;

namespace CodingArena.Main.Battlefields.Explosions
{
    public class Explosion : Collider
    {
        public Explosion(Point position)
        {
            Position = position;
            Radius = double.Parse(ConfigurationManager.AppSettings["GrenadeExplosionRadius"]);
        }
    }
}