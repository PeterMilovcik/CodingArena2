﻿namespace CodingArena.AI
{
    public interface IAmmunition
    {
        double Speed { get; }
        double Damage { get; }
        int Remaining { get; }
        int MaxCount { get; }
    }
}