using CodingArena.Annotations;
using CodingArena.Main.Battlefields;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace CodingArena.Common
{
    public interface IMovable : ICollider
    {
        double MinSpeed { get; }
        double MaxSpeed { get; }
        double Speed { get; }
        Vector Direction { get; }
        DateTime LastUpdate { get; }
        Task<bool> MoveAsync();
    }

    public abstract class Movable : Collider, IMovable
    {
        public Movable([NotNull] Battlefield battlefield)
        {
            Battlefield = battlefield ?? throw new ArgumentNullException(nameof(battlefield));
        }

        public Battlefield Battlefield { get; }
        public double MinSpeed { get; protected set; }
        public double MaxSpeed { get; protected set; }
        public double Speed { get; protected set; }
        public Vector Direction { get; protected set; }
        public DateTime LastUpdate { get; protected set; }
        public abstract Task<bool> MoveAsync();
    }
}