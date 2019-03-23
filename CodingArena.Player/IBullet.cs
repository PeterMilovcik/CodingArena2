namespace CodingArena.Player
{
    public interface IBullet : IMovable
    {
        IBot Shooter { get; }
        double Damage { get; }
    }
}