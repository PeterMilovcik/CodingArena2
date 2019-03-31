using System.Windows;

namespace CodingArena.AI
{
    public interface IMovable : ICollider
    {
        double MinSpeed { get; }
        double MaxSpeed { get; }
        double Speed { get; }
        double Angle { get; }
        Vector Direction { get; }
    }
}