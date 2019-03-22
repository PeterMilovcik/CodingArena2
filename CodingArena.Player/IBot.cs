namespace CodingArena.Player
{
    public interface IBot : IMovable
    {
        string Name { get; }
        double Angle { get; }
        IValue HitPoints { get; }
        bool HasResource { get; }
        IWeapon Weapon { get; }
    }
}