using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CodingArena.Player.Proto
{
    public class Proto : IBotAI
    {
        private IBot myAttacker;
        private Random Random { get; }
        public string BotName { get; } = "Proto";
        private List<Point> Corners { get; }
        private Point SafePoint { get; set; }

        public Proto()
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
            //if (ownBot.HitPoints.Percent < 90)
            //{
            //    if (ownBot.HasResource) return TurnAction.DropDownResource();
            //    return TurnAction.MoveTowards(SafePoint);
            //}

            if (ownBot.HasResource)
            {
                return ownBot.DistanceTo(ownBot.Home) > ownBot.Radius
                    ? TurnAction.MoveTowards(ownBot.Home)
                    : TurnAction.DropDownResource();
            }

            //if (myAttacker != null)
            //{
            //    if (ownBot.HasResource)
            //    {
            //        return TurnAction.DropDownResource();
            //    }

            //    if (battlefield.Bots.Contains(myAttacker))
            //    {
            //        return TurnAction.ShootAt(myAttacker);
            //    }

            //    myAttacker = null;
            //}

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
    }
}
