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
                var closest = enemies.OrderBy(e => e.DistanceTo(ownBot)).First();
                return ownBot.DistanceTo(closest) > ownBot.EquippedWeapon.MaxRange
                    ? TurnAction.MoveTowards(closest)
                    : TurnAction.ShootAt(closest);
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

        public void OnAmmoPicked(IAmmo ammo)
        {
        }
    }
}
