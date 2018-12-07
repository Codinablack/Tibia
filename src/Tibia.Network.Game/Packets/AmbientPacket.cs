using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    internal class AmbientPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="lightInfo">The light information.</param>
        public static void Add(NetworkMessage message, ILightInfo lightInfo)
        {
            message.AddPacketType(GamePacketType.Ambient);
            message.AddByte(lightInfo.Level);
            message.AddByte(lightInfo.Color);
        }
    }
}