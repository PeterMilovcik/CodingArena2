using CodingArena.Annotations;
using System;
using System.Windows;
using CodingArena.AI;

namespace CodingArena.Main.Battlefields.Resources
{
    public class ResourceViewModel : Observable
    {
        private double myX;
        private double myY;
        private Visibility myResourceVisibility;

        public ResourceViewModel([NotNull] IResource resource)
        {
            Resource = resource ?? throw new ArgumentNullException(nameof(resource));
            X = Resource.Position.X;
            Y = Resource.Position.Y;
        }

        public IResource Resource { get; }

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

        public Visibility ResourceVisibility
        {
            get => myResourceVisibility;
            set
            {
                if (value == myResourceVisibility) return;
                myResourceVisibility = value;
                OnPropertyChanged();
            }
        }
    }
}