using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class InventoryItemPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inventoryItem">The inventory item.</param>
        /// <param name="slotType">Type of the slot.</param>
        public static void Add(NetworkMessage message, IItemSpawn inventoryItem, SlotType slotType)
        {
            if (inventoryItem != null)
            {
                message.AddPacketType(GamePacketType.InventoryItem);
                message.AddSlotType(slotType);
                message.AddItemSpawn(inventoryItem);
            }
            else
            {
                message.AddPacketType(GamePacketType.InventoryItemEmpty);
                message.AddSlotType(slotType);
            }
        }
    }
}