using System.Windows;

namespace CodingArena.Player
{
    public interface IGameObject
    {
        Point Position { get; }
        double DistanceTo(IGameObject gameObject);
    }
}