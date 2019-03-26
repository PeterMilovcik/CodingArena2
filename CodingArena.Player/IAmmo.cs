namespace CodingArena.Player
{
    public interface IAmmo : ICollider
    {
        string Weapon { get; }
        int Count { get; }
    }
}