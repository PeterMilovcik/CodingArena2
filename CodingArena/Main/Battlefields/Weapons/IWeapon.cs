using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Bullets;

namespace CodingArena.Main.Battlefields.Weapons
{
    public interface IWeapon : Player.IWeapon
    {
        Bullet Fire(Bot shooter);
        void Reload();
    }
}