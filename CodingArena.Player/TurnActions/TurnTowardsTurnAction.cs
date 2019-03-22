using System;

namespace CodingArena.Player.TurnActions
{
    public class TurnTowardsTurnAction : ITurnAction
    {
        public TurnTowardsTurnAction(IGameObject gameObject)
        {
            GameObject = gameObject ?? throw new ArgumentNullException(nameof(gameObject));
        }

        public IGameObject GameObject { get; }
    }
}