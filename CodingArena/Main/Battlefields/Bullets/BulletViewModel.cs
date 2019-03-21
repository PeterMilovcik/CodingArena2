namespace CodingArena.Main.Battlefields.Bullets
{
    public class BulletViewModel : Observable
    {
        private double myX;
        private double myY;

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
    }
}