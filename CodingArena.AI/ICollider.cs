namespace CodingArena.AI
{
    public interface ICollider : IGameObject
    {
        double Radius { get; }
        bool IsInCollisionWith(ICollider collider);
    }
}