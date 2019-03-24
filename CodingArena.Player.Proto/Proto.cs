using System.Linq;

namespace CodingArena.Player.Proto
{
    public class Proto : IBotAI
    {
        private IBot myAttacker;
        public string BotName { get; } = "Proto";

        public ITurnAction Update(IBot ownBot, IBattlefield battlefield)
        {
            if (ownBot.HasResource)
            {
                var home = battlefield.Homes.Single(h => h.Name == BotName);
                return ownBot.DistanceTo(home) > ownBot.Radius
                    ? TurnAction.MoveTowards(home)
                    : TurnAction.DropDownResource();
            }
            if (myAttacker != null)
            {
                return TurnAction.MoveAwayFrom(myAttacker);
            }
            if (battlefield.Resources.Any())
            {
                var resource = battlefield.Resources.First();
                if (ownBot.DistanceTo(resource) < ownBot.Radius) return TurnAction.PickUpResource();
                return TurnAction.MoveTowards(resource.Position);
            }

            return TurnAction.Idle;
        }

        public void OnDamaged(double damage, IBot shooter)
        {
            myAttacker = shooter;
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
