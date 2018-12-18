using System;

namespace Tibia.Data
{
    public static class CharacterSpawnExtensions
    {
        /// <summary>
        ///     Gets the item spawn from the inventory slot type.
        /// </summary>
        /// <param name="spawn">The spawn.</param>
        /// <param name="slotType">Type of the slot.</param>
        /// <returns>The item spawn.</returns>
        /// <exception cref="ArgumentOutOfRangeException">slotType - null</exception>
        public static IItemSpawn GetItemSpawnFromInventorySlotType(this ICharacterSpawn spawn, SlotType slotType)
        {
            switch (slotType)
            {
                case SlotType.Head:
                    return spawn.Head;
                case SlotType.Amulet:
                    return spawn.Amulet;
                case SlotType.Container:
                    return spawn.Container;
                case SlotType.Torso:
                    return spawn.Torso;
                case SlotType.Shield:
                    return spawn.Shield;
                case SlotType.Weapon:
                    return spawn.Weapon;
                case SlotType.Legs:
                    return spawn.Legs;
                case SlotType.Feet:
                    return spawn.Feet;
                case SlotType.Ring:
                    return spawn.Ring;
                case SlotType.Belt:
                    return spawn.Belt;
                default:
                    throw new ArgumentOutOfRangeException(nameof(slotType), slotType, null);
            }
        }
    }
}