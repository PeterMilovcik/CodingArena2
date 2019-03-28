using System;
using System.Threading.Tasks;
using System.Windows;
using CodingArena.Annotations;
using CodingArena.Common;
using CodingArena.Main.Battlefields.Bots;
using CodingArena.Main.Battlefields.Bullets;
using CodingArena.Player;

namespace CodingArena.Main.Battlefields.Weapons
{
    public abstract class Weapon : Collider
    {
        protected Battlefield myBattlefield;
        protected TimeSpan myAimTime;
        protected TimeSpan myReloadTime;
        protected Ammunition myAmmunition;
        protected TimeSpan myRemainingReloadTime;

        protected Weapon([NotNull] Battlefield battlefield)
        {
            myBattlefield = battlefield ?? throw new ArgumentNullException(nameof(battlefield));
        }

        protected Weapon([NotNull] Battlefield battlefield, Point position)
        {
            myBattlefield = battlefield;
            Position = position;
        }

        public string Name { get; protected set; }
        public double MaxRange { get; protected set; }
        public TimeSpan ReloadTime => new TimeSpan(myReloadTime.Ticks);
        public TimeSpan AimTime => new TimeSpan(myAimTime.Ticks);
        public bool IsReloading => myRemainingReloadTime > TimeSpan.Zero;
        public TimeSpan RemainingReloadTime => new TimeSpan(myRemainingReloadTime.Ticks);
        public IAmmunition Ammunition => myAmmunition;

        public override async Task UpdateAsync()
        {
            await base.UpdateAsync();
            if (IsReloading)
            {
                Reload();
            }
        }

        public void Reload()
        {
            if (IsReloading)
            {
                myRemainingReloadTime -= DeltaTime;
            }
        }

        public abstract Bullet Fire(Bot shooter);
    }
}