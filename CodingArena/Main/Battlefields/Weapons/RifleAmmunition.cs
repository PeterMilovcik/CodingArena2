using System.Configuration;

namespace CodingArena.Main.Battlefields.Weapons
{
    public class RifleAmmunition : Ammunition
    {
        public RifleAmmunition()
        {
            Speed = double.Parse(ConfigurationManager.AppSettings["RifleAmmunitionSpeed"]);
            Damage = double.Parse(ConfigurationManager.AppSettings["RifleAmmunitionDamage"]);
            MaxCount = int.Parse(ConfigurationManager.AppSettings["RifleAmmunitionMaxCount"]);
            Remaining = int.Parse(ConfigurationManager.AppSettings["RifleAmmunitionCount"]);
        }
    }
}