using System;
using System.Windows;

namespace CodingArena.Player
{
    public interface IGameObject
    {
        Point Position { get; }
        DateTime LastUpdate { get; }
        TimeSpan DeltaTime { get; }
        double DistanceTo(IGameObject gameObject);
        double DistanceTo(Point point);
    }
}