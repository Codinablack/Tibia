using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class CreatureChangeAppearancePacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="creatureSpawnId">The creature spawn identifier.</param>
        /// <param name="outfit">The outfit.</param>
        /// <param name="mount">The mount.</param>
        public static void Add(NetworkMessage message, uint creatureSpawnId, IOutfit outfit, IMount mount)
        {
            message.AddPacketType(GamePacketType.CreatureChangeAppearance);
            message.AddUInt32(creatureSpawnId);
            message.AddAppearance(outfit, mount);
        }
    }
}