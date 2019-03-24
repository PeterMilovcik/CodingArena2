using System.Collections.Generic;

namespace CodingArena.Player
{
    public interface IBattlefield
    {
        IReadOnlyList<IBot> Bots { get; }
        IReadOnlyList<IBullet> Bullets { get; }
        IReadOnlyList<IResource> Resources { get; }
        IReadOnlyList<IHome> Homes { get; }
        IReadOnlyList<IWeapon> Weapons { get; }
    }
}