namespace CodingArena.Player
{
    public interface IBot : IMovable
    {
        string Name { get; }
        double Angle { get; }
        IValue HitPoints { get; }
        bool HasResource { get; }
        bool IsAiming { get; }
        IWeapon Weapon { get; }
    }
}