using CodingArena.Player;
using System;
using System.Configuration;

namespace CodingArena.Main.Battlefields.Weapons
{
    public class Ammunition : IAmmunition
    {
        public Ammunition(string name)
        {
            Init(name.Replace(" ", ""));
        }

        public double Speed { get; protected set; }
        public double Damage { get; protected set; }
        public int Remaining { get; protected set; }
        public int MaxCount { get; protected set; }

        public void Add(int count)
        {
            Remaining += count;
            Remaining = Math.Min(MaxCount, Remaining);
        }

        public void Remove(int count)
        {
            Remaining -= count;
            Remaining = Math.Max(Remaining, 0);
        }

        protected void Init(string prefix)
        {
            Speed = double.Parse(ConfigurationManager.AppSettings[prefix + "AmmunitionSpeed"]);
            Damage = double.Parse(ConfigurationManager.AppSettings[prefix + "AmmunitionDamage"]);
            MaxCount = int.Parse(ConfigurationManager.AppSettings[prefix + "AmmunitionMaxCount"]);
            Remaining = int.Parse(ConfigurationManager.AppSettings[prefix + "AmmunitionCount"]);
        }
    }
}