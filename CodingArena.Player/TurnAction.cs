using CodingArena.Player.TurnActions;
using System.Windows;

namespace CodingArena.Player
{
    public static class TurnAction
    {
        public static ITurnAction MoveTowards(IGameObject gameObject) =>
            new MoveTowardsTurnAction(gameObject.Position);

        public static ITurnAction MoveTowards(Point position) =>
            new MoveTowardsTurnAction(position);

        public static ITurnAction MoveAwayFrom(IGameObject gameObject) =>
            new MoveAwayFromTurnAction(gameObject.Position);

        public static ITurnAction MoveAwayFrom(Point position) =>
            new MoveAwayFromTurnAction(position);

        public static ITurnAction TurnTowards(IGameObject gameObject) =>
            new TurnTowardsTurnAction(gameObject.Position);

        public static ITurnAction TurnTowards(Point position) =>
            new TurnTowardsTurnAction(position);

        public static ITurnAction TurnAwayFrom(IGameObject gameObject) =>
            new TurnAwayFromTurnAction(gameObject.Position);

        public static ITurnAction TurnAwayFrom(Point position) =>
            new TurnAwayFromTurnAction(position);

        public static ITurnAction ShootAt(IGameObject gameObject) =>
            new ShootAtTurnAction(gameObject.Position);

        public static ITurnAction ShootAt(Point position) =>
            new ShootAtTurnAction(position);

        public static ITurnAction PickUpResource() =>
            new PickUpResourceTurnAction();

        public static ITurnAction DropDownResource() =>
            new DropDownResourceTurnAction();

        public static ITurnAction PickUpWeapon() =>
            new PickUpWeaponTurnAction();

        public static ITurnAction EquipWeapon(IWeapon weapon) =>
            new EquipWeaponTurnAction(weapon);

        public static ITurnAction PickUpFirstAidKit() =>
            new PickUpFirstAidKitTurnAction();

        public static ITurnAction Idle =>
            new IdleTurnAction();
    }
}