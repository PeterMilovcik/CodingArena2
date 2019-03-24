using System.Windows;
using System.Windows.Media;

namespace CodingArena.Main.Battlefields.Homes
{
    public class HomeViewModel : Observable
    {
        private Visibility myCountVisibility;

        public HomeViewModel(Home home)
        {
            Home = home;
            Name = home.Name;
            X = home.Position.X;
            Y = home.Position.Y;
            Color = new SolidColorBrush(home.Color);
            home.Changed += (sender, args) => Update();
            Update();
        }

        private void Update()
        {
            CountVisibility = Visibility.Hidden;
            Count = Home.Count;
            CountVisibility = Visibility.Visible;
        }

        public Home Home { get; }

        public string Name { get; }

        public double X { get; }

        public double Y { get; }

        public Brush Color { get; }

        public int Count { get; set; }

        public Visibility CountVisibility
        {
            get => myCountVisibility;
            set
            {
                if (value == myCountVisibility) return;
                myCountVisibility = value;
                OnPropertyChanged();
            }
        }
    }
}