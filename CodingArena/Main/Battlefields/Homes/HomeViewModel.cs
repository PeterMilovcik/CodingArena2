using System.Windows.Media;
using CodingArena.Player;

namespace CodingArena.Main.Battlefields.Homes
{
    public class HomeViewModel : Observable
    {
        public HomeViewModel(IHome home)
        {
            Name = home.Name;
            X = home.Position.X;
            Y = home.Position.Y;
            Color = new SolidColorBrush(home.Color);
        }

        public string Name { get; }

        public double X { get; }

        public double Y { get; }

        public Brush Color { get; }
    }
}