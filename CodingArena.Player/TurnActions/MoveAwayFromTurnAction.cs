using System.Windows;

namespace CodingArena.Player.TurnActions
{
    public class MoveAwayFromTurnAction : ITurnAction
    {
        internal MoveAwayFromTurnAction(Point position)
        {
            Position = position;
        }

        public Point Position { get; }
    }
}