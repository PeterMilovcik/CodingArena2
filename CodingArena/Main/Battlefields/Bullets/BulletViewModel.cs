﻿using CodingArena.Annotations;
using System;

namespace CodingArena.Main.Battlefields.Bullets
{
    public class BulletViewModel : Observable
    {
        private double myX;
        private double myY;

        public BulletViewModel([NotNull] Bullet bullet)
        {
            Bullet = bullet ?? throw new ArgumentNullException(nameof(bullet));
            Bullet.Changed += (sender, args) => Update();
            Update();
        }

        private void Update()
        {
            X = Bullet.Position.X;
            Y = Bullet.Position.Y;
        }

        public Bullet Bullet { get; }

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