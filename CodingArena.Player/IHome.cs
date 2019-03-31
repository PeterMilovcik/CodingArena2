using System.Windows.Media;

namespace CodingArena.AI
{
    public interface IHome : IGameObject
    {
        string Name { get; }
        IBot Owner { get; }
        Color Color { get; }
        int Count { get; }
    }
}