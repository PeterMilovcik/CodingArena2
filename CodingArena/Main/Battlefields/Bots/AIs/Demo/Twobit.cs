/* -------------------------------------------------------------------------------------------------
   Restricted - Copyright (C) Siemens Healthcare GmbH/Siemens Medical Solutions USA, Inc., 2019. All rights reserved
   ------------------------------------------------------------------------------------------------- */

using CodingArena.Player;
using System.Linq;

namespace CodingArena.Main.Battlefields.Bots.AIs.Demo
{
    internal class Twobit : IBotAI
    {
        public Twobit()
        {
            BotName = nameof(Twobit);
        }
        public string BotName { get; }

        public ITurnAction Update(IBot ownBot, IBattlefield battlefield)
        {
            if (ownBot.HasResource)
            {
                return ownBot.DistanceTo(ownBot.Home) > ownBot.Radius
                    ? TurnAction.MoveTowards(ownBot.Home)
                    : TurnAction.DropDownResource();
            }

            if (battlefield.Resources.Any())
            {
                var resource = battlefield.Resources.OrderBy(r => r.DistanceTo(ownBot)).First();
                return ownBot.DistanceTo(resource) < ownBot.Radius
                    ? TurnAction.PickUpResource()
                    : TurnAction.MoveTowards(resource.Position);
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