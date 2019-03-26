using System;
using System.Threading.Tasks;
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
        private DateTime myLastUpdate;
        private TimeSpan myDeltaTime;

        public GameObject()
        {
            LastUpdate = DateTime.MinValue;
            DeltaTime = TimeSpan.Zero;
        }

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

        public DateTime LastUpdate
        {
            get => new DateTime(myLastUpdate.Ticks);
            set => myLastUpdate = value;
        }

        public TimeSpan DeltaTime
        {
            get => new TimeSpan(myDeltaTime.Ticks);
            set => myDeltaTime = value;
        }

        public virtual Task UpdateAsync()
        {
            if (LastUpdate != DateTime.MinValue)
            {
                DeltaTime = DateTime.Now - LastUpdate;
            }
            LastUpdate = DateTime.Now;
            return Task.CompletedTask;
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