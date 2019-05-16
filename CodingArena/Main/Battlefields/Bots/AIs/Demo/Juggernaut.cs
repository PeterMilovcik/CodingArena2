using System.Linq;
using CodingArena.AI;

namespace CodingArena.Main.Battlefields.Bots.AIs.Demo
{
    internal class Juggernaut : BotAI
    {
        public Juggernaut()
        {
            BotName = nameof(Juggernaut);
        }

        public override string BotName { get; }

        public override ITurnAction Update(IBot ownBot, IBattlefield battlefield)
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
    }
}