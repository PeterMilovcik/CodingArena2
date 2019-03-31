namespace CodingArena.AI
{
    public interface IBotAI
    {
        string BotName { get; }
        ITurnAction Update(IBot ownBot, IBattlefield battlefield);
        void OnDamaged(double damage, IBot shooter);
        void OnMoved(double distance);
        void OnCollisionWith(IBot bot);
        void OnResourcePicked();
        void OnWeaponPicked(IWeapon weapon);
        void OnRegenerated();
    }
}
