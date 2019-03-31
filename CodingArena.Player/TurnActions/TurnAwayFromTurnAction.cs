using System.Windows;

namespace CodingArena.AI.TurnActions
{
    public class TurnAwayFromTurnAction : ITurnAction
    {
        internal TurnAwayFromTurnAction(Point position)
        {
            Position = position;
        }

        public Point Position { get; }
    }
}