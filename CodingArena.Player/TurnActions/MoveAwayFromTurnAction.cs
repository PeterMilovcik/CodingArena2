using System;

namespace CodingArena.Player.TurnActions
{
    public class MoveAwayFromTurnAction : ITurnAction
    {
        internal MoveAwayFromTurnAction(IGameObject gameObject)
        {
            GameObject = gameObject ?? throw new ArgumentNullException(nameof(gameObject));
        }

        public IGameObject GameObject { get; }
    }
}