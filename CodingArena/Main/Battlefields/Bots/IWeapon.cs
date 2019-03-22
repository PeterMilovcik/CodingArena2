using CodingArena.Main.Battlefields.Bullets;

namespace CodingArena.Main.Battlefields.Bots
{
    public interface IWeapon : Player.IWeapon
    {
        Bullet Fire(Bot shooter);
        void Reload();
    }
}