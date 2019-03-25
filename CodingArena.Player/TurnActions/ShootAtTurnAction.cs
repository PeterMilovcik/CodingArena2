using System.Windows;

namespace CodingArena.Player.TurnActions
{
    public class ShootAtTurnAction : ITurnAction
    {
        internal ShootAtTurnAction(Point position)
        {
            Position = position;
        }

        public Point Position { get; }
    }
}