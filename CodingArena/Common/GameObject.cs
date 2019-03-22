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
        public Point Position { get; protected set; }
        public double DistanceTo(Player.IGameObject gameObject) =>
            Math.Sqrt(Math.Pow(gameObject.Position.X - Position.X, 2) + Math.Pow(gameObject.Position.Y - Position.Y, 2));

        public event EventHandler Changed;

        protected virtual void OnChanged() =>
            Changed?.Invoke(this, EventArgs.Empty);
    }
}