using System.Linq;

namespace CodingArena.Player.Proto
{
    public class Proto : IBotAI
    {
        public string BotName { get; } = "Proto";

        public ITurnAction Update(IBot ownBot, IBattlefield battlefield)
        {
            var enemies = battlefield.Bots.Except(new[] { ownBot });
            if (enemies.Any())
            {
                return TurnAction.MoveTowards(enemies.First().Position);
            }

            return TurnAction.Idle;
        }
    }
}
