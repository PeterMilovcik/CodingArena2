using System;

namespace CodingArena.Player.TurnActions
{
    public class TurnTowardsTurnAction : ITurnAction
    {
        internal TurnTowardsTurnAction(IGameObject gameObject)
        {
            GameObject = gameObject ?? throw new ArgumentNullException(nameof(gameObject));
        }

        public IGameObject GameObject { get; }
    }
}