namespace CodingArena.Common
{
    public interface ICollider : Player.ICollider
    {
    }

    public class Collider : GameObject, ICollider
    {
        public double Radius { get; protected set; }
        public virtual bool IsInCollisionWith(Player.ICollider collider) =>
            DistanceTo(collider) - (Radius + collider.Radius) <= 0;
    }
}