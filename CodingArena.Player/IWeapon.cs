namespace CodingArena.Player
{
    public interface IWeapon : ICollider
    {
        string Name { get; }
        double MaxRange { get; }
        double ReloadTime { get; }
        double AimTime { get; }
        bool IsReloading { get; }
        double RemainingReloadTime { get; }
        IBulletSpecification Bullet { get; }
    }
}