using System.Linq;
using CodingArena.AI;

namespace CodingArena.Main.Battlefields.Bots.AIs.Demo
{
    internal class Juggernaut : IBotAI
    {
        public Juggernaut()
        {
            BotName = nameof(Juggernaut);
        }

        public string BotName { get; }

        public ITurnAction Update(IBot ownBot, IBattlefield battlefield)
        {
            var enemies = battlefield.Bots.Except(new[] { ownBot }).Where(b => b.HasResource);

            if (enemies.Any())
            {
                var closestEnemy = enemies.OrderBy(ownBot.DistanceTo).First();
                if (ownBot.EquippedWeapon.Ammunition.Remaining > 0)
                {
                    return ownBot.DistanceTo(closestEnemy) < ownBot.EquippedWeapon.MaxRange / 2
                        ? TurnAction.ShootAt(closestEnemy)
                        : TurnAction.MoveTowards(closestEnemy);
                }

                var closestWeapon = battlefield.Weapons.OrderBy(ownBot.DistanceTo).First();
                return ownBot.DistanceTo(closestWeapon) < ownBot.Radius
                    ? TurnAction.PickUpWeapon()
                    : TurnAction.MoveTowards(closestWeapon);
            }
            else
            {
                var closestWeapon = battlefield.Weapons.OrderBy(ownBot.DistanceTo).First();
                return ownBot.DistanceTo(closestWeapon) < ownBot.Radius
                    ? TurnAction.PickUpWeapon()
                    : TurnAction.MoveTowards(closestWeapon);
            }
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