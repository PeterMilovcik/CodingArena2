using CodingArena.Player;

namespace CodingArena.Main.Battlefields.Ammos
{
    public class AmmoViewModel : Observable
    {
        public IAmmo Ammo { get; }

        public AmmoViewModel(IAmmo ammo)
        {
            Ammo = ammo;
            Name = $"{ammo.Weapon} Ammo";
            X = ammo.Position.X;
            Y = ammo.Position.Y;
        }

        public string Name { get; }
        public double X { get; }
        public double Y { get; }
    }
}