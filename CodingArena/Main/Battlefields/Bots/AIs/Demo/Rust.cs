﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CodingArena.AI;

namespace CodingArena.Main.Battlefields.Bots.AIs.Demo
{
    public class Rust : BotAI
    {
        private IBot myAttacker;
        public override string BotName { get; } = nameof(Rust);
        private List<Point> Corners { get; }

        public Rust()
        {
            Corners = new List<Point>();
        }

        public override ITurnAction Update(IBot ownBot, IBattlefield battlefield)
        {
            if (!Corners.Any())
            {
                Corners.Add(new Point(10, 10));
                Corners.Add(new Point(10, battlefield.Height - 10));
                Corners.Add(new Point(battlefield.Width - 10, battlefield.Height - 10));
                Corners.Add(new Point(battlefield.Width - 10, 10));
            }

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

            if (ownBot.HitPoints.Percent < 50)
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

            if (myAttacker != null)
            {
                if (ownBot.HasResource)
                {
                    return TurnAction.DropDownResource();
                }

                if (battlefield.Bots.Contains(myAttacker))
                {
                    return ownBot.DistanceTo(myAttacker) < ownBot.EquippedWeapon.MaxRange
                        ? TurnAction.ShootAt(myAttacker)
                        : TurnAction.MoveTowards(myAttacker);
                }

                myAttacker = null;
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
