using System.Windows.Media;

namespace CodingArena.Player
{
    public interface IHome : IGameObject
    {
        string Name { get; }
        Color Color { get; }
    }
}