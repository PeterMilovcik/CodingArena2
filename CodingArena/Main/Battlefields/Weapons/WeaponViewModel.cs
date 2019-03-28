namespace CodingArena.Main.Battlefields.Weapons
{
    public class WeaponViewModel : Observable
    {
        public WeaponViewModel(Weapon weapon)
        {
            Weapon = weapon;
            Name = Weapon.Name;
            X = Weapon.Position.X;
            Y = Weapon.Position.Y;
        }

        public Weapon Weapon { get; }
        public string Name { get; }
        public double X { get; }
        public double Y { get; }
    }
}