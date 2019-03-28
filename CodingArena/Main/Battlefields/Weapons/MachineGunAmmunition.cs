using System.Configuration;

namespace CodingArena.Main.Battlefields.Weapons
{
    public class MachineGunAmmunition : Ammunition
    {
        public MachineGunAmmunition()
        {
            Speed = double.Parse(ConfigurationManager.AppSettings["MachineGunAmmunitionSpeed"]);
            Damage = double.Parse(ConfigurationManager.AppSettings["MachineGunAmmunitionDamage"]);
            MaxCount = int.Parse(ConfigurationManager.AppSettings["MachineGunAmmunitionMaxCount"]);
            Remaining = int.Parse(ConfigurationManager.AppSettings["MachineGunAmmunitionCount"]);
        }
    }
}