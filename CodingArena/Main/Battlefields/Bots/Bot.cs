using CodingArena.Annotations;
using CodingArena.Common;
using CodingArena.Main.Battlefields.FirstAidKits;
using CodingArena.Main.Battlefields.Homes;
using CodingArena.Main.Battlefields.Resources;
using CodingArena.Main.Battlefields.Weapons;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CodingArena.AI;
using CodingArena.AI.TurnActions;
using IWeapon = CodingArena.AI.IWeapon;

namespace CodingArena.Main.Battlefields.Bots
{
    public sealed class Bot : Movable, IBot
    {
        private const double MinDistance = 0.5;
        private Battlefield myBattlefield;
        private Weapon myWeapon;
        private TimeSpan myRemainingAimTime;
        private TimeSpan myRespawnIn;
        private List<Weapon> myAvailableWeapons;
        private bool myIsBotCollisionEnabled;

        public Bot([NotNull] Battlefield battlefield, IBotAI botAI) : base(battlefield)
        {
            Regeneration = new Regeneration(this);
            Init(battlefield, botAI);
        }

        public Regeneration Regeneration { get; }

        private void Init(Battlefield battlefield, IBotAI botAI)
        {
            Regeneration.Init();
            BotAI = botAI;
            Name = BotAI.BotName;
            myBattlefield = battlefield ?? throw new ArgumentNullException(nameof(battlefield));
            Radius = 20;
            var maxHitPoints = double.Parse(ConfigurationManager.AppSettings["MaxHitPoints"]);
            HitPoints = new Value(maxHitPoints, maxHitPoints);
            MaxSpeed = double.Parse(ConfigurationManager.AppSettings["MaxBotSpeed"]);
            MinSpeed = double.Parse(ConfigurationManager.AppSettings["MinBotSpeed"]);
            Speed = MaxSpeed;
            myWeapon = new Pistol(myBattlefield, new Point());
            myAvailableWeapons = new List<Weapon>(new[] { myWeapon });
            Angle = 90;
            myRemainingAimTime = TimeSpan.Zero;
            myRespawnIn = TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["RespawnInSeconds"]));
            myIsBotCollisionEnabled = bool.Parse(ConfigurationManager.AppSettings["BotCollision"]);
        }

        public IBotAI BotAI { get; set; }
        public string Name { get; set; }
        public IValue HitPoints { get; set; }
        public IResource Resource { get; private set; }
        public bool HasResource => Resource != null;
        public bool IsAiming => myRemainingAimTime > TimeSpan.Zero;
        public IWeapon EquippedWeapon => myWeapon;
        public IReadOnlyList<IWeapon> AvailableWeapons =>
            myAvailableWeapons.OfType<IWeapon>().ToList();
        public IHome Home => myBattlefield.Homes.SingleOrDefault(h => h.Name == Name);
        public TimeSpan RegenerationActiveIn => Regeneration.ActiveIn;
        public double RegenerationRate => Regeneration.Rate;
        public bool IsDead => HitPoints.Actual <= 0;
        public TimeSpan RespawnIn => new TimeSpan(myRespawnIn.Ticks);

        public override async Task UpdateAsync()
        {
            await base.UpdateAsync();

            if (IsDead)
            {
                myRespawnIn -= DeltaTime;
                OnChanged();
                if (myRespawnIn < TimeSpan.Zero)
                {
                    Respawn();
                }

                return;
            }

            await myWeapon.UpdateAsync();
            try
            {
                Regeneration.Update();
                if (IsAiming)
                {
                    Aim();
                    return;
                }
                var turnAction = BotAI.Update(this, myBattlefield);
                switch (turnAction)
                {
                    case ShootAtTurnAction shoot:
                        Execute(shoot);
                        break;
                    case MoveTowardsTurnAction moveTowards:
                        Execute(moveTowards);
                        break;
                    case MoveAwayFromTurnAction moveAway:
                        Execute(moveAway);
                        break;
                    case PickUpResourceTurnAction pickUpResource:
                        Execute(pickUpResource);
                        break;
                    case DropDownResourceTurnAction dropDownResource:
                        Execute(dropDownResource);
                        break;
                    case PickUpWeaponTurnAction pickUpAmmo:
                        Execute(pickUpAmmo);
                        break;
                    case EquipWeaponTurnAction equipWeapon:
                        Execute(equipWeapon);
                        break;
                    case PickUpFirstAidKitTurnAction pickUpFirstAidKit:
                        Execute(pickUpFirstAidKit);
                        break;
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void Respawn()
        {
            Init(Battlefield, BotAI);
            Position = new Point(Home.Position.X, Home.Position.Y);
            Battlefield.Add(this);
            OnRespawned();
        }

        private void Execute(PickUpResourceTurnAction pickUpResource)
        {
            var resource = Battlefield.Resources.OrderBy(DistanceTo).FirstOrDefault();
            if (resource != null)
            {
                if (DistanceTo(resource) < Radius)
                {
                    PickUpResource(resource);
                }
            }
        }

        private void Execute(PickUpWeaponTurnAction pickUpWeapon)
        {
            var weapon = Battlefield.Weapons.OfType<Weapon>().OrderBy(DistanceTo).FirstOrDefault();
            if (weapon != null)
            {
                if (DistanceTo(weapon) < Radius)
                {
                    PickUpWeapon(weapon);
                }
            }
        }

        private void Execute(PickUpFirstAidKitTurnAction pickUpFirstAidKit)
        {
            var firstAidKit = Battlefield.FirstAidKits.OfType<FirstAidKit>().OrderBy(DistanceTo).FirstOrDefault();
            if (firstAidKit != null)
            {
                if (DistanceTo(firstAidKit) < Radius)
                {
                    PickUpFirstAidKit(firstAidKit);
                }
            }
        }

        private void Execute(DropDownResourceTurnAction dropDownResource)
        {
            if (Resource == null) return;
            var home = Battlefield.Homes.OfType<Home>().Single(h => h.Name == Name);
            if (DistanceTo(home) < Radius)
            {
                Speed = MaxSpeed;
                OnResourceDropped(Resource);
                home.IncreaseCount();
                Resource = null;
            }
            else
            {
                DropDownResource();
            }
        }

        private void Execute(MoveTowardsTurnAction moveTowards)
        {
            if (DistanceTo(moveTowards.Position) < MinDistance)
            {
                return;
            }
            var movement = new Vector(moveTowards.Position.X - Position.X, moveTowards.Position.Y - Position.Y);
            movement.Normalize();
            Direction = movement;
            Move();
        }

        private void Execute(MoveAwayFromTurnAction moveAway)
        {
            var movement = new Vector(Position.X - moveAway.Position.X, Position.Y - moveAway.Position.Y);
            movement.Normalize();
            Direction = movement;
            Move();
        }

        private void Execute(ShootAtTurnAction shootAt)
        {
            if (IsAiming) return;
            if (EquippedWeapon.IsReloading) return;
            ShootAt(shootAt.Position);
        }

        private void Execute(EquipWeaponTurnAction equipWeapon)
        {
            if (!AvailableWeapons.Contains(equipWeapon.Weapon)) return;
            if (EquippedWeapon == equipWeapon.Weapon) return;
            myWeapon = (Weapon)equipWeapon.Weapon;
        }

        private bool Move()
        {
            var movement = new Vector(Direction.X, Direction.Y);
            movement.X *= Speed * DeltaTime.TotalSeconds;
            movement.Y *= Speed * DeltaTime.TotalSeconds;

            var afterMove = new Bot(Battlefield, BotAI)
            {
                Position = new Point(Position.X + movement.X, Position.Y + movement.Y),
                Radius = Radius
            };

            if (afterMove.Position.X > Battlefield.Width - 1 ||
                afterMove.Position.X < 0 ||
                afterMove.Position.Y > Battlefield.Height - 1 ||
                afterMove.Position.Y < 0)
            {
                Die();
                return true;
            }

            if (myIsBotCollisionEnabled)
            {
                var otherBots = Battlefield.Bots.Except(new[] { this });
                foreach (var bot in otherBots)
                {
                    if (bot.IsInCollisionWith(afterMove))
                    {
                        OnCollisionWith(bot);

                        return false;
                    }
                }
            }

            Position = new Point(afterMove.Position.X, afterMove.Position.Y);
            OnChanged();

            OnMoved(movement);

            return true;
        }

        private void OnCollisionWith(IBot bot)
        {
            try
            {
                BotAI.OnCollisionWith(bot);
            }
            catch
            {
                // ignored
            }
        }

        private void OnMoved(Vector movement)
        {
            try
            {
                BotAI.OnMoved(movement.Length);
            }
            catch
            {
                // ignored
            }
        }

        public void TakeDamageFrom(IBullet bullet)
        {
            var newActual = HitPoints.Actual - bullet.Damage;
            newActual = Math.Max(newActual, 0);
            HitPoints = new Value(HitPoints.Maximum, newActual);
            Regeneration.ResetActiveIn();
            OnDamaged(bullet);

            OnChanged();
            if (HitPoints.Actual <= 0)
            {
                Die(bullet.Shooter);
            }
        }

        private void OnDamaged(IBullet bullet)
        {
            try
            {
                BotAI.OnDamaged(bullet.Damage, bullet.Shooter);
            }
            catch
            {
                // ignored
            }
        }

        private void Die()
        {
            if (HasResource)
            {
                DropDownResource();
            }
            HitPoints = new Value(HitPoints.Maximum, 0);
            Battlefield.Remove(this);
            OnDied();
        }

        private void Die(IBot shooter)
        {
            Die();
        }

        private void ShootAt(Point position)
        {
            var movement = new Vector(position.X - Position.X, position.Y - Position.Y);
            movement.Normalize();
            Direction = movement;
            myRemainingAimTime = new TimeSpan(myWeapon.AimTime.Ticks);
        }

        public void PickUpResource(IResource resource)
        {
            if (HasResource) return;
            Resource = resource;
            Battlefield.Remove(resource);
            Speed = MinSpeed;
            OnResourcePicked();
            OnResourcePicked(Resource);
        }

        private void PickUpWeapon(Weapon weaponToPickUp)
        {
            var weapon = AvailableWeapons.SingleOrDefault(w => w.Name == weaponToPickUp.Name);
            if (weapon != null)
            {
                (weapon.Ammunition as Ammunition)?.Add(weaponToPickUp.Ammunition.Remaining);
            }
            else
            {
                myAvailableWeapons.Add(weaponToPickUp);
            }
            myWeapon = weaponToPickUp;
            Battlefield.Remove(weaponToPickUp);
            OnWeaponPicked(weaponToPickUp);
        }

        private void PickUpFirstAidKit(FirstAidKit firstAidKit)
        {
            Regeneration.Regenerate(firstAidKit.RegenerationAmount);
            Battlefield.Remove(firstAidKit);
        }

        private void OnResourcePicked()
        {
            try
            {
                BotAI.OnResourcePicked();
            }
            catch
            {
                // ignore
            }
        }

        private void OnWeaponPicked(Weapon weapon)
        {
            OnWeaponPicked(new WeaponEventArgs(weapon));
            try
            {
                BotAI.OnWeaponPicked((IWeapon)weapon);
            }
            catch
            {
                // ignore
            }
        }

        public void DropDownResource()
        {
            Speed = MaxSpeed;
            OnResourceDropped(Resource);
            Battlefield.Add(new Resource(new Point(Position.X, Position.Y)));
            Resource = null;
        }

        public event EventHandler<ResourceEventArgs> ResourcePicked;

        private void OnResourcePicked(IResource resource) =>
            ResourcePicked?.Invoke(this, new ResourceEventArgs(resource));


        public event EventHandler<ResourceEventArgs> ResourceDropped;

        private void OnResourceDropped(IResource resource) =>
            ResourceDropped?.Invoke(this, new ResourceEventArgs(resource));

        public event EventHandler<WeaponEventArgs> WeaponPicked;

        private void OnWeaponPicked(WeaponEventArgs e) => WeaponPicked?.Invoke(this, e);

        public event EventHandler Died;

        private void OnDied() => Died?.Invoke(this, EventArgs.Empty);

        public event EventHandler Respawned;

        private void OnRespawned() => Respawned?.Invoke(this, EventArgs.Empty);

        private void Aim()
        {
            if (IsDead) return;
            myRemainingAimTime -= DeltaTime;
            if (myRemainingAimTime <= TimeSpan.Zero)
            {
                var bullets = myWeapon.Fire(this);
                foreach (var bullet in bullets)
                {
                    Battlefield.Add(bullet);
                }
            }
        }

        public void SetPositionTo(Point position) => Position = position;
    }
}