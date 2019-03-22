using System.Windows;

namespace CodingArena.Player.TurnActions
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