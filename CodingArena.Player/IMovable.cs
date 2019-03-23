using System;
using System.Windows;

namespace CodingArena.Player
{
    public interface IMovable : ICollider
    {
        double MinSpeed { get; }
        double MaxSpeed { get; }
        double Speed { get; }
        double Angle { get; }
        Vector Direction { get; }
        DateTime LastUpdate { get; }
    }
}