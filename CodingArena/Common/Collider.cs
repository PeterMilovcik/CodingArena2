namespace CodingArena.Common
{
    public interface ICollider : AI.ICollider
    {
    }

    public class Collider : GameObject, ICollider
    {
        public double Radius { get; protected set; }
        public virtual bool IsInCollisionWith(AI.ICollider collider) =>
            DistanceTo(collider) - (Radius + collider.Radius) <= 0;
    }
}