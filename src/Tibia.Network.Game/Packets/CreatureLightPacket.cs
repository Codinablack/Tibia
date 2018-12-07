using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class CreatureLightPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="creatureId">The creature identifier.</param>
        /// <param name="lightInfo">The light information.</param>
        public static void Add(NetworkMessage message, uint creatureId, ILightInfo lightInfo)
        {
            message.AddPacketType(GamePacketType.CreatureLightInfo);
            message.AddUInt32(creatureId);
            message.AddLightInfo(lightInfo);
        }
    }
}