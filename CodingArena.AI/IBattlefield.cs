﻿using System.Collections.Generic;

namespace CodingArena.AI
{
    public interface IBattlefield
    {
        double Width { get; }
        double Height { get; }
        IReadOnlyList<IBot> Bots { get; }
        IReadOnlyList<IBullet> Bullets { get; }
        IReadOnlyList<IResource> Resources { get; }
        IReadOnlyList<IHome> Homes { get; }
        IReadOnlyList<IWeapon> Weapons { get; }
        IReadOnlyList<IHospital> Hospitals { get; }
        IReadOnlyList<IFirstAidKit> FirstAidKits { get; }
    }
}