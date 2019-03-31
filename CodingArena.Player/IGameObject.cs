using System;
using System.Windows;

namespace CodingArena.AI
{
    public interface IGameObject
    {
        Point Position { get; }
        TimeSpan DeltaTime { get; }
        DateTime LastUpdate { get; }
        double DistanceTo(IGameObject gameObject);
        double DistanceTo(Point point);
    }
}