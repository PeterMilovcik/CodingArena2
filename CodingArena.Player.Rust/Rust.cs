using System.Linq;

namespace CodingArena.Player.Rust
{
    public class Rust : IBotAI
    {
        public string BotName { get; } = "Rust";
        public ITurnAction Update(IBot ownBot, IBattlefield battlefield)
        {
            var enemies = battlefield.Bots.Except(new[] { ownBot }).ToList();
            return enemies.Any()
                ? TurnAction.ShootAt(enemies.First())
                : TurnAction.Idle;
        }

        public void OnDamaged(double damage, IBot shooter)
        {
        }

        public void OnMoved(double distance)
        {
        }

        public void OnCollisionWith(IBot bot)
        {
        }

        public void OnResourcePicked()
        {
        }
    }
}
