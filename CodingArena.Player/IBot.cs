using System.Collections.Generic;

namespace CodingArena.Player
{
    public interface IBot : IMovable
    {
        string Name { get; }
        IValue HitPoints { get; }
        bool HasResource { get; }
        bool IsAiming { get; }
        IWeapon EquippedWeapon { get; }
        IReadOnlyList<IWeapon> AvailableWeapons { get; }
        double RegenerationIn { get; }
        double RegenerationRate { get; }
    }
}