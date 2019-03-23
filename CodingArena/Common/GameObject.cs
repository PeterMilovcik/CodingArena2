using System;
using System.Windows;

namespace CodingArena.Common
{
    public interface IGameObject : Player.IGameObject
    {
        event EventHandler Changed;
    }

    public class GameObject : IGameObject
    {
        private Point myPosition;

        public Point Position
        {
            get => myPosition;
            protected set
            {
                if (myPosition.Equals(value)) return;
                myPosition = value;
                OnChanged();
            }
        }

        public double DistanceTo(Player.IGameObject gameObject) =>
            DistanceTo(gameObject.Position);

        public double DistanceTo(Point position) =>
            Math.Sqrt(Math.Pow(position.X - Position.X, 2) + Math.Pow(position.Y - Position.Y, 2));

        public event EventHandler Changed;

        protected virtual void OnChanged() =>
            Changed?.Invoke(this, EventArgs.Empty);
    }
}