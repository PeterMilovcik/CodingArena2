using System.Linq;

namespace CodingArena.Player.Proto
{
    public class Proto : IBotAI
    {
        public string BotName { get; } = "Proto";

        public ITurnAction Update(IBot ownBot, IBattlefield battlefield)
        {
            if (battlefield.Resources.Any())
            {
                return TurnAction.MoveTowards(battlefield.Resources.First().Position);
            }

            return TurnAction.Idle;
        }

        public void OnDamaged(double damage)
        {
        }

        public void OnMoved(double distance)
        {
        }
    }
}
