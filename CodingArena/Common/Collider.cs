namespace CodingArena.Common
{
    public interface ICollider : IGameObject
    {
        double Radius { get; }
        bool IsInCollisionWith(ICollider collider);
    }

    public class Collider : GameObject, ICollider
    {
        public double Radius { get; protected set; }
        public virtual bool IsInCollisionWith(ICollider collider) =>
            DistanceTo(collider) - (Radius + collider.Radius) <= 0;
    }
}