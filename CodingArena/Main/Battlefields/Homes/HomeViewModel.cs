using System.Windows.Media;

namespace CodingArena.Main.Battlefields.Homes
{
    public class HomeViewModel : Observable
    {
        private readonly Home myHome;

        public HomeViewModel(Home home)
        {
            myHome = home;
            Name = myHome.Name;
            X = myHome.Position.X;
            Y = myHome.Position.Y;
            Color = new SolidColorBrush(home.Color);
            myHome.Changed += (sender, args) => Update();
            Update();
        }

        private void Update()
        {
            Count = myHome.Count;
        }

        public string Name { get; }

        public double X { get; }

        public double Y { get; }

        public Brush Color { get; }

        public int Count { get; set; }
    }
}