using System;

namespace CodingArena.Player.TurnActions
{
    public class MoveTowardsTurnAction : ITurnAction
    {
        internal MoveTowardsTurnAction(IGameObject gameObject)
        {
            GameObject = gameObject ?? throw new ArgumentNullException(nameof(gameObject));
        }

        public IGameObject GameObject { get; }
    }
}