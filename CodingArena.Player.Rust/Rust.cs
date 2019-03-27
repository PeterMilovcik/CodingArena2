﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CodingArena.Player.Rust
{
    public class Rust : IBotAI
    {
        private IBot myAttacker;
        private Random Random { get; }
        public string BotName { get; } = nameof(Rust);
        private List<Point> Corners { get; }
        private Point SafePoint { get; set; }

        public Rust()
        {
            Random = new Random();
            Corners = new List<Point>();
        }

        public ITurnAction Update(IBot ownBot, IBattlefield battlefield)
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
                var ammo = battlefield.Ammos.OrderBy(a => a.DistanceTo(ownBot)).FirstOrDefault();
                if (ammo != null)
                {
                    return ownBot.DistanceTo(ammo) > ownBot.Radius
                        ? TurnAction.MoveTowards(ammo)
                        : TurnAction.PickUpAmmo();
                }
            }

            if (ownBot.HitPoints.Percent < 50)
            {
                if (ownBot.HasResource) return TurnAction.DropDownResource();
                return TurnAction.MoveTowards(SafePoint);
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

        public void OnDamaged(double damage, IBot shooter)
        {
            myAttacker = shooter;
            SafePoint = Corners[Random.Next(4)];
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
