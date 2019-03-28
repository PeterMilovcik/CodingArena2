using System.Configuration;

namespace CodingArena.Main.Battlefields.Weapons
{
    public class SniperRifleAmmunition : Ammunition
    {
        public SniperRifleAmmunition()
        {
            Speed = double.Parse(ConfigurationManager.AppSettings["SniperRifleAmmunitionSpeed"]);
            Damage = double.Parse(ConfigurationManager.AppSettings["SniperRifleAmmunitionDamage"]);
            MaxCount = int.Parse(ConfigurationManager.AppSettings["SniperRifleAmmunitionMaxCount"]);
            Remaining = int.Parse(ConfigurationManager.AppSettings["SniperRifleAmmunitionCount"]);
        }
    }
}