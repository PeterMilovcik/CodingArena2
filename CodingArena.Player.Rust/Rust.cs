namespace CodingArena.Player.Rust
{
    public class Rust : IBotAI
    {
        public string BotName { get; } = "Rust";
        public ITurnAction Update(IBot ownBot, IBattlefield battlefield)
        {
            return TurnAction.Idle;
        }
    }
}
