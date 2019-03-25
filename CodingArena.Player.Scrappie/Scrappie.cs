using System.Linq;

namespace CodingArena.Player.Scrappie
{
    public class Scrappie : IBotAI
    {
        public string BotName { get; } = "Scrappie";
        public ITurnAction Update(IBot ownBot, IBattlefield battlefield)
        {
            var enemies = battlefield.Bots.Except(new[] { ownBot }).ToList();
            if (enemies.Any())
            {
                var first = enemies.First();
                return ownBot.DistanceTo(first) > ownBot.EquippedWeapon.MaxRange
                    ? TurnAction.MoveTowards(first)
                    : TurnAction.ShootAt(first);
            }
            //var enemies = battlefield.Bots.Except(new[] { ownBot });
            //if (enemies.Any())
            //{
            //    return TurnAction.MoveTowards(enemies.First().Position);
            //}

            return TurnAction.Idle;
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
