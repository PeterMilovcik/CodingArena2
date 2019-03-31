/* -------------------------------------------------------------------------------------------------
   Restricted - Copyright (C) Siemens Healthcare GmbH/Siemens Medical Solutions USA, Inc., 2019. All rights reserved
   ------------------------------------------------------------------------------------------------- */

using CodingArena.Common;
using CodingArena.Main.Battlefields.Bots;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CodingArena.AI;

namespace CodingArena.Main.Battlefields.Hospitals
{
    public class Hospital : Collider, IHospital
    {
        private readonly Battlefield myBattlefield;
        private readonly double myRegenerationPerSecond;

        public Hospital(Battlefield battlefield, Point position)
        {
            myBattlefield = battlefield;
            Radius = 30;
            Position = position;
            myRegenerationPerSecond = double.Parse(ConfigurationManager.AppSettings["HospitalRegenerationPerSecond"]);
        }

        public override async Task UpdateAsync()
        {
            await base.UpdateAsync();
            var botsToHeal = myBattlefield.Bots.OfType<Bot>().Where(b => DistanceTo(b) < Radius);
            var amount = myRegenerationPerSecond * DeltaTime.TotalSeconds;
            foreach (var bot in botsToHeal)
            {
                bot.Regeneration.Regenerate(amount);
            }

        }
    }
}