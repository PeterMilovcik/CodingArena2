using System;

namespace CodingArena.Player
{
    public interface IWeapon : ICollider
    {
        string Name { get; }
        double MaxRange { get; }
        TimeSpan ReloadTime { get; }
        TimeSpan AimTime { get; }
        bool IsReloading { get; }
        TimeSpan RemainingReloadTime { get; }
        IBulletSpecification Bullet { get; }
    }
}