using CodingArena.Player;
using System.Configuration;

namespace CodingArena.Main.Battlefields.Bots
{
    public class PistolBulletSpecification : IBulletSpecification
    {
        public PistolBulletSpecification()
        {
            Speed = double.Parse(ConfigurationManager.AppSettings["PistolBulletSpeed"]);
            Damage = double.Parse(ConfigurationManager.AppSettings["PistolBulletDamage"]);
        }

        public double Speed { get; }
        public double Damage { get; }
    }
}