using System;
using System.Windows;
using System.Windows.Media;

namespace CodingArena.Main.Battlefields.Bots
{
    public sealed class BotViewModel : Observable
    {
        private double myX;
        private double myY;
        private double myWeaponX;
        private double myWeaponY;
        private double myHP;
        private string myName;
        private double myAngle;
        private Visibility myWeaponVisibility;
        private Visibility myResourceVisibility;
        private Brush myColor;
        private const double WeaponSize = 30;

        public BotViewModel(Bot bot)
        {
            Bot = bot;
            Name = Bot.Name;
            Bot.Changed += (sender, args) => Update();
            Bot.ResourcePicked += (sender, args) => HasResource = true;
            Bot.ResourcePicked += (sender, args) => HasResource = false;
            Bot.Died += (sender, args) => OnDied();
            Color = Brushes.Black;
            Update();
        }

        public Bot Bot { get; }

        public void Update()
        {
            X = Bot.Position.X;
            Y = Bot.Position.Y;
            HP = Bot.HitPoints.Actual;
            HasResource = Bot.HasResource;
        }

        public event EventHandler Died;

        public string Name
        {
            get => myName;
            set
            {
                if (value == myName) return;
                myName = value;
                OnPropertyChanged();
            }
        }

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

        public double Angle
        {
            get => myAngle;
            set
            {
                if (value.Equals(myAngle)) return;
                myAngle = value;
                WeaponX = 20 + WeaponSize * Math.Cos(Angle * Math.PI / 180);
                WeaponY = 20 + WeaponSize * Math.Sin(Angle * Math.PI / 180);
                OnPropertyChanged();
            }
        }

        public double WeaponX
        {
            get => myWeaponX;
            set
            {
                if (value.Equals(myWeaponX)) return;
                myWeaponX = value;
                OnPropertyChanged();
            }
        }

        public double WeaponY
        {
            get => myWeaponY;
            set
            {
                if (value.Equals(myWeaponY)) return;
                myWeaponY = value;
                OnPropertyChanged();
            }
        }

        public double HP
        {
            get => myHP;
            set
            {
                if (value.Equals(myHP)) return;
                myHP = value;
                OnPropertyChanged();
            }
        }

        public Visibility WeaponVisibility
        {
            get => myWeaponVisibility;
            set
            {
                if (value == myWeaponVisibility) return;
                myWeaponVisibility = value;
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

        public bool HasResource
        {
            set
            {
                if (value)
                {
                    WeaponVisibility = Visibility.Hidden;
                    ResourceVisibility = Visibility.Visible;
                }
                else
                {
                    WeaponVisibility = Visibility.Visible;
                    ResourceVisibility = Visibility.Hidden;
                }
            }
        }

        private void OnDied() => Died?.Invoke(this, EventArgs.Empty);
    }
}