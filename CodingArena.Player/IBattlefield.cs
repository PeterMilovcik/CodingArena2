using System.Collections.Generic;

namespace CodingArena.Player
{
    public interface IBattlefield
    {
        IReadOnlyList<IBot> Bots { get; }
        IReadOnlyList<IBullet> Bullets { get; }
        IReadOnlyList<IResource> Resources { get; }
        IReadOnlyList<IBase> Bases { get; }
        IReadOnlyList<IWeapon> Weapons { get; }
    }
}