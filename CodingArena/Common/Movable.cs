using CodingArena.Annotations;
using CodingArena.Main.Battlefields;
using System;
using System.Windows;

namespace CodingArena.Common
{
    public interface IMovable : Player.IMovable
    {
    }

    public abstract class Movable : Collider, IMovable
    {
        private double myAngle;
        private Vector myDirection;

        protected Movable([NotNull] Battlefield battlefield)
        {
            Battlefield = battlefield ?? throw new ArgumentNullException(nameof(battlefield));
        }

        public Battlefield Battlefield { get; }
        public double MinSpeed { get; protected set; }
        public double MaxSpeed { get; protected set; }
        public double Speed { get; protected set; }

        public Vector Direction
        {
            get => new Vector(myDirection.X, myDirection.Y);
            protected set
            {
                if (myDirection.Equals(value)) return;
                myDirection = value;
                myAngle = CalculateAngle();
                OnChanged();
            }
        }

        private double CalculateAngle()
        {
            var radian = Math.Atan2(Direction.Y, Direction.X);
            var angle = radian * (180 / Math.PI);
            if (angle < 0) angle += 360;
            return angle;
        }

        public double Angle
        {
            get => myAngle;
            protected set
            {
                if (Math.Abs(myAngle - value) < 0.0001) return;
                myAngle = value;
                myDirection = new Vector(Math.Cos(Angle), Math.Sin(Angle));
                OnChanged();
            }
        }
    }
}