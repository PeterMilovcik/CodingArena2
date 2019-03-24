using CodingArena.Player;
using System.Windows.Media;

namespace CodingArena.Main.Battlefields.Bases
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