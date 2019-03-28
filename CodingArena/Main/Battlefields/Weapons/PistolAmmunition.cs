using System.Configuration;

namespace CodingArena.Main.Battlefields.Weapons
{
    public class PistolAmmunition : Ammunition
    {
        public PistolAmmunition()
        {
            Speed = double.Parse(ConfigurationManager.AppSettings["PistolAmmunitionSpeed"]);
            Damage = double.Parse(ConfigurationManager.AppSettings["PistolAmmunitionDamage"]);
            MaxCount = int.Parse(ConfigurationManager.AppSettings["PistolAmmunitionMaxCount"]);
            Remaining = int.Parse(ConfigurationManager.AppSettings["PistolAmmunitionCount"]);
        }
    }
}