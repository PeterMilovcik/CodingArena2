using System.Windows;

namespace CodingArena.AI.TurnActions
{
    public class TurnTowardsTurnAction : ITurnAction
    {
        internal TurnTowardsTurnAction(Point position)
        {
            Position = position;
        }

        public Point Position { get; }
    }
}