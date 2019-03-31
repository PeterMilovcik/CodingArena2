using System.Windows;

namespace CodingArena.AI.TurnActions
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