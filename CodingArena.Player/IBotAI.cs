namespace CodingArena.Player
{
    public interface IBotAI
    {
        string BotName { get; }
        ITurnAction Update(IBot ownBot, IBattlefield battlefield);
        void OnDamaged(double damage);
        void OnMoved(double distance);
        void OnCollisionWith(IBot bot);
    }
}
