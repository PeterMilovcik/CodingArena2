using System.Linq;

namespace CodingArena.Player.Rust
{
    public class Rust : IBotAI
    {
        public string BotName { get; } = "Rust";
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
