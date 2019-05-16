using System.Linq;
using CodingArena.AI;

namespace CodingArena.Main.Battlefields.Bots.AIs.Demo
{
    internal class Twobit : BotAI
    {
        public Twobit()
        {
            BotName = nameof(Twobit);
        }
        public override string BotName { get; }

        public override ITurnAction Update(IBot ownBot, IBattlefield battlefield)
        {
            if (ownBot.HasResource)
            {
                return ownBot.DistanceTo(ownBot.Home) > ownBot.Radius
                    ? TurnAction.MoveTowards(ownBot.Home)
                    : TurnAction.DropDownResource();
            }

            if (battlefield.Resources.Any())
            {
                var resource = battlefield.Resources.OrderBy(r => r.DistanceTo(ownBot)).First();
                return ownBot.DistanceTo(resource) < ownBot.Radius
                    ? TurnAction.PickUpResource()
                    : TurnAction.MoveTowards(resource.Position);
            }

            return TurnAction.Idle;
        }
    }
}