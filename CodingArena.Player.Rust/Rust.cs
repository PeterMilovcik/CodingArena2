using System.Linq;

namespace CodingArena.Player.Rust
{
    public class Rust : IBotAI
    {
        private bool myIsMoved;
        public string BotName { get; } = "Rust";
        public ITurnAction Update(IBot ownBot, IBattlefield battlefield)
        {
            var enemies = battlefield.Bots.Except(new[] { ownBot });
            if (enemies.Any())
            {
                if (myIsMoved)
                {
                    myIsMoved = false;
                    return TurnAction.Shoot();
                }
                myIsMoved = true;
                return TurnAction.MoveTowards(enemies.First().Position);
            }

            return TurnAction.Idle;
        }

        public void OnDamaged(double damage)
        {
        }
    }
}
