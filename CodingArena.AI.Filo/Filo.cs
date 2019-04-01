using System.Linq;

namespace CodingArena.AI.Filo
{
    public class Filo : IBotAI
    {
        public Filo()
        {
            BotName = "Filo";
        }

        public string BotName { get; }

        public ITurnAction Update(IBot ownBot, IBattlefield battlefield)
        {
            if (!battlefield.Resources.Any()) return TurnAction.Idle;
            var closestResource = battlefield.Resources.OrderBy(ownBot.DistanceTo).First();
            return ownBot.DistanceTo(closestResource) < ownBot.Radius
                ? TurnAction.PickUpResource()
                : TurnAction.MoveTowards(closestResource);
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

        public void OnWeaponPicked(IWeapon weapon)
        {

        }

        public void OnRegenerated()
        {

        }
    }
}
