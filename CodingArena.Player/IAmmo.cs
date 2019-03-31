namespace CodingArena.AI
{
    public interface IAmmo : ICollider
    {
        string Weapon { get; }
        int Count { get; }
    }
}