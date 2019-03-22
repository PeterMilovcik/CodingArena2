using System.Windows;

namespace CodingArena.Player.TurnActions
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