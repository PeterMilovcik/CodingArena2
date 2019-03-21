using System.Windows.Media;

namespace CodingArena.Main.Battlefields.Bases
{
    public class BaseViewModel : Observable
    {
        private double myX;
        private double myY;
        private Brush myColor;

        public double X
        {
            get => myX;
            set
            {
                if (value.Equals(myX)) return;
                myX = value;
                OnPropertyChanged();
            }
        }

        public double Y
        {
            get => myY;
            set
            {
                if (value.Equals(myY)) return;
                myY = value;
                OnPropertyChanged();
            }
        }

        public Brush Color
        {
            get => myColor;
            set
            {
                if (Equals(value, myColor)) return;
                myColor = value;
                OnPropertyChanged();
            }
        }
    }
}