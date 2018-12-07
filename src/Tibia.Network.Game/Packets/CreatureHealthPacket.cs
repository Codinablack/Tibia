using Tibia.Data;
using Tibia.Spawns;

namespace Tibia.Network.Game.Packets
{
    public class CreatureHealthPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="creatureSpawn">The creature spawn.</param>
        public static void Add(NetworkMessage message, CreatureSpawn creatureSpawn)
        {
            message.AddPacketType(GamePacketType.CreatureHealth);
            message.AddUInt32(creatureSpawn.Id);
            message.AddPercent(creatureSpawn.Health.ToPercent());
        }
    }
}