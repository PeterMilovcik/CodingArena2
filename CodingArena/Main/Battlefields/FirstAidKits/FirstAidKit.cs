using CodingArena.Annotations;
using CodingArena.Common;
using System;
using System.Configuration;
using System.Windows;
using CodingArena.AI;

namespace CodingArena.Main.Battlefields.FirstAidKits
{
    public sealed class FirstAidKit : Collider, IFirstAidKit
    {
        [NotNull] private readonly Battlefield myBattlefield;

        public FirstAidKit([NotNull] Battlefield battlefield, Point position)
        {
            Radius = 10;
            myBattlefield = battlefield ?? throw new ArgumentNullException(nameof(battlefield));
            Position = position;
            RegenerationAmount = double.Parse(ConfigurationManager.AppSettings["FirstAidKitRegenerationAmount"]);
        }

        public double RegenerationAmount { get; }
    }
}