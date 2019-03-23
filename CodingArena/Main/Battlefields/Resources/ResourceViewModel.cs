using CodingArena.Annotations;
using System;

namespace CodingArena.Main.Battlefields.Resources
{
    public class ResourceViewModel : Observable
    {
        private double myX;
        private double myY;

        public ResourceViewModel([NotNull] Resource resource)
        {
            Resource = resource ?? throw new ArgumentNullException(nameof(resource));
        }

        public Resource Resource { get; }

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