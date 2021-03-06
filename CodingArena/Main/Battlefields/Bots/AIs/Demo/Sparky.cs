﻿using System.Linq;
using CodingArena.AI;

namespace CodingArena.Main.Battlefields.Bots.AIs.Demo
{
    internal class Sparky : BotAI
    {
        public Sparky()
        {
            BotName = nameof(Sparky);
        }
        public override string BotName { get; }

        public override ITurnAction Update(IBot ownBot, IBattlefield battlefield)
        {
            if (ownBot.EquippedWeapon.Ammunition.Remaining == 0)
            {
                var weapon = battlefield.Weapons.OrderBy(a => a.DistanceTo(ownBot)).FirstOrDefault();
                if (weapon != null)
                {
                    return ownBot.DistanceTo(weapon) > ownBot.Radius
                        ? TurnAction.MoveTowards(weapon)
                        : TurnAction.PickUpWeapon();
                }
            }

            if (ownBot.HitPoints.Percent < 30)
            {
                if (ownBot.HasResource) return TurnAction.DropDownResource();
                return TurnAction.MoveTowards(battlefield.Hospitals.First());
            }

            if (ownBot.HasResource)
            {
                return ownBot.DistanceTo(ownBot.Home) > ownBot.Radius
                    ? TurnAction.MoveTowards(ownBot.Home)
                    : TurnAction.DropDownResource();
            }

            var target = battlefield.Bots.Except(new[] { ownBot })
                .Where(b => b.HasResource)
                .OrderBy(b => b.DistanceTo(ownBot))
                .FirstOrDefault();
            if (target != null)
            {
                return ownBot.DistanceTo(target) < ownBot.EquippedWeapon.MaxRange
                    ? TurnAction.ShootAt(target)
                    : TurnAction.MoveTowards(target);
            }

            if (battlefield.Resources.Any())
            {
                var resource = battlefield.Resources.OrderBy(r => r.DistanceTo(ownBot)).First();
                if (ownBot.DistanceTo(resource) < ownBot.Radius) return TurnAction.PickUpResource();
                return TurnAction.MoveTowards(resource.Position);
            }

            return TurnAction.Idle;
        }
    }
}
