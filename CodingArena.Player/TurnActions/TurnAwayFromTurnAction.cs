using System;

namespace CodingArena.Player.TurnActions
{
    public class TurnAwayFromTurnAction : ITurnAction
    {
        internal TurnAwayFromTurnAction(IGameObject gameObject)
        {
            GameObject = gameObject ?? throw new ArgumentNullException(nameof(gameObject));
        }

        public IGameObject GameObject { get; }
    }
}