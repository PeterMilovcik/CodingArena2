using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Bullets;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace CodingArena.Main.Battlefields.Weapons
{
    public interface IWeapon : AI.IWeapon
    {
        Task UpdateAsync();
        IEnumerable<Bullet> Fire(Bot shooter, Point target);
        void Reload();
    }
}