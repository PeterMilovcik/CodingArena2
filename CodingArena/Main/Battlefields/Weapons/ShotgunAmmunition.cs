using System.Configuration;

namespace CodingArena.Main.Battlefields.Weapons
{
    public class ShotgunAmmunition : Ammunition
    {
        public ShotgunAmmunition()
        {
            Speed = double.Parse(ConfigurationManager.AppSettings["ShotgunAmmunitionSpeed"]);
            Damage = double.Parse(ConfigurationManager.AppSettings["ShotgunAmmunitionDamage"]);
            MaxCount = int.Parse(ConfigurationManager.AppSettings["ShotgunAmmunitionMaxCount"]);
            Remaining = int.Parse(ConfigurationManager.AppSettings["ShotgunAmmunitionCount"]);
        }
    }
}