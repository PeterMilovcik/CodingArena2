using System;

namespace CodingArena.Common
{
    public interface IGameObject
    {
        double X { get; }
        double Y { get; }
        double DistanceTo(IGameObject gameObject);
        event EventHandler Changed;
    }

    public class GameObject : IGameObject
    {
        public double X { get; protected set; }
        public double Y { get; protected set; }
        public double DistanceTo(IGameObject gameObject) =>
            Math.Sqrt(Math.Pow(gameObject.X - X, 2) + Math.Pow(gameObject.Y - Y, 2));

        public event EventHandler Changed;

        protected virtual void OnChanged() =>
            Changed?.Invoke(this, EventArgs.Empty);
    }
}