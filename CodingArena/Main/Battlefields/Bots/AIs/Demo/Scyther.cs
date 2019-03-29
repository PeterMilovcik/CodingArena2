using CodingArena.Player;
using System.Linq;

namespace CodingArena.Main.Battlefields.Bots.AIs.Demo
{
    internal class Scyther : IBotAI
    {
        public Scyther()
        {
            BotName = nameof(Scyther);
        }

        public string BotName { get; }

        public ITurnAction Update(IBot ownBot, IBattlefield battlefield)
        {
            var enemies = battlefield.Bots.Except(new[] { ownBot });

            if (ownBot.AvailableWeapons.Count == 1)
            {
                var closestWeapon = battlefield.Weapons.OrderBy(ownBot.DistanceTo).First();
                return ownBot.DistanceTo(closestWeapon) < ownBot.Radius
                    ? TurnAction.PickUpWeapon()
                    : TurnAction.MoveTowards(closestWeapon);
            }

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

        public void OnWeaponPicked(IWeapon weapon)
        {
        }
    }
}
