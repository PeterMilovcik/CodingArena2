using CodingArena.Player;
using System;

namespace CodingArena.Main.Battlefields.Weapons
{
    public class Ammunition : IAmmunition
    {
        public double Speed { get; protected set; }
        public double Damage { get; protected set; }
        public int Remaining { get; protected set; }

        public void Add(int count) => Remaining += count;

        public void Remove(int count)
        {
            Remaining -= count;
            Remaining = Math.Max(Remaining, 0);
        }
    }
}