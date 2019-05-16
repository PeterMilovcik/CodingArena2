namespace CodingArena.AI
{
    public abstract class BotAI
    {
        public abstract string BotName { get; }
        public abstract ITurnAction Update(IBot ownBot, IBattlefield battlefield);
        public virtual void OnDamaged(double damage, IBot attacker) { }
        public virtual void OnDied(IBot attacker) { }
        public virtual void OnMoved(double distance) { }
        public virtual void OnCollisionWith(IBot bot) { }
        public virtual void OnResourcePicked() { }
        public virtual void OnWeaponPicked(IWeapon weapon) { }
        public virtual void OnRegenerated() { }
    }
}
