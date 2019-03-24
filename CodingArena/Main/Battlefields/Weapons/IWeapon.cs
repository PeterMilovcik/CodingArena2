using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Bullets;
using System.Threading.Tasks;

namespace CodingArena.Main.Battlefields.Weapons
{
    public interface IWeapon : Player.IWeapon
    {
        Task UpdateAsync();
        Bullet Fire(Bot shooter);
        void Reload();
    }
}