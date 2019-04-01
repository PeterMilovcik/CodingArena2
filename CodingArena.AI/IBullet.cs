namespace CodingArena.AI
{
    public interface IBullet : IMovable
    {
        IBot Shooter { get; }
        double Damage { get; }
    }
}