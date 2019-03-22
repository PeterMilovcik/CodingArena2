using CodingArena.Player.TurnActions;

namespace CodingArena.Player
{
    public static class TurnAction
    {
        public static ITurnAction MoveTowards(IGameObject gameObject) => new MoveTowardsTurnAction(gameObject);
        public static ITurnAction MoveAwayFrom(IGameObject gameObject) => new MoveAwayFromTurnAction(gameObject);
        public static ITurnAction TurnTowards(IGameObject gameObject) => new TurnTowardsTurnAction(gameObject);
        public static ITurnAction TurnAwayFrom(IGameObject gameObject) => new TurnAwayFromTurnAction(gameObject);
    }
}