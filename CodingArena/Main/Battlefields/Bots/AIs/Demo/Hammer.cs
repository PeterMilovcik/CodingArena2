/* -------------------------------------------------------------------------------------------------
   Restricted - Copyright (C) Siemens Healthcare GmbH/Siemens Medical Solutions USA, Inc., 2019. All rights reserved
   ------------------------------------------------------------------------------------------------- */

using System.Linq;
using CodingArena.AI;

namespace CodingArena.Main.Battlefields.Bots.AIs.Demo
{
    internal class Hammer : BotAI
    {
        public Hammer()
        {
            BotName = nameof(Hammer);
        }

        public override string BotName { get; }

        public override ITurnAction Update(IBot ownBot, IBattlefield battlefield)
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
    }
}