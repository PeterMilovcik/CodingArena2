using System.Windows;

namespace CodingArena.Player.TurnActions
{
    public class MoveTowardsTurnAction : ITurnAction
    {
        internal MoveTowardsTurnAction(Point position)
        {
            Position = position;
        }

        public Point Position { get; }
    }
}