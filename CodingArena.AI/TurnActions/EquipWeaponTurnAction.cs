using System;

namespace CodingArena.AI.TurnActions
{
    public class EquipWeaponTurnAction : ITurnAction
    {
        internal EquipWeaponTurnAction(IWeapon weapon)
        {
            Weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
        }

        public IWeapon Weapon { get; }
    }
}