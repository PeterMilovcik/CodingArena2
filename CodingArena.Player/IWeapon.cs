using System;

namespace CodingArena.Player
{
    public interface IWeapon
    {
        string Name { get; }
        double Damage { get; }
        TimeSpan ReloadTime { get; }
        bool IsReloading { get; }
        TimeSpan RemainingReloadTime { get; }
        IBulletSpecification Bullet { get; }
    }
}