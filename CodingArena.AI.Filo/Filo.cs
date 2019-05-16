using System.Linq;

namespace CodingArena.AI.Filo
{
    public class Filo : BotAI
    {
        public Filo()
        {
            BotName = "Filo";
        }

        public override string BotName { get; }

        public override ITurnAction Update(IBot ownBot, IBattlefield battlefield)
        {
            if (!battlefield.Resources.Any()) return TurnAction.Idle;
            return ownBot.HasResource 
                ? BaseResource(ownBot) 
                : GetResource(ownBot, battlefield);
        }

        private ITurnAction BaseResource(IBot ownBot) =>
            ownBot.DistanceTo(ownBot.Home) < ownBot.Radius
                ? TurnAction.DropDownResource()
                : TurnAction.MoveTowards(ownBot.Home);

        private static ITurnAction GetResource(IBot ownBot, IBattlefield battlefield)
        {
            var closestResource = battlefield.Resources.OrderBy(ownBot.DistanceTo).First();
            return ownBot.DistanceTo(closestResource) < ownBot.Radius
                ? TurnAction.PickUpResource()
                : TurnAction.MoveTowards(closestResource);
        }
    }
}
